// <copyright file="Weaver.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Weaver
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using CrossCutterN.Aspect.Aspect;
    using CrossCutterN.Weaver.Statistics;
    using CrossCutterN.Weaver.Switch;
    using CrossCutterN.Weaver.Utilities;
    using Mono.Cecil;
    using Mono.Cecil.Pdb;

    /// <summary>
    /// Weaver implementation.
    /// </summary>
    internal sealed class Weaver : ICanAddAspect<IWeaver>, IWeaver
    {
        private ICanAddAspect<IWeavingPlanner> plannerBuilder = WeaverFactory.InitializeWeavingPlanner();
        private IWeavingPlanner planner;

        /// <inheritdoc/>
        public void AddAspect(string aspectName, IAspect aspect, IReadOnlyDictionary<JoinPoint, int> sequenceDict)
        {
            if (plannerBuilder == null)
            {
                throw new InvalidOperationException("The weaver can't accept aspect build any more.");
            }

            plannerBuilder.AddAspect(aspectName, aspect, sequenceDict);
        }

        /// <inheritdoc/>
        public IAssemblyWeavingStatistics Weave(string inputAssemblyPath, bool includeSymbol, string outputAssemblyPath, string strongNameKeyFile)
        {
            if (string.IsNullOrWhiteSpace(inputAssemblyPath))
            {
                throw new ArgumentNullException("inputAssemblyPath");
            }

            if (string.IsNullOrWhiteSpace(outputAssemblyPath))
            {
                throw new ArgumentNullException("outputAssemblyPath");
            }

            // Mono.Cecil implementation has issues that if we directly pass file name for input assembly,
            // the assembly can't be overwritten if we give the same file name for output assembly,
            // So here we directly convert input assembly file to memory stream to ensure that input assembly file handle is release when outputing.
            var readerParameters = new ReaderParameters();
            if (includeSymbol)
            {
                readerParameters.ReadSymbols = true;
                readerParameters.SymbolReaderProvider = new PdbReaderProvider();
                readerParameters.SymbolStream = new MemoryStream(File.ReadAllBytes(Path.ChangeExtension(inputAssemblyPath, "pdb")));
            }

            var assembly = AssemblyDefinition.ReadAssembly(new MemoryStream(File.ReadAllBytes(inputAssemblyPath)), readerParameters);
            var assemblyStatistics = StatisticsFactory.InitializeAssemblyWeavingRecord(assembly.FullName);

            try
            {
                foreach (var module in assembly.Modules)
                {
                    var moduleStatistics = StatisticsFactory.InitializeModuleWeavingRecord(module.Name);
                    var context = WeaverFactory.InitializeMethodWeavingContext(module);
                    foreach (var clazz in module.GetTypes().Where(tp => tp.IsClass && (tp.HasMethods || tp.HasFields)))
                    {
                        // supporting data structure initialization.
                        var classStatistics = StatisticsFactory.InitializeClassWeavingRecord(clazz.Name, clazz.FullName, clazz.Namespace);
                        var switchHandlerBuilder = SwitchFactory.InitializeSwitchHandler(clazz, context.GetTypeReference(typeof(int)));
                        var classCustomAttributes = GetClassCustomAttributes(clazz);
                        var methodWeaver = new MethodWeaver(classCustomAttributes, context, switchHandlerBuilder);

                        WeaveProperties(clazz, classCustomAttributes, methodWeaver, classStatistics);
                        WeaveMethods(clazz, classCustomAttributes, methodWeaver, classStatistics);
                        WeaveSwitches(switchHandlerBuilder.Build(), clazz, context, classStatistics);

                        // handle statistics
                        var classStatisticsFinished = classStatistics.Build();
                        if (classStatisticsFinished.WeavedMethodPropertyCount > 0)
                        {
                            moduleStatistics.AddClassWeavingStatistics(classStatisticsFinished);
                        }
                    }

                    AddAssemblyReference(context, module, moduleStatistics);

                    // handle statistics.
                    var moduleStatisticsFinished = moduleStatistics.Build();
                    if (moduleStatisticsFinished.WeavedClassCount > 0)
                    {
                        assemblyStatistics.AddModuleWeavingStatistics(moduleStatisticsFinished);
                    }
                }

                var writerParameters = new WriterParameters();
                if (includeSymbol)
                {
                    writerParameters.WriteSymbols = true;
                    writerParameters.SymbolWriterProvider = new PdbWriterProvider();
                }

                if (!string.IsNullOrWhiteSpace(strongNameKeyFile))
                {
                    writerParameters.StrongNameKeyPair = new StrongNameKeyPair(File.ReadAllBytes(strongNameKeyFile));
                }

                assembly.Write(outputAssemblyPath, writerParameters);
            }
            catch (Exception e)
            {
                assemblyStatistics.Exception = e;
            }

            return assemblyStatistics.Build();
        }

        /// <inheritdoc/>
        public IWeaver Build()
        {
            if (plannerBuilder == null)
            {
                throw new InvalidOperationException("The weaver has been set to read-only already.");
            }

            planner = plannerBuilder.Build();
            plannerBuilder = null;
            return this;
        }

        private static bool IsPropertyMethod(MethodDefinition method) => method.IsGetter || method.IsSetter;

        private static IReadOnlyList<CrossCutterN.Aspect.Metadata.ICustomAttribute> GetClassCustomAttributes(TypeDefinition clazz)
        {
            var classCustomAttributes = new List<CrossCutterN.Aspect.Metadata.ICustomAttribute>();
            if (clazz.HasCustomAttributes)
            {
                for (var i = 0; i < clazz.CustomAttributes.Count; i++)
                {
                    classCustomAttributes.Add(clazz.CustomAttributes.ElementAt(i).Convert(i));
                }
            }

            return classCustomAttributes;
        }

        private static void WeaveSwitches(
            ISwitchHandler switchHandler,
            TypeDefinition clazz,
            IWeavingContext context,
            IClassWeavingStatisticsBuilder statistics)
        {
            var switchData = switchHandler.GetData().ToList();
            if (switchData.Any())
            {
                SwitchInitializationWeaver.Weave(clazz, context, switchData, statistics);
            }
        }

        private static void AddAssemblyReference(IWeavingContext context, ModuleDefinition module, IModuleWeavingStatisticsBuilder statistics)
        {
            var assemblyReferences = context.AssemblyReferences;
            if (assemblyReferences != null && assemblyReferences.Any())
            {
                foreach (var reference in assemblyReferences)
                {
                    if (!module.AssemblyReferences.Any(r => Equals(r.Name, reference.Name)))
                    {
                        module.AssemblyReferences.Add(reference);
                        statistics.AddAssemblyReference(reference.FullName);
                    }
                }
            }
        }

        private void WeaveProperties(
            TypeDefinition clazz,
            IReadOnlyList<CrossCutterN.Aspect.Metadata.ICustomAttribute> classCustomAttributes,
            MethodWeaver methodWeaver,
            IClassWeavingStatisticsBuilder statistics)
        {
            foreach (var property in clazz.Properties)
            {
                var propertyInfo = property.Convert(classCustomAttributes);
                var plan = planner.MakePlan(propertyInfo);
                if (!plan.IsEmpty())
                {
                    var propertyStatistics = StatisticsFactory.InitializePropertyWeavingRecord(property.Name, property.FullName);
                    var getterPlan = plan.GetterPlan;
                    var getter = property.GetMethod;
                    if (!getterPlan.IsEmpty() && getter != null)
                    {
                        methodWeaver.Weave(getter, getterPlan, property.Name, propertyStatistics.GetterContainer);
                    }

                    var setterPlan = plan.SetterPlan;
                    var setter = property.SetMethod;
                    if (!setterPlan.IsEmpty() && setter != null)
                    {
                        methodWeaver.Weave(setter, setterPlan, property.Name, propertyStatistics.SetterContainer);
                    }

                    var propertyStatisticsFinished = propertyStatistics.Build();
                    if (propertyStatisticsFinished != null)
                    {
                        statistics.AddPropertyWeavingStatistics(propertyStatisticsFinished);
                    }
                }
            }
        }

        private void WeaveMethods(
            TypeDefinition clazz,
            IReadOnlyList<CrossCutterN.Aspect.Metadata.ICustomAttribute> classCustomAttributes,
            MethodWeaver methodWeaver,
            IClassWeavingStatisticsBuilder statistics)
        {
            foreach (var method in clazz.Methods)
            {
                // methods without bodies can't be injected
                // property getter and setter will be handled in property phase
                if (!method.HasBody || IsPropertyMethod(method))
                {
                    continue;
                }

                var methodInfo = method.Convert(classCustomAttributes);
                var plan = planner.MakePlan(methodInfo);
                if (!plan.IsEmpty())
                {
                    var methodStatistics = StatisticsFactory.InitializeMethodWeavingRecord(method.Name, method.FullName);
                    methodWeaver.Weave(method, plan, null, methodStatistics);
                    var methodStatisticsFinished = methodStatistics.Build();
                    if (methodStatisticsFinished != null)
                    {
                        statistics.AddMethodWeavingStatistics(methodStatisticsFinished);
                    }
                }
            }
        }
    }
}

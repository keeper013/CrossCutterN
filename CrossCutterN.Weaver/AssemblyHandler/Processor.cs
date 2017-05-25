/**
* Description: Main entrance of assembly handler
* Author: David Cui
*/

namespace CrossCutterN.Weaver.AssemblyHandler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Pdb;
    using Advice.Common;
    using Aspect;
    using Statistics;
    using Utilities;
    using Batch;
    using Switch;

    internal static class Processor
    {
        public static IAssemblyWeavingStatistics Weave(string inputAssemblyPath, string outputAssemblyPath, IWeavingBatch batch, bool includeSymbol)
        {
            if(includeSymbol && !File.Exists(Path.ChangeExtension(inputAssemblyPath, "pdb")))
            {
                throw new ArgumentException("Can't find pdb file for symbol", "includeSymbol");
            }
            var readerParameters = new ReaderParameters
            {
                ReadSymbols = includeSymbol,
                SymbolReaderProvider = includeSymbol ? new PdbReaderProvider() : null
            };
            var assembly = AssemblyDefinition.ReadAssembly(inputAssemblyPath, readerParameters);
            var assemblyStatistics = StatisticsFactory.InitializeAssemblyWeavingRecord(assembly.FullName);
            
            try
            {
                foreach (var module in assembly.Modules)
                {
                    var moduleStatistics = StatisticsFactory.InitializeModuleWeavingRecord(module.FullyQualifiedName);
                    var context = WeavingFactory.InitializeMethodWeavingContext(module);
                    foreach (var clazz in module.GetTypes().Where(tp => tp.IsClass && (tp.HasMethods || tp.HasFields)))
                    {
                        var classStatistics = StatisticsFactory.InitializeClassWeavingRecord(clazz.Name, clazz.FullName, clazz.Namespace);
                        var classCustomAttributes = new List<Aspect.Concern.ICustomAttribute>();
                        if (clazz.HasCustomAttributes)
                        {
                            for (var i = 0; i < clazz.CustomAttributes.Count; i++)
                            {
                                classCustomAttributes.Add(clazz.CustomAttributes.ElementAt(i).Convert(i));
                            }
                        }
                        var switchHandler = SwitchFactory.InitializeSwitchHandler(clazz, context.GetTypeReference(typeof(int)));
                        foreach (var property in clazz.Properties)
                        {
                            ProcessProperty(property, batch, classCustomAttributes, context, switchHandler, classStatistics);
                        }
                        foreach (var method in clazz.Methods)
                        {
                            // methods without bodies can't be injected
                            // property getter and setter will be handled in property phase
                            if (!method.HasBody || method.IsPropertyMethod())
                            {
                                continue;
                            }
                            ProcessMethod(method, batch, classCustomAttributes, context, switchHandler, classStatistics);
                        }
                        WProcessStaticConstructor(clazz, context, switchHandler.Convert());
                        var classStatisticsFinished = classStatistics.Convert();
                        if(classStatisticsFinished.WeavedMethodPropertyCount > 0)
                        {
                            moduleStatistics.AddClassWeavingStatistics(classStatisticsFinished);
                        }
                    }
                    var moduleStatisticsFinished = moduleStatistics.Convert();
                    if(moduleStatisticsFinished.WeavedClassCount > 0)
                    {
                        assemblyStatistics.AddModuleWeavingStatistics(moduleStatisticsFinished);
                    }
                }
                var writerParameters = new WriterParameters
                {
                    WriteSymbols = includeSymbol,
                    SymbolWriterProvider = includeSymbol ? new PdbWriterProvider() : null
                };
                assembly.Write(outputAssemblyPath, writerParameters);
            }
            catch(Exception e)
            {
                assemblyStatistics.Exception = e;
            }
            return assemblyStatistics.Convert();
        }

        private static void ProcessMethod(MethodDefinition method, IWeavingBatch batch, List<Aspect.Concern.ICustomAttribute> classCustomAttributes,
            IWeavingContext context, IWriteOnlySwitchHandler switchHandler, IWriteOnlyClassWeavingStatistics classStatistics)
        {
            var methodInfo = method.Convert(classCustomAttributes.AsReadOnly());
            var plan = batch.BuildPlan(methodInfo);
            if (!plan.IsEmpty())
            {
                switchHandler.Property = null;
                var methodStatistics = StatisticsFactory.InitializeMethodWeavingRecord(method.Name, method.FullName);
                var ilhandler = WeavingFactory.InitializeIlHandler(method, context);
                SetLocalParameters(method, ilhandler, plan);
                WeaveEntryJoinPoint(ilhandler, plan.GetAdvices(JoinPoint.Entry), switchHandler, methodStatistics);
                WeaveExceptionJoinPoint(ilhandler, plan.GetAdvices(JoinPoint.Exception), switchHandler, methodStatistics);
                WeaveExitJoinPoint(ilhandler, plan.GetAdvices(JoinPoint.Exit), switchHandler, methodStatistics);
                var methodStatisticsFinished = methodStatistics.Convert();
                if (methodStatisticsFinished.JoinPointCount > 0)
                {
                    classStatistics.AddMethodWeavingStatistics(methodStatisticsFinished);
                }
            }
        }

        private static void ProcessProperty(PropertyDefinition property, IWeavingBatch batch, List<Aspect.Concern.ICustomAttribute> classCustomAttributes,
            IWeavingContext context, IWriteOnlySwitchHandler switchHandler, IWriteOnlyClassWeavingStatistics classStatistics)
        {
            var propertyInfo = property.Convert(classCustomAttributes.AsReadOnly());
            var plan = batch.BuildPlan(propertyInfo);
            if (!plan.IsEmpty())
            {
                switchHandler.Property = property.Name;
                var propertyStatistics = StatisticsFactory.InitializePropertyWeavingRecord(property.Name, property.FullName);
                var getterPlan = plan.GetterPlan;
                var getter = property.GetMethod;
                if (!getterPlan.IsEmpty() && getter != null)
                {
                    var ilhandler = WeavingFactory.InitializeIlHandler(getter, context);
                    SetLocalParameters(getter, ilhandler, getterPlan);
                    WeaveEntryJoinPoint(ilhandler, getterPlan.GetAdvices(JoinPoint.Entry), switchHandler, propertyStatistics.GetterContainer);
                    WeaveExceptionJoinPoint(ilhandler, getterPlan.GetAdvices(JoinPoint.Exception), switchHandler, propertyStatistics.GetterContainer);
                    WeaveExitJoinPoint(ilhandler, getterPlan.GetAdvices(JoinPoint.Exit), switchHandler, propertyStatistics.GetterContainer);
                }
                var setterPlan = plan.SetterPlan;
                var setter = property.SetMethod;
                if (!setterPlan.IsEmpty() && setter != null)
                {
                    var ilhandler = WeavingFactory.InitializeIlHandler(setter, context);
                    SetLocalParameters(setter, ilhandler, setterPlan);
                    WeaveEntryJoinPoint(ilhandler, setterPlan.GetAdvices(JoinPoint.Entry), switchHandler, propertyStatistics.SetterContainer);
                    WeaveExceptionJoinPoint(ilhandler, setterPlan.GetAdvices(JoinPoint.Exception), switchHandler, propertyStatistics.SetterContainer);
                    WeaveExitJoinPoint(ilhandler, setterPlan.GetAdvices(JoinPoint.Exit), switchHandler, propertyStatistics.SetterContainer);
                }
                var propertyStatisticsFinished = propertyStatistics.Convert();
                if (propertyStatisticsFinished.JoinPointCount > 0)
                {
                    classStatistics.AddPropertyWeavingStatistics(propertyStatisticsFinished);
                }
            }
        }

        private static void WProcessStaticConstructor(TypeDefinition type, IWeavingContext context, ISwitchHandler switchHandler)
        {
            var ilhandler = WeavingFactory.InitializeStaticConstructorIlHandler(type, context);
        }

        private static void SetLocalParameters(MethodDefinition method, IlHandler handler, IWeavingPlan plan)
        {
            if (NeedToStoreReturnValueAsLocalVariable(method, plan))
            {
                handler.AddReturnValueVariable();
            }
            if (NeedExecutionContextParameter(method, plan))
            {
                handler.AddExecutionContextVariable();
            }
            if (plan.NeedExecutionParameter())
            {
                handler.AddExecutionParameter();
            }
            if (plan.NeedExceptionParameter())
            {
                handler.AddExceptionParameter();
            }
            if (plan.NeedReturnParameter())
            {
                handler.AddReturnParameter();
            }
            handler.FinalizeSetLocalVariableInstructions();
        }

        private static void WeaveEntryJoinPoint(IlHandler handler, IReadOnlyCollection<IAdviceInfo> advices,
            IWriteOnlySwitchHandler switchHandler, ICanAddMethodWeavingRecord statistics)
        {
            if (advices != null && advices.Any())
            {
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    handler.CallAdvice(advice, switchHandler);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Entry, advice.BuilderId, advice.Advice.GetFullName(), advice.Advice.GetSignatureWithTypeFullName(), i);
                    statistics.AddWeavingRecord(record);
                }
                handler.FinalizeWeavingEntry();
            }
        }

        private static void WeaveExceptionJoinPoint(IlHandler handler, IReadOnlyCollection<IAdviceInfo> advices,
            IWriteOnlySwitchHandler switchHandler, ICanAddMethodWeavingRecord statistics)
        {
            handler.UpdateLocalVariablesOnException();
            if (advices != null && advices.Any())
            {
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    handler.CallAdvice(advice, switchHandler);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Exception, advice.BuilderId, advice.Advice.GetFullName(), advice.Advice.GetSignatureWithTypeFullName(), i);
                    statistics.AddWeavingRecord(record);
                }
            }
            handler.FinalizeWeavingException();
        }

        private static void WeaveExitJoinPoint(IlHandler handler, IReadOnlyCollection<IAdviceInfo> advices,
            IWriteOnlySwitchHandler switchHandler, ICanAddMethodWeavingRecord statistics)
        {
            if (advices != null && advices.Any())
            {
                handler.UpdateLocalVariablesOnExit();
                // inject injectables
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    handler.CallAdvice(advice, switchHandler);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Exit, advice.BuilderId, advice.Advice.GetFullName(), advice.Advice.GetSignatureWithTypeFullName(), i);
                    statistics.AddWeavingRecord(record);
                }
            }
            handler.FinalizeWeavingExit();
        }

        private static bool NeedToStoreReturnValueAsLocalVariable(MethodDefinition method, IWeavingPlan plan)
        {
            return !method.ReturnType.FullName.Equals(typeof(void).FullName) && plan.NeedToStoreReturnValueAsLocalVariable();
        }

        private static bool NeedExecutionContextParameter(MethodDefinition method, IWeavingPlan plan)
        {
            return plan.NeedHasException() || 
                (plan.NeedReturnParameter() && NeedToStoreReturnValueAsLocalVariable(method, plan));
        }

        private static bool IsPropertyMethod(this MethodDefinition method)
        {
            return method.IsGetter || method.IsSetter;
        }
    }
}

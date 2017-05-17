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
    using Aspect;
    using Statistics;
    using Utilities;
    using Batch;

    internal static class Processor
    {
        public static IAssemblyWeavingStatistics Weave(string inputAssemblyPath, string outputAssemblyPath, IWeavingBatch batch)
        {
            var readWriteSymbols = File.Exists(Path.ChangeExtension(inputAssemblyPath, "pdb"));
            var readerParameters = new ReaderParameters
            {
                ReadSymbols = readWriteSymbols,
                SymbolReaderProvider = readWriteSymbols ? new PdbReaderProvider() : null
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
                        foreach (var property in clazz.Properties)
                        {
                            var propertyInfo = property.Convert(classCustomAttributes.AsReadOnly());
                            var plan = batch.BuildPlan(propertyInfo);
                            if (!plan.IsEmpty())
                            {
                                var propertyStatistics = StatisticsFactory.InitializePropertyWeavingRecord(property.Name, property.FullName);
                                WeaveProperty(property, plan, context, propertyStatistics);
                                var propertyStatisticsFinished = propertyStatistics.ToReadOnly();
                                if (propertyStatisticsFinished.JoinPointCount > 0)
                                {
                                    classStatistics.AddPropertyWeavingStatistics(propertyStatisticsFinished);
                                }
                            }
                        }
                        foreach (var method in clazz.Methods)
                        {
                            // getter and setter will be handled in property phase
                            if (!method.HasBody || method.IsGetter || method.IsSetter)
                            {
                                continue;
                            }
                            
                            var methodInfo = method.Convert(classCustomAttributes.AsReadOnly());
                            var plan = batch.BuildPlan(methodInfo);
                            if (!plan.IsEmpty())
                            {
                                var methodStatistics = StatisticsFactory.InitializeMethodWeavingRecord(method.Name, method.FullName);
                                WeaveMethod(method, plan, context, methodStatistics);
                                var methodStatisticsFinished = methodStatistics.ToReadOnly();
                                if(methodStatisticsFinished.JoinPointCount > 0)
                                {
                                    classStatistics.AddMethodWeavingStatistics(methodStatisticsFinished);
                                }
                            }
                        }
                        var classStatisticsFinished = classStatistics.ToReadOnly();
                        if(classStatisticsFinished.WeavedMethodPropertyCount > 0)
                        {
                            moduleStatistics.AddClassWeavingStatistics(classStatisticsFinished);
                        }
                    }
                    var moduleStatisticsFinished = moduleStatistics.ToReadOnly();
                    if(moduleStatisticsFinished.WeavedClassCount > 0)
                    {
                        assemblyStatistics.AddModuleWeavingStatistics(moduleStatisticsFinished);
                    }
                }
                var writerParameters = new WriterParameters
                {
                    WriteSymbols = readWriteSymbols,
                    SymbolWriterProvider = readWriteSymbols ? new PdbWriterProvider() : null
                };
                assembly.Write(outputAssemblyPath, writerParameters);
            }
            catch(Exception e)
            {
                assemblyStatistics.Exception = e;
            }
            return assemblyStatistics.ToReadOnly();
        }

        public static void WeaveMethod(MethodDefinition method, IWeavingPlan plan, IWeavingContext context,
                          ICanAddMethodWeavingRecord statistics)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
            var handler = WeavingFactory.InitializeIlHandler(method, context);
            SetLocalParameters(method, handler, plan);
            WeaveEntryJoinPoint(handler, plan.GetAdvices(JoinPoint.Entry), statistics);
            WeaveExceptionJoinPoint(handler, plan.GetAdvices(JoinPoint.Exception), statistics);
            WeaveExitJoinPoint(handler, plan.GetAdvices(JoinPoint.Exit), statistics);
        }

        public static void WeaveProperty(PropertyDefinition property, IPropertyWeavingPlan plan, IWeavingContext context,
                          ICanAddPropertyWeavingRecord statistics)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (plan == null)
            {
                throw new ArgumentNullException("plan");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (statistics == null)
            {
                throw new ArgumentNullException("statistics");
            }
            var getterPlan = plan.GetterPlan;
            var getter = property.GetMethod;
            if (!getterPlan.IsEmpty() && getter != null)
            {
                var handler = WeavingFactory.InitializeIlHandler(getter, context);
                SetLocalParameters(getter, handler, getterPlan);
                WeaveEntryJoinPoint(handler, getterPlan.GetAdvices(JoinPoint.Entry), statistics.GetterContainer);
                WeaveExceptionJoinPoint(handler, getterPlan.GetAdvices(JoinPoint.Exception), statistics.GetterContainer);
                WeaveExitJoinPoint(handler, getterPlan.GetAdvices(JoinPoint.Exit), statistics.GetterContainer);
            }
            var setterPlan = plan.SetterPlan;
            var setter = property.SetMethod;
            if (!setterPlan.IsEmpty() && setter != null)
            {
                var handler = WeavingFactory.InitializeIlHandler(setter, context);
                SetLocalParameters(setter, handler, setterPlan);
                WeaveEntryJoinPoint(handler, setterPlan.GetAdvices(JoinPoint.Entry), statistics.SetterContainer);
                WeaveExceptionJoinPoint(handler, setterPlan.GetAdvices(JoinPoint.Exception), statistics.SetterContainer);
                WeaveExitJoinPoint(handler, setterPlan.GetAdvices(JoinPoint.Exit), statistics.SetterContainer);
            }
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
            ICanAddMethodWeavingRecord statistics)
        {
            if (advices != null && advices.Any())
            {
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    handler.CallAdvice(advice);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Entry, advice.BuilderId, advice.Advice.GetFullName(), advice.Advice.GetSignature(), i);
                    statistics.AddWeavingRecord(record);
                }
                handler.FinalizeWeavingEntry();
            }
        }

        private static void WeaveExceptionJoinPoint(IlHandler handler, IReadOnlyCollection<IAdviceInfo> advices,
            ICanAddMethodWeavingRecord statistics)
        {
            handler.UpdateLocalVariablesOnException();
            if (advices != null && advices.Any())
            {
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    handler.CallAdvice(advice);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Exception, advice.BuilderId, advice.Advice.GetFullName(), advice.Advice.GetSignature(), i);
                    statistics.AddWeavingRecord(record);
                }
            }
            handler.FinalizeWeavingException();
        }

        private static void WeaveExitJoinPoint(IlHandler handler, IReadOnlyCollection<IAdviceInfo> advices,
            ICanAddMethodWeavingRecord statistics)
        {
            if (advices != null && advices.Any())
            {
                handler.UpdateLocalVariablesOnExit();
                // inject injectables
                for (var i = 0; i < advices.Count; i++)
                {
                    var advice = advices.ElementAt(i);
                    handler.CallAdvice(advice);
                    var record = StatisticsFactory.InitializeWeavingRecord(
                        JoinPoint.Exit, advice.BuilderId, advice.Advice.GetFullName(), advice.Advice.GetSignature(), i);
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
    }
}

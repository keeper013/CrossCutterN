/*
 * Description: adapter of AspectDc.Common.Data type methods
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference
{
    using System;
    using Mono.Cecil;
    using CrossCutterN.Advice.Common;
    using CrossCutterN.Advice.Parameter;
    using CrossCutterN.Advice.Switch;
    using Advice.Parameter;
    using Advice.Switch;

    internal static class AdviceReferenceFactory
    {
        public static IAdviceReference InitializeAdviceParameterReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }

            return new AdviceReference
                {
                    ParameterFactory = InitializeParameterFactory(module),
                    Execution = InitializeExecution(module),
                    ExecutionContext = InitializeExecutionContext(module),
                    Parameter = InitializeParameter(module),
                    CustomAttribute = InitializeCustomAttribute(module),
                    AttributeProperty = InitializeAttributeProperty(module),
                    Return = InitializeReturn(module),
                    BuildUp = InitializeBuildUp(module),
                    Controller = InitializeController(module),
                    LookUp = InitializeLookUp(module)
                };
        }

        private static IParameterFactoryReference InitializeParameterFactory(ModuleDefinition module)
        {
            const string methodInitializeExecution = "InitializeExecution";
            const string methodInitializeExecutionContext = "InitializeExecutionContext";
            const string methodInitializeParameter = "InitializeParameter";
            const string methodInitializeCustomAttribute = "InitializeCustomAttribute";
            const string methodInitializeAttributeProperty = "InitializeAttributeProperty";
            const string methodInitializeReturn = "InitializeReturn";

            var reference = new ParameterFactoryReference(module);
            var type = typeof(ParameterFactory);
            reference.InitializeAttributePropertyMethod = type.GetMethod(methodInitializeAttributeProperty);
            reference.InitializeCustomAttributeMethod = type.GetMethod(methodInitializeCustomAttribute);
            reference.InitializeExecutionContextMethod = type.GetMethod(methodInitializeExecutionContext);
            reference.InitializeExecutionMethod = type.GetMethod(methodInitializeExecution);
            reference.InitializeParameterMethod = type.GetMethod(methodInitializeParameter);
            reference.InitializeReturnMethod = type.GetMethod(methodInitializeReturn);

            return reference.Convert();
        }

        private static ICanAddParameterReference InitializeExecution(ModuleDefinition module)
        {
            const string methodAddParameter = "AddParameter";
            const string methodConvert = "Convert";
            var reference = new WriteOnlyExecutionReference(module);
            var type = typeof(ICanAddParameter);
            reference.AddParameterMethod = type.GetMethod(methodAddParameter);
            reference.ReadOnlyTypeReference = typeof(IExecution);
            reference.TypeReference = type;
            reference.ConvertMethod = typeof(ICanConvert<IExecution>).GetMethod(methodConvert);
            return reference.Convert();
        }

        private static IExecutionContextReference InitializeExecutionContext(ModuleDefinition module)
        {
            var reference = new ExecutionContextReference(module);
            var type = typeof(IExecutionContext);
            reference.TypeReference = type;
            return reference.Convert();
        }

        private static ICanAddCustomAttributeReference InitializeParameter(ModuleDefinition module)
        {
            const string methodAddCustomAttribute = "AddCustomAttribute";
            const string methodConvert = "Convert";
            var reference = new WriteOnlyParameterReference(module);
            var type = typeof(ICanAddCustomAttribute);
            reference.AddCustomAttributeMethod = type.GetMethod(methodAddCustomAttribute);
            reference.ReadOnlyTypeReference = typeof(IParameter);
            reference.ConvertMethod = typeof(ICanConvert<IParameter>).GetMethod(methodConvert);
            reference.TypeReference = type;
            return reference.Convert();
        }

        private static ICanAddAttributePropertyReference InitializeCustomAttribute(ModuleDefinition module)
        {
            const string methodAddAttributeProperty = "AddAttributeProperty";
            const string methodConvert = "Convert";
            var reference = new WriteOnlyCustomAttributeReference(module);
            var type = typeof(ICanAddAttributeProperty);
            reference.AddAttributePropertyMethod = type.GetMethod(methodAddAttributeProperty);
            reference.ReadOnlyTypeReference = typeof(CrossCutterN.Advice.Parameter.ICustomAttribute);
            reference.ConvertMethod = typeof(ICanConvert<CrossCutterN.Advice.Parameter.ICustomAttribute>).GetMethod(methodConvert);
            reference.TypeReference = type;
            return reference.Convert();
        }

        private static IAttributePropertyReference InitializeAttributeProperty(ModuleDefinition module)
        {
            var reference = new AttributePropertyReference(module);
            var type = typeof(IAttributeProperty);
            reference.TypeReference = type;
            return reference.Convert();
        }

        private static IWriteOnlyReturnReference InitializeReturn(ModuleDefinition module)
        {
            const string propertyHasReturn = "HasReturn";
            const string propertyValue = "Value";
            const string methodConvert = "Convert";
            var reference = new WriteOnlyReturnReference(module);
            var type = typeof(IWriteOnlyReturn);
            reference.HasReturnSetter = type.GetProperty(propertyHasReturn).GetSetMethod();
            reference.ReadOnlyTypeReference = typeof(IReturn);
            reference.ConvertMethod = typeof(ICanConvert<IReturn>).GetMethod(methodConvert);
            reference.TypeReference = type;
            reference.ValueSetter = type.GetProperty(propertyValue).GetSetMethod();
            return reference.Convert();
        }

        private static IAdviceSwitchBuildUpReference InitializeBuildUp(ModuleDefinition module)
        {
            const string methodRegisterSwitch = "RegisterSwitch";
            const string methodComplete = "Complete";
            var reference = new AdviceSwitchBuildUpReference(module);
            var type = typeof (IAdviceSwitchBuildUp);
            reference.CompleteMethod = type.GetMethod(methodComplete);
            reference.RegisterSwitchMethod = type.GetMethod(methodRegisterSwitch);
            return reference.Convert();
        }

        private static ISwitchBackStageReference InitializeController(ModuleDefinition module)
        {
            const string propertyLookUp = "LookUp";
            const string propertyBuildUp = "BuildUp";
            var reference = new SwitchBackStageReference(module);
            var type = typeof (SwitchBackStage);
            reference.BuildUpGetterReference = type.GetProperty(propertyBuildUp).GetMethod;
            reference.LookUpGetterReference = type.GetProperty(propertyLookUp).GetMethod;
            return reference.Convert();
        }

        private static IAdviceSwitchLookUpReference InitializeLookUp(ModuleDefinition module)
        {
            const string methodIsOn = "IsOn";
            var reference = new AdviceSwitchLookUpReference(module);
            reference.IsOnMethod = typeof (IAdviceSwitchLookUp).GetMethod(methodIsOn);
            return reference.Convert();
        }
    }
}

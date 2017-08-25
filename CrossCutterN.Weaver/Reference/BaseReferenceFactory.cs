// <copyright file="BaseReferenceFactory.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Weaver.Reference
{
    using System;
    using CrossCutterN.Base.Metadata;
    using CrossCutterN.Base.Switch;
    using CrossCutterN.Weaver.Reference.Base.Metadata;
    using CrossCutterN.Weaver.Reference.Base.Switch;
    using Mono.Cecil;

    /// <summary>
    /// Base reference factory.
    /// </summary>
    internal static class BaseReferenceFactory
    {
        /// <summary>
        /// Initializes a new instance of of <see cref="IBaseReference"/> interface.
        /// </summary>
        /// <param name="module">The current module that this <see cref="IBaseReference"/> is for.</param>
        /// <returns>The <see cref="IBaseReference"/> initialized.</returns>
        public static IBaseReference InitializeBaseReference(ModuleDefinition module)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }

            return new BaseReference
                {
                    MetadataFactory = InitializeMetadataFactory(module),
                    Execution = InitializeExecution(module),
                    ExecutionContext = InitializeExecutionContext(module),
                    Parameter = InitializeParameter(module),
                    CustomAttribute = InitializeCustomAttribute(module),
                    AttributeProperty = InitializeAttributeProperty(module),
                    Return = InitializeReturn(module),
                    Builder = InitializeBuilder(module),
                    BackStage = InitializeBackStage(module),
                    Glancer = InitializeGlancer(module),
                };
        }

        private static IMetadataFactoryReference InitializeMetadataFactory(ModuleDefinition module)
        {
            const string methodInitializeExecution = "InitializeExecution";
            const string methodInitializeExecutionContext = "InitializeExecutionContext";
            const string methodInitializeParameter = "InitializeParameter";
            const string methodInitializeCustomAttribute = "InitializeCustomAttribute";
            const string methodInitializeAttributeProperty = "InitializeAttributeProperty";
            const string methodInitializeReturn = "InitializeReturn";

            var reference = new MetadataFactoryReference(module);
            var type = typeof(MetadataFactory);
            reference.InitializeAttributePropertyMethod = type.GetMethod(methodInitializeAttributeProperty);
            reference.InitializeCustomAttributeMethod = type.GetMethod(methodInitializeCustomAttribute);
            reference.InitializeExecutionContextMethod = type.GetMethod(methodInitializeExecutionContext);
            reference.InitializeExecutionMethod = type.GetMethod(methodInitializeExecution);
            reference.InitializeParameterMethod = type.GetMethod(methodInitializeParameter);
            reference.InitializeReturnMethod = type.GetMethod(methodInitializeReturn);

            return reference.Build();
        }

        private static IExecutionBuilderReference InitializeExecution(ModuleDefinition module)
        {
            const string methodAddParameter = "AddParameter";
            const string methodBuild = "Build";
            var reference = new ExecutionBuilderReference(module);
            var type = typeof(IExecutionBuilder);
            reference.AddParameterMethod = type.GetMethod(methodAddParameter);
            reference.ReadOnlyTypeReference = typeof(IExecution);
            reference.TypeReference = type;
            reference.BuildMethod = type.GetMethod(methodBuild);
            return reference.Build();
        }

        private static IExecutionContextReference InitializeExecutionContext(ModuleDefinition module)
        {
            var reference = new ExecutionContextReference(module);
            var type = typeof(IExecutionContext);
            reference.TypeReference = type;
            return reference.Build();
        }

        private static IParameterBuilderReference InitializeParameter(ModuleDefinition module)
        {
            const string methodAddCustomAttribute = "AddCustomAttribute";
            const string methodBuild = "Build";
            var reference = new ParameterBuilderReference(module);
            var type = typeof(IParameterBuilder);
            reference.AddCustomAttributeMethod = type.GetMethod(methodAddCustomAttribute);
            reference.ReadOnlyTypeReference = typeof(IParameter);
            reference.BuildMethod = type.GetMethod(methodBuild);
            reference.TypeReference = type;
            return reference.Build();
        }

        private static ICustomAttributeBuilderReference InitializeCustomAttribute(ModuleDefinition module)
        {
            const string methodAddAttributeProperty = "AddAttributeProperty";
            const string methodBuild = "Build";
            var reference = new CustomAttributeBuilderReference(module);
            var type = typeof(ICustomAttributeBuilder);
            reference.AddAttributePropertyMethod = type.GetMethod(methodAddAttributeProperty);
            reference.ReadOnlyTypeReference = typeof(CrossCutterN.Base.Metadata.ICustomAttribute);
            reference.BuildMethod = type.GetMethod(methodBuild);
            reference.TypeReference = type;
            return reference.Build();
        }

        private static IAttributePropertyReference InitializeAttributeProperty(ModuleDefinition module)
        {
            var reference = new AttributePropertyReference(module);
            var type = typeof(IAttributeProperty);
            reference.TypeReference = type;
            return reference.Build();
        }

        private static IReturnBuilderReference InitializeReturn(ModuleDefinition module)
        {
            const string propertyHasReturn = "HasReturn";
            const string propertyValue = "Value";
            const string methodBuild = "Build";
            var reference = new ReturnBuilderReference(module);
            var type = typeof(IReturnBuilder);
            reference.HasReturnSetter = type.GetProperty(propertyHasReturn).GetSetMethod();
            reference.ReadOnlyTypeReference = typeof(IReturn);
            reference.BuildMethod = type.GetMethod(methodBuild);
            reference.TypeReference = type;
            reference.ValueSetter = type.GetProperty(propertyValue).GetSetMethod();
            return reference.Build();
        }

        private static IAspectSwitchBuilderReference InitializeBuilder(ModuleDefinition module)
        {
            const string methodRegisterSwitch = "RegisterSwitch";
            const string methodComplete = "Complete";
            var reference = new AspectSwitchBuilderReference(module);
            var type = typeof(IAspectSwitchBuilder);
            reference.CompleteMethod = type.GetMethod(methodComplete);
            reference.RegisterSwitchMethod = type.GetMethod(methodRegisterSwitch);
            return reference.Build();
        }

        private static ISwitchBackStageReference InitializeBackStage(ModuleDefinition module)
        {
            const string propertyGlancer = "Glancer";
            const string propertyBuilder = "Builder";
            var reference = new SwitchBackStageReference(module);
            var type = typeof(SwitchBackStage);
            reference.BuilderGetterReference = type.GetProperty(propertyBuilder).GetMethod;
            reference.GlancerGetterReference = type.GetProperty(propertyGlancer).GetMethod;
            return reference.Build();
        }

        private static IAspectSwitchGlancerReference InitializeGlancer(ModuleDefinition module)
        {
            const string methodIsOn = "IsOn";
            var reference = new AspectSwitchGlancerReference(module)
            {
                IsOnMethod = typeof(IAspectSwitchGlancer).GetMethod(methodIsOn),
            };
            return reference.Build();
        }
    }
}

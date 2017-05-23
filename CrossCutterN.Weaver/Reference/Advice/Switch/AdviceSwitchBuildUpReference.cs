/**
 * Description: AdviceSwitchBuildUp reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using System.Reflection;
    using Mono.Cecil;

    internal class AdviceSwitchBuildUpReference : ReferenceBase, IAdviceSwitchBuildUpReference, IAdviceSwitchBuildUpWriteOnlyReference
    {
        public AdviceSwitchBuildUpReference(ModuleDefinition module) : base(module, true)
        {
        }

        MethodReference IAdviceSwitchBuildUpReference.RegisterSwitchMethod
        {
            get { return GetMethod("RegisterSwitchMethod"); }
        }

        public MethodInfo RegisterSwitchMethod
        {
            set { SetMethod("RegisterSwitchMethod", value); }
        }

        MethodReference IAdviceSwitchBuildUpReference.CompleteMethod
        {
            get { return GetMethod("CompleteMethod"); }
        }

        public MethodInfo CompleteMethod
        {
            set { SetMethod("CompleteMethod", value); }
        }

        public IAdviceSwitchBuildUpReference Convert()
        {
            ValidateConvert("RegisterSwitchMethod", "CompleteMethod");
            return this;
        }
    }
}

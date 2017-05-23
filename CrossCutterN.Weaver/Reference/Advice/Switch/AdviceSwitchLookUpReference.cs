/**
 * Description: IAdviceSwitchLookup reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using System.Reflection;
    using Mono.Cecil;

    internal sealed class AdviceSwitchLookUpReference : ReferenceBase, IAdviceSwitchLookUpReference, IAdviceSwitchLookUpWriteOnlyReference
    {
        public AdviceSwitchLookUpReference(ModuleDefinition module) : base(module, true)
        {
        }

        MethodReference IAdviceSwitchLookUpReference.IsOnMethod
        {
            get { return GetMethod("IsOnMethod"); }
        }

        public MethodInfo IsOnMethod
        {
            set { SetMethod("IsOnMethod", value); }
        }

        public IAdviceSwitchLookUpReference Convert()
        {
            ValidateConvert("IsOnMethod");
            return this;
        }
    }
}

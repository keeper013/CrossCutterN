/**
 * Description: IAdviceSwitchController reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using System.Reflection;
    using Mono.Cecil;
    
    internal sealed class AdviceSwitchControllerReference : ReferenceBase, IAdviceSwitchControllerReference, IAdviceSwitchControllerWriteOnlyReference
    {
        public AdviceSwitchControllerReference(ModuleDefinition module) : base(module, true)
        {
        }

        MethodReference IAdviceSwitchControllerReference.LookUpGetterReference
        {
            get { return GetMethod("LookUpGetterReference"); }
        }

        public MethodInfo LookUpGetterReference
        {
            set { SetMethod("LookUpGetterReference", value); }
        }

        MethodReference IAdviceSwitchControllerReference.BuildUpGetterReference
        {
            get { return GetMethod("BuildUpGetterReference"); }
        }

        public MethodInfo BuildUpGetterReference
        {
            set { SetMethod("BuildUpGetterReference", value); }
        }

        public IAdviceSwitchControllerReference Convert()
        {
            ValidateConvert("LookUpGetterReference", "BuildUpGetterReference");
            return this;
        }
    }
}

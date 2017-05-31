/**
 * Description: IAdviceSwitchController reference implementation
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using System.Reflection;
    using Mono.Cecil;
    
    internal sealed class SwitchBackStageReference : ReferenceBase, ISwitchBackStageReference, ISwitchBackStageWriteOnlyReference
    {
        public SwitchBackStageReference(ModuleDefinition module) : base(module, true)
        {
        }

        MethodReference ISwitchBackStageReference.LookUpGetterReference
        {
            get { return GetMethod("LookUpGetterReference"); }
        }

        public MethodInfo LookUpGetterReference
        {
            set { SetMethod("LookUpGetterReference", value); }
        }

        MethodReference ISwitchBackStageReference.BuildUpGetterReference
        {
            get { return GetMethod("BuildUpGetterReference"); }
        }

        public MethodInfo BuildUpGetterReference
        {
            set { SetMethod("BuildUpGetterReference", value); }
        }

        public ISwitchBackStageReference Convert()
        {
            ValidateConvert("LookUpGetterReference", "BuildUpGetterReference");
            return this;
        }
    }
}

/**
 * Description: IAdviceSwitchBuildUp reference write only interface
 * Author: David Cui
 */

namespace CrossCutterN.Weaver.Reference.Advice.Switch
{
    using System.Reflection;
    using CrossCutterN.Advice.Common;

    internal interface IAdviceSwitchBuildUpWriteOnlyReference : ICanConvert<IAdviceSwitchBuildUpReference>
    {
        MethodInfo RegisterSwitchMethod { set; }
        MethodInfo CompleteMethod { set; }
    }
}

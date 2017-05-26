/**
* Description: switch weaving record
* Author: David Cui
*/

namespace CrossCutterN.Weaver.Statistics
{
    public interface ISwitchWeavingRecord
    {
        string Class { get; }
        string Property { get; }
        string Method { get; }
        string Aspect { get; }
        string StaticVariableName { get; }
        bool Value { get; }
    }
}

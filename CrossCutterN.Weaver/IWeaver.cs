/**
* Description: weaver interface
* Author: David Cui
*/

namespace CrossCutterN.Weaver
{
    using Statistics;

    public interface IWeaver
    {
        IAssemblyWeavingStatistics Weave(string inputAssemblyPath, string outputAssemblyPath, bool includeSymbol);
    }
}

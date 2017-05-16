/**
 * Description: read only method execution information
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Parameter
{
    using System.Collections.Generic;

    public interface IExecution
    {
        string AssemblyFullName { get; }
        string Namespace { get; }
        string ClassFullName { get; }
        string ClassName { get; }
        string FullName { get; }
        string Name { get; }
        string ReturnType { get; }
        IReadOnlyCollection<IParameter> Parameters { get; }
        IParameter GetParameter(string name);
        bool HasParameter(string name);
    }
}

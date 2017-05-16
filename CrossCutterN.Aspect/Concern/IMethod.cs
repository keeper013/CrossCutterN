/**
 * Description: readonly method interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System.Collections.Generic;

    public interface IMethod
    {
        string AssemblyFullName { get; }
        string Namespace { get; }
        string ClassFullName { get; }
        string ClassName { get; }
        string MethodFullName { get; }
        string MethodName { get; }
        string ReturnType { get; }
        bool IsInstance { get; }
        bool IsConstructor { get; }
        Accessibility Accessibility { get; }

        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { get; }
        IReadOnlyCollection<ICustomAttribute> CustomAttributes { get; }
        IReadOnlyCollection<IParameter> Parameters { get; }
        IParameter GetParameter(string name);
        bool HasParameter(string name);
    }
}

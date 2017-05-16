/**
 * Description: readonly property interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Concern
{
    using System.Collections.Generic;

    public interface IProperty
    {
        string AssemblyFullName { get; }
        string Namespace { get; }
        string ClassFullName { get; }
        string ClassName { get; }
        string PropertyFullName { get; }
        string PropertyName { get; }
        string Type { get; }
        bool IsInstance { get; }
        Accessibility? GetterAccessibility { get; }
        Accessibility? SetterAccessibility { get; }

        IReadOnlyCollection<ICustomAttribute> ClassCustomAttributes { get; }
        IReadOnlyCollection<ICustomAttribute> CustomAttributes { get; }
        IReadOnlyCollection<ICustomAttribute> GetterCustomAttributes { get; }
        IReadOnlyCollection<ICustomAttribute> SetterCustomAttributes { get; }
    }
}

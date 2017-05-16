/**
 * Description: Join point enumeration
 * Author: David Cui
 */

namespace CrossCutterN.Aspect
{
    /// <summary>
    /// Thoughts about extensibility for using enumeration:
    /// Weaving for each join point type is fixed and happens in weaver, which can't be open for extension
    /// Even validation must happen in weaver because validation without weaving process doesn't make sense
    /// So extending join point will cause core business of CrossCutterN to be updated
    /// Then users meant to extend from CrossCutterN should be able to add new join points
    /// So we use non-extendable enumeration as interface to extending users to reduce complexity of the code
    /// </summary>
    public enum JoinPoint
    {
        Entry,
        Exception,
        Exit
    }
}

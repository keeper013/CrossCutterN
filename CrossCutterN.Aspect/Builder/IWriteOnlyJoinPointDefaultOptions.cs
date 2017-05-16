/**
 * Description: Write only flagged aspect builder interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Builder
{
    /// <summary>
    /// These flags are default behaviors which may be overwritten by implementation
    /// </summary>
    public interface IWriteOnlyJoinPointDefaultOptions
    {
        bool ConcernConstructor { set; }
        bool ConcernInstance { set; }
        bool ConcernInternal { set; }
        bool ConcernMethod { set; }
        bool ConcernPropertyGetter { set; }
        bool ConcernPropertySetter { set; }
        bool ConcernPrivate { set; }
        bool ConcernProtected { set; }
        bool ConcernPublic { set; }
        bool ConcernStatic { set; }
    }
}

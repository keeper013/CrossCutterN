/**
 * Description: Write only concern attribute aspect builder with default options interface
 * Author: David Cui
 */

namespace CrossCutterN.Aspect.Builder
{
    public interface IWriteOnlyConcernAttributeAspectBuilder : IWriteOnlyAspectBuilder, IWriteOnlyJoinPointDefaultOptions
    {
        bool PointCutAtEntry { set; }
        bool PointCutAtException { set; }
        bool PointCutAtExit { set; }
    }
}

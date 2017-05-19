/**
 * Description: trace property attribute
 * Author: David Cui
 */
namespace CrossCutterN.Advice.Concern
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class PropertyConcernAttribute : Attribute
    {
        public static readonly string PointCutAtEntryPropertyName = "PointCutAtEntry";
        public static readonly string PointCutAtExceptionPropertyName = "PointCutAtException";
        public static readonly string PointCutAtExitPropertyName = "PointCutAtExit";
        public static readonly string ConcernGetterPropertyName = "ConcernGetter";
        public static readonly string ConcernSetterPropertyName = "ConcernSetter";

        public bool PointCutAtEntry { get; set; }
        public bool PointCutAtException { get; set; }
        public bool PointCutAtExit { get; set; }
        public bool ConcernGetter { get; set; }
        public bool ConcernSetter { get; set; }
    }
}

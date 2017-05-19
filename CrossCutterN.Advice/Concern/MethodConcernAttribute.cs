/**
 * Description: Attribute for trace
 * Author: David Cui
 */

namespace CrossCutterN.Advice.Concern
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class MethodConcernAttribute : Attribute
    {
        public static readonly string PointCutAtEntryPropertyName = "PointCutAtEntry";
        public static readonly string PointCutAtExceptionPropertyName = "PointCutAtException";
        public static readonly string PointCutAtExitPropertyName = "PointCutAtExit";

        public bool PointCutAtEntry { get; set; }
        public bool PointCutAtException { get; set; }
        public bool PointCutAtExit { get; set; }
    }
}

/**
 * Description: Attribute for logging
 */

namespace CrossCutterN.Concern.Attribute
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public abstract class ClassConcernAttribute : Attribute
    {
        public static readonly string ConcernPropertyGetterPropertyName = "ConcernPropertyGetter";
        public static readonly string ConcernPropertySetterPropertyName = "ConcernPropertySetter";
        public static readonly string ConcernMethodPropertyName = "ConcernMethod";
        public static readonly string ConcernConstructorPropertyName = "ConcernConstructor";
        public static readonly string ConcernPublicPropertyName = "ConcernPublic";
        public static readonly string ConcernPrivatePropertyName = "ConcernPrivate";
        public static readonly string ConcernProtectedPropertyName = "ConcernProtected";
        public static readonly string ConcernInternalPropertyName = "ConcernInternal";
        public static readonly string ConcernStaticPropertyName = "ConcernStatic";
        public static readonly string ConcernInstancePropertyName = "ConcernInstance";
        public static readonly string PointCutAtEntryPropertyName = "PointCutAtEntry";
        public static readonly string PointCutAtExceptionPropertyName = "PointCutAtException";
        public static readonly string PointCutAtExitPropertyName = "PointCutAtExit";

        public bool ConcernPropertyGetter { get; set; }
        public bool ConcernPropertySetter { get; set; }
        public bool ConcernMethod { get; set; }
        public bool ConcernConstructor { get; set; }
        public bool ConcernPublic { get; set; }
        public bool ConcernPrivate { get; set; }
        public bool ConcernProtected { get; set; }
        public bool ConcernInternal { get; set; }
        public bool ConcernStatic { get; set; }
        public bool ConcernInstance { get; set; }
        public bool PointCutAtEntry { get; set; }
        public bool PointCutAtException { get; set; }
        public bool PointCutAtExit { get; set; }
    }
}

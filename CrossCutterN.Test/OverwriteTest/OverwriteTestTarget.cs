/**
 * Description: Overwrite Test Targets
 * Author: David Cui
 */

namespace CrossCutterN.Test.OverwriteTest
{
    using System;

    [OverwriteConcernClass]
    internal class OverwriteTestTarget
    {
        private static int _value;

        internal OverwriteTestTarget()
        {
            OverwriteByPropertyConcern = 1;
        }

        [OverwriteNoConcern]
        public void NoConcernMethod()
        {
            Console.Out.WriteLine("1");
        }

        [OverwriteConcernMethod]
        public static void OverwriteByMethodConcern()
        {
            Console.Out.WriteLine(OverwriteByPropertyConcern);
        }

        [OverwriteConcernProperty(ConcernGetter = true, ConcernSetter = true)]
        private static int OverwriteByPropertyConcern
        {
            get { return _value; }
            [OverwriteNoConcern]
            set { _value = value; }
        }

        [OverwriteConcernMethod(PointCutAtEntry = false, PointCutAtExit = false)]
        public void ThrowException()
        {
            throw new Exception();
        }
    }

    internal class OverwriteTestClassNotMarked
    {
        [OverwriteConcernMethod]
        internal void InternalMethodConceredByAttribute()
        {
            Console.Out.WriteLine(InternalProperty);
        }

        [OverwriteConcernProperty(ConcernGetter = true, ConcernSetter = false)]
        internal int InternalProperty { get; set; }

        [OverwriteConcernMethod(PointCutAtEntry = false, PointCutAtException = false, PointCutAtExit = true)]
        internal void OnlyExit()
        {
            
        }
    }

    [OverwriteConcernClass(ConcernPropertyGetter = true, ConcernMethod = false, ConcernInternal = true)]
    internal class OverwriteTestClassPropertyConcerned
    {
        internal int InernalProperty { get; set; }

        public void NotConcernedMethod()
        {
            
        }
    }
}

/**
 * Description: attribute target entry
 * Author: David Cui
 */

namespace CrossCutterN.SampleTarget
{
    using System;

    class Target
    {
        [SampleAdvice.SampleConcernMethod]
        public static int Add(int x, int y)
        {
            Console.Out.WriteLine("Add starting");
            var z = x + y;
            Console.Out.WriteLine("Add ending");
            return z;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //CrossCutterN.Advice.Switch.SwitchFacade.Controller.SwitchOn(
            //  "AspectInjectedByAttributeExample");
            Target.Add(1, 2);
        }
    }
}

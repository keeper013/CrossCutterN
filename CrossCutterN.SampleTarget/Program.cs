/**
 * Description: attribute target entry
 * Author: David Cui
 */

namespace CrossCutterN.SampleTarget
{
    using System;
    using SampleAdvice;

    class Target
    {
        [SampleConcernMethod]
        public static int Add1(int x, int y)
        {
            Console.Out.WriteLine("Inside Add1, starting");
            var z = x + y;
            Console.Out.WriteLine("Inside Add1, ending");
            return z;
        }

        public static int Add2(int x, int y)
        {
            Console.Out.WriteLine("Inside Add2, starting");
            var z = x + y;
            Console.Out.WriteLine("Inside Add2, ending");
            return z;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Target.Add1(1, 2);
            Target.Add2(1, 2);
        }
    }
}

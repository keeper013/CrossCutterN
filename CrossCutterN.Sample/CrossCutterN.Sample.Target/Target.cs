namespace CrossCutterN.Sample.Target
{
    using System;

    internal class Target
    {
        [CrossCutterN.Sample.Advice.SampleConcernMethod]
        public static int Add(int x, int y)
        {
            Console.Out.WriteLine("Add starting");
            var z = x + y;
            Console.Out.WriteLine("Add ending");
            return z;
        }
    }
}

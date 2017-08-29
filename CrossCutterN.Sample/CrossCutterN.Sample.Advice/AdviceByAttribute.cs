namespace CrossCutterN.Sample.Advice
{
    using System;
    using CrossCutterN.Base.Concern;
    using CrossCutterN.Base.Metadata;

    public static class AdviceByAttribute
    {
        public static void OnEntry(IExecution execution) 
            => Console.Out.WriteLine($"{Utility.CurrentTime} Injected by attribute on entry: {Utility.GetMethodInfo(execution)}");

        public static void OnExit(IReturn rReturn) 
            => Console.Out.WriteLine($"{Utility.CurrentTime} Injected by attribute on exit: {Utility.GetReturnInfo(rReturn)}");
    }

    public sealed class SampleConcernMethodAttribute : ConcernMethodAttribute
    {
    }
}

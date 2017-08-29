namespace CrossCutterN.Sample.Advice
{
    using System;
    using CrossCutterN.Base.Metadata;

    public static class AdviceByNameExpression
    {
        public static void OnEntry(IExecution execution)
            => Console.Out.WriteLine($"{Utility.CurrentTime} Injected by method name on entry: {Utility.GetMethodInfo(execution)}");

        public static void OnExit(IReturn rReturn)
            => Console.Out.WriteLine($"{Utility.CurrentTime} Injected by method name on exit: {Utility.GetReturnInfo(rReturn)}");
    }
}

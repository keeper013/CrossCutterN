namespace CrossCutterN.Sample.Advice
{
    using System;
    using System.Text;
    using CrossCutterN.Base.Metadata;

    public static class Advices
    {
        public static void InjectByAttributeOnEntry(IExecution execution) => Console.Out.WriteLine($"{DateTime.Now.ToString("yyyy - MM - dd hh: mm:ss.fff tt")} Injected by attribute on entry: {GetMethodInfo(execution)}");

        public static void InjectByAttributeOnExit(IReturn rReturn) => Console.Out.WriteLine($"{DateTime.Now.ToString("yyyy - MM - dd hh: mm:ss.fff tt")} Injected by attribute on exit: {GetReturnInfo(rReturn)}");

        public static void InjectByMethodNameOnEntry(IExecution execution) => Console.Out.WriteLine($"{DateTime.Now.ToString("yyyy - MM - dd hh: mm:ss.fff tt")} Injected by method name on entry: {GetMethodInfo(execution)}");

        public static void InjectByMethodNameOnExit(IReturn rReturn) => Console.Out.WriteLine($"{DateTime.Now.ToString("yyyy - MM - dd hh: mm:ss.fff tt")} Injected by method name on exit: {GetReturnInfo(rReturn)}");

        private static string GetMethodInfo(IExecution execution)
        {
            var strb = new StringBuilder(execution.Name);
            strb.Append("(");
            if (execution.Parameters.Count > 0)
            {
                foreach (var parameter in execution.Parameters)
                {
                    strb.Append(parameter.Name).Append("=").Append(parameter.Value).Append(",");
                }

                strb.Remove(strb.Length - 1, 1);
            }

            strb.Append(")");
            return strb.ToString();
        }

        private static string GetReturnInfo(IReturn rReturn) => rReturn.HasReturn ? $"returns {rReturn.Value}" : "no return";
    }
}

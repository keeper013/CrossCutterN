/**
 * Description: Sample advice and attribute
 * Author: David Cui
 */

namespace CrossCutterN.SampleAdvice
{
    using System;
    using System.Text;
    using Advice.Parameter;
    using Advice.Concern;

    public sealed class SampleConcernMethodAttribute : MethodConcernAttribute
    {
    }

    public static class Advices
    {
        public static void InjectByAttributeOnEntry(IExecution execution)
        {
            Console.Out.WriteLine("{0} Injected by attribute on entry: {1}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetMethodInfo(execution));
        }

        public static void InjectByAttributeOnExit(IReturn rReturn)
        {
            Console.Out.WriteLine("{0} Injected by attribute on exit: {1}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetReturnInfo(rReturn));
        }

        public static void InjectByMethodNameOnEntry(IExecution execution)
        {
            Console.Out.WriteLine("{0} Injected by method name on entry: {1}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetMethodInfo(execution));
        }

        public static void InjectByMethodNameOnExit(IReturn rReturn)
        {
            Console.Out.WriteLine("{0} Injected by method name on exit: {1}",
                DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), GetReturnInfo(rReturn));
        }

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

        private static string GetReturnInfo(IReturn rReturn)
        {
            return rReturn.HasReturn ? string.Format("returns {0}", rReturn.Value) : "no return";
        }
    }
}

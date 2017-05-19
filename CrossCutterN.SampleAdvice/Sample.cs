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
        public static void OnEntry(IExecution execution)
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
            Console.Out.WriteLine("Entry at {0}: {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), strb);
        }

        public static void OnExit(IReturn rReturn)
        {
            if (rReturn.HasReturn)
            {
                Console.Out.WriteLine("Exit at {0}: returns {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"), rReturn.Value);
            }
            else
            {
                Console.Out.WriteLine("Exit at {0}: no return", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt"));
            }
        }
    }
}

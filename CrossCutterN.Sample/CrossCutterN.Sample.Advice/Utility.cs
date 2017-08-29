namespace CrossCutterN.Sample.Advice
{
    using System;
    using System.Text;
    using CrossCutterN.Base.Metadata;

    internal sealed class Utility
    {
        internal static string CurrentTime => DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff tt");

        internal static string GetMethodInfo(IExecution execution)
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

        internal static string GetReturnInfo(IReturn rReturn) 
            => rReturn.HasReturn ? $"returns {rReturn.Value}" : "no return";
    }
}

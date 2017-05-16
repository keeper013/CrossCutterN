/**
 * Description: Method advice container
 * Author: David Cui
 */

namespace CrossCutterN.Test.Utilities
{
    using System.Collections.Generic;

    internal static class MethodAdviceContainer
    {
        private static readonly List<MethodAdviceRecord> Records = new List<MethodAdviceRecord>();

        public static IReadOnlyCollection<MethodAdviceRecord> Content
        {
            get { return Records.AsReadOnly(); }
        } 

        public static void Add(MethodAdviceRecord record)
        {
            Records.Add(record);
        }

        public static void Clear()
        {
            Records.Clear();
        }
    }
}

// <copyright file="MethodAdviceContainer.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.Utilities
{
    using System.Collections.Generic;

    /// <summary>
    /// Method advice container to record and search advice call records.
    /// </summary>
    internal static class MethodAdviceContainer
    {
        private const string Null = "null";
        private const string HasContext = "HasContext";
        private static readonly List<MethodAdviceRecord> Records = new List<MethodAdviceRecord>();

        /// <summary>
        /// Gets recorded advice calling records.
        /// </summary>
        public static IReadOnlyCollection<MethodAdviceRecord> Content => Records.AsReadOnly();

        /// <summary>
        /// Prints all advice call method.
        /// </summary>
        /// <param name="writer">Text writer.</param>
        public static void PrintContent(System.IO.TextWriter writer)
        {
            int count = Records.Count;
            writer.WriteLine($"Number of records: {count}.");
            if (count > 0)
            {
                for (var i = 0; i < count; i++)
                {
                    var item = Records[i];
                    var name = item.Name;
                    var context = item.Context == null ? Null : HasContext;
                    var method = item.Execution == null ? Null : item.Execution.Name;
                    var exception = item.Exception == null ? Null : item.Exception.Message;
                    var rtn = item.Return == null ? Null : item.Return.Value;
                    var hasException = item.HasException;
                    writer.WriteLine($"({name}, {method}, {rtn}, {hasException}, {context}, {exception})");
                }
            }
        }

        /// <summary>
        /// Adds an advice calling record.
        /// </summary>
        /// <param name="record">The advice calling record to be added.</param>
        public static void Add(MethodAdviceRecord record)
        {
            Records.Add(record);
        }

        /// <summary>
        /// Clears all advice calling records.
        /// </summary>
        public static void Clear()
        {
            Records.Clear();
        }
    }
}

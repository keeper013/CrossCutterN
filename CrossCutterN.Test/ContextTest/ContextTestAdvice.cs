// <copyright file="ContextTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.ContextTest
{
    using System;
    using CrossCutterN.Base.Metadata;
    using Utilities;

    /// <summary>
    /// Advices for <see cref="IExecutionContext"/> parameter test.
    /// </summary>
    public static class ContextTestAdvice
    {
        /// <summary>
        /// Entry with <see cref="IExecutionContext"/> parameter.
        /// </summary>
        /// <param name="context"><see cref="IExecutionContext"/> parameter.</param>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Entry1(IExecutionContext context, IExecution execution)
        {
            context.Set("Entry1", new TestObj { Value1 = 1, Value2 = "Entry1" });
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry1", context, execution, null, null, null));
        }

        /// <summary>
        /// Exception with <see cref="IExecutionContext"/> parameter.
        /// </summary>
        /// <param name="context"><see cref="IExecutionContext"/> parameter.</param>
        /// <param name="e">System.Exception parameter.</param>
        public static void Exception1(IExecutionContext context, Exception e)
        {
            if (context.Exist("Entry1"))
            {
                context.Remove("Entry1");
            }

            context.Set("Exception1", new TestObj { Value1 = 100, Value2 = "Exception1" });
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exception1", context, null, e, null, null));
        }

        /// <summary>
        /// Exit with <see cref="IExecutionContext"/> parameter.
        /// </summary>
        /// <param name="context"><see cref="IExecutionContext"/> parameter.</param>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void Exit1(IExecutionContext context, IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit1", context, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Entry without <see cref="IExecutionContext"/> parameter.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Entry2(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry2", null, execution, null, null, null));
        }

        /// <summary>
        /// Exception without <see cref="IExecutionContext"/> parameter.
        /// </summary>
        /// <param name="e">System.Exception parameter.</param>
        public static void Exception2(Exception e)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exception2", null, null, e, null, null));
        }

        /// <summary>
        /// Exit without <see cref="IExecutionContext"/> parameter.
        /// </summary>
        /// <param name="hasException">HasException parameter.</param>
        public static void Exit2(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit2", null, null, null, null, hasException));
        }
    }
}

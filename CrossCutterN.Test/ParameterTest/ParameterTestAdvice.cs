// <copyright file="ParameterTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.ParameterTest
{
    using System;
    using CrossCutterN.Base.Metadata;
    using Utilities;

    /// <summary>
    /// Advices for parameter test.
    /// </summary>
    internal static class ParameterTestAdvice
    {
        /// <summary>
        /// Advice to be injected at entry join point, no parameters.
        /// </summary>
        public static void EntryEmpty()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryEmpty", null, null, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at entry join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void EntryExecution(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryEmpty", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point, no parameters.
        /// </summary>
        public static void ExceptionEmpty()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, null, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void ExceptionExecution(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="e">System.Exception parameter.</param>
        public static void ExceptionException(Exception e)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, null, e, null, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="exception">System.Exception parameter.</param>
        public static void ExceptionExecutionException(IExecution execution, Exception exception)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExceptionEmpty", null, execution, exception, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        public static void ExitEmpty()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitEmpty", null, null, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void ExitExecution(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        public static void ExitReturn(IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, null, null, rtn, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="hasException">HasException parameter.</param>
        public static void ExitHasException(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, null, null, null, hasException));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        public static void ExitExecutionReturn(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, rtn, null));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void ExitExecutionHasException(IExecution execution, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, null, hasException));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void ExitReturnHasException(IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, null, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at exception join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void ExitExecutionReturnHasException(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitExecution", null, execution, null, rtn, hasException));
        }
    }
}

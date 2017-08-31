// <copyright file="SwitchTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    using CrossCutterN.Base.Metadata;
    using Utilities;

    /// <summary>
    /// Switch test advices.
    /// </summary>
    internal static class SwitchTestAdvice
    {
        /// <summary>
        /// Advice to be injected at entry join point.
        /// </summary>
        /// <param name="context"><see cref="IExecutionContext"/> parameter.</param>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Entry1(IExecutionContext context, IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry1", context, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at entry join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Entry2(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry2", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at entry join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Entry3(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry3", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="context"><see cref="IExecutionContext"/> parameter.</param>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        public static void Exit1(IExecutionContext context, IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit1", context, execution, null, rtn, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        public static void Exit2(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit2", null, execution, null, rtn, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        public static void Exit3(IExecution execution, IReturn rtn)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit3", null, execution, null, rtn, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// /// <param name="hasException">HasException parameter.</param>
        public static void Exit4(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit4", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="hasException">HasException parameter.</param>
        public static void Exit5(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit5", null, null, null, null, hasException));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Entry6(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry6", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="hasException">HasException parameter.</param>
        public static void Exit6(bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit6", null, null, null, null, hasException));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        public static void Entry7()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry7", null, null, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void Exit7(IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit7", null, null, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Entry8(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Entry8", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void Exit8(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit8", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point.
        /// </summary>
        /// <param name="context"><see cref="IExecutionContext"/> parameter.</param>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// /// <param name="hasException">HasException parameter.</param>
        public static void Exit9(IExecutionContext context, IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("Exit9", null, execution, null, rtn, hasException));
        }
    }
}

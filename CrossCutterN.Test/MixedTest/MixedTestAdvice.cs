// <copyright file="MixedTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.MixedTest
{
    using CrossCutterN.Base.Metadata;
    using Utilities;

    /// <summary>
    /// Advices for mixed aspect test.
    /// </summary>
    internal static class MixedTestAdvice
    {
        /// <summary>
        /// Advice supposed to be injected at entry join point by <see cref="MixedConcernMethod1Attribute"/>.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void EntryByAttribute1(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryByAttribute1", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice supposed to be injected at entry join point by <see cref="MixedConcernMethod2Attribute"/>.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void EntryByAttribute2(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryByAttribute2", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice supposed to be injected at entry join point by <see cref="MixedConcernClass3Attribute"/>.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void EntryByAttribute3(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("EntryByAttribute3", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice supposed to be injected at exit join point by <see cref="MixedConcernMethod1Attribute"/>.
        /// </summary>
        public static void ExitByAttribute1()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByAttribute1", null, null, null, null, null));
        }

        /// <summary>
        /// Advice supposed to be injected at exit join point by <see cref="MixedConcernMethod2Attribute"/>.
        /// </summary>
        public static void ExitByAttribute2()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByAttribute2", null, null, null, null, null));
        }

        /// <summary>
        /// Advice supposed to be injected at exit join point by <see cref="MixedConcernClass3Attribute"/>.
        /// </summary>
        public static void ExitByAttribute3()
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByAttribute3", null, null, null, null, null));
        }

        /// <summary>
        /// Advice supposed to be injected at exit join point by name expression.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void ExitByNameExpression(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ExitByNameExpression", null, execution, null, null, null));
        }
    }
}

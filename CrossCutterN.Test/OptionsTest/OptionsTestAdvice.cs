// <copyright file="OptionsTestAdvice.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OptionsTest
{
    using CrossCutterN.Base.Metadata;
    using Utilities;

    /// <summary>
    /// Advices for options test.
    /// </summary>
    internal static class OptionsTestAdvice
    {
        /// <summary>
        /// Advice to be injected at entry join point to public methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void PublicEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PublicEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to public methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void PublicExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PublicExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to protected methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void ProtectedEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ProtectedEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to protected methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void ProtectedExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ProtectedExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to internal methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void InternalEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InternalEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to internal methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void InternalExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InternalExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to private methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void PrivateEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PrivateEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to private methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void PrivateExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PrivateExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to instance methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void InstanceEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InstanceEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to instance methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void InstanceExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InstanceExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to static methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void StaticEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("StaticEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to static methods and properties.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void StaticExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("StaticExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to methods.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void MethodEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("MethodEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to methods.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void MethodExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("MethodExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to property getters.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void PropertyGetterEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertyGetterEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to property getters.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void PropertyGetterExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertyGetterExit", null, execution, null, rtn, hasException));
        }

        /// <summary>
        /// Advice to be injected at entry join point to property setters.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        public static void PropertySetterEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertySetterEntry", null, execution, null, null, null));
        }

        /// <summary>
        /// Advice to be injected at exit join point to property setters.
        /// </summary>
        /// <param name="execution"><see cref="IExecution"/> parameter.</param>
        /// <param name="rtn"><see cref="IReturn"/> parameter.</param>
        /// <param name="hasException">HasException parameter.</param>
        public static void PropertySetterExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertySetterExit", null, execution, null, rtn, hasException));
        }
    }
}

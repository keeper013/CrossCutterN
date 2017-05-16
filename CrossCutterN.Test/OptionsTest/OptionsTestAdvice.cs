/**
 * Description: OptionsTestAdvice
 * Author: David Cui
 */

namespace CrossCutterN.Test.OptionsTest
{
    using Advice.Parameter;
    using Utilities;

    internal static class OptionsTestAdvice
    {
        public static void PublicEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PublicEntry", execution, null, null, null));
        }

        public static void PublicExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PublicExit", execution, null, rtn, hasException));
        }

        public static void ProtectedEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ProtectedEntry", execution, null, null, null));
        }

        public static void ProtectedExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ProtectedExit", execution, null, rtn, hasException));
        }

        public static void InternalEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InternalEntry", execution, null, null, null));
        }

        public static void InternalExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InternalExit", execution, null, rtn, hasException));
        }

        public static void PrivateEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PrivateEntry", execution, null, null, null));
        }

        public static void PrivateExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PrivateExit", execution, null, rtn, hasException));
        }

        public static void InstanceEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InstanceEntry", execution, null, null, null));
        }

        public static void InstanceExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InstanceExit", execution, null, rtn, hasException));
        }

        public static void StaticEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("StaticEntry", execution, null, null, null));
        }

        public static void StaticExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("StaticExit", execution, null, rtn, hasException));
        }

        public static void MethodEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("MethodEntry", execution, null, null, null));
        }

        public static void MethodExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("MethodExit", execution, null, rtn, hasException));
        }

        public static void PropertyGetterEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertyGetterEntry", execution, null, null, null));
        }

        public static void PropertyGetterExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertyGetterExit", execution, null, rtn, hasException));
        }

        public static void PropertySetterEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertySetterEntry", execution, null, null, null));
        }

        public static void PropertySetterExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertySetterExit", execution, null, rtn, hasException));
        }
    }
}

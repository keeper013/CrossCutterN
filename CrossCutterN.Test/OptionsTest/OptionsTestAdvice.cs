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
            MethodAdviceContainer.Add(new MethodAdviceRecord("PublicEntry", null, execution, null, null, null));
        }

        public static void PublicExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PublicExit", null, execution, null, rtn, hasException));
        }

        public static void ProtectedEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ProtectedEntry", null, execution, null, null, null));
        }

        public static void ProtectedExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("ProtectedExit", null, execution, null, rtn, hasException));
        }

        public static void InternalEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InternalEntry", null, execution, null, null, null));
        }

        public static void InternalExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InternalExit", null, execution, null, rtn, hasException));
        }

        public static void PrivateEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PrivateEntry", null, execution, null, null, null));
        }

        public static void PrivateExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PrivateExit", null, execution, null, rtn, hasException));
        }

        public static void InstanceEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InstanceEntry", null, execution, null, null, null));
        }

        public static void InstanceExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("InstanceExit", null, execution, null, rtn, hasException));
        }

        public static void StaticEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("StaticEntry", null, execution, null, null, null));
        }

        public static void StaticExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("StaticExit", null, execution, null, rtn, hasException));
        }

        public static void MethodEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("MethodEntry", null, execution, null, null, null));
        }

        public static void MethodExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("MethodExit", null, execution, null, rtn, hasException));
        }

        public static void PropertyGetterEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertyGetterEntry", null, execution, null, null, null));
        }

        public static void PropertyGetterExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertyGetterExit", null, execution, null, rtn, hasException));
        }

        public static void PropertySetterEntry(IExecution execution)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertySetterEntry", null, execution, null, null, null));
        }

        public static void PropertySetterExit(IExecution execution, IReturn rtn, bool hasException)
        {
            MethodAdviceContainer.Add(new MethodAdviceRecord("PropertySetterExit", null, execution, null, rtn, hasException));
        }
    }
}

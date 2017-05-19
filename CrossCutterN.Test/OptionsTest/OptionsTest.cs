/**
 * Description: test cases
 * Author: David Cui
 */

namespace CrossCutterN.Test.OptionsTest
{
    using System;
    using System.Text;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    [TestFixture]
    public class OptionsTest
    {
        // "Aa" is a work around to make sure that this test runs first
        // Because this test contains static constructor triggering test
        [Test]
        public void TestAaConstructor()
        {
            MethodAdviceContainer.Clear();
            var derived = new OptionsTestTargetDerived();
            Console.Out.WriteLine(derived);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(18, content.Count);
            // private static constructor
            Assert.AreEqual("PrivateEntry", content.ElementAt(0).Name);
            Assert.AreEqual(".cctor", content.ElementAt(0).Execution.Name);
            Assert.AreEqual("StaticEntry", content.ElementAt(1).Name);
            Assert.AreEqual(".cctor", content.ElementAt(1).Execution.Name);
            Assert.AreEqual("MethodEntry", content.ElementAt(2).Name);
            Assert.AreEqual(".cctor", content.ElementAt(2).Execution.Name);
            Assert.AreEqual("MethodExit", content.ElementAt(3).Name);
            Assert.AreEqual(".cctor", content.ElementAt(3).Execution.Name);
            Assert.AreEqual("StaticExit", content.ElementAt(4).Name);
            Assert.AreEqual(".cctor", content.ElementAt(4).Execution.Name);
            Assert.AreEqual("PrivateExit", content.ElementAt(5).Name);
            Assert.AreEqual(".cctor", content.ElementAt(5).Execution.Name);
            Assert.AreEqual("ProtectedEntry", content.ElementAt(6).Name);
            Assert.AreEqual(".ctor", content.ElementAt(6).Execution.Name);
            Assert.AreEqual("InstanceEntry", content.ElementAt(7).Name);
            Assert.AreEqual(".ctor", content.ElementAt(7).Execution.Name);
            Assert.AreEqual("MethodEntry", content.ElementAt(8).Name);
            Assert.AreEqual(".ctor", content.ElementAt(8).Execution.Name);
            Assert.AreEqual("InternalEntry", content.ElementAt(9).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(9).Execution.Name);
            Assert.AreEqual("InstanceEntry", content.ElementAt(10).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(10).Execution.Name);
            Assert.AreEqual("PropertySetterEntry", content.ElementAt(11).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(11).Execution.Name);
            Assert.AreEqual("PropertySetterExit", content.ElementAt(12).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(12).Execution.Name);
            Assert.AreEqual("InstanceExit", content.ElementAt(13).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(13).Execution.Name);
            Assert.AreEqual("InternalExit", content.ElementAt(14).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(14).Execution.Name);
            Assert.AreEqual("MethodExit", content.ElementAt(15).Name);
            Assert.AreEqual(".ctor", content.ElementAt(15).Execution.Name);
            Assert.AreEqual("InstanceExit", content.ElementAt(16).Name);
            Assert.AreEqual(".ctor", content.ElementAt(16).Execution.Name);
            Assert.AreEqual("ProtectedExit", content.ElementAt(17).Name);
            Assert.AreEqual(".ctor", content.ElementAt(17).Execution.Name);
        }

        [Test]
        public void TestInternalProperty()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.InternalInstanceFunc(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(12, content.Count);
            Assert.AreEqual("InternalEntry", content.ElementAt(0).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(0).Execution.Name);
            Assert.AreEqual("InstanceEntry", content.ElementAt(1).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(1).Execution.Name);
            Assert.AreEqual("PropertyGetterEntry", content.ElementAt(2).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(2).Execution.Name);
            Assert.AreEqual("PropertyGetterExit", content.ElementAt(3).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(3).Execution.Name);
            Assert.AreEqual("InstanceExit", content.ElementAt(4).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(4).Execution.Name);
            Assert.AreEqual("InternalExit", content.ElementAt(5).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(5).Execution.Name);
            Assert.AreEqual("PrivateEntry", content.ElementAt(6).Name);
            Assert.AreEqual("Square", content.ElementAt(6).Execution.Name);
            Assert.AreEqual("StaticEntry", content.ElementAt(7).Name);
            Assert.AreEqual("Square", content.ElementAt(7).Execution.Name);
            Assert.AreEqual("MethodEntry", content.ElementAt(8).Name);
            Assert.AreEqual("Square", content.ElementAt(8).Execution.Name);
            Assert.AreEqual("MethodExit", content.ElementAt(9).Name);
            Assert.AreEqual("Square", content.ElementAt(9).Execution.Name);
            Assert.AreEqual("StaticExit", content.ElementAt(10).Name);
            Assert.AreEqual("Square", content.ElementAt(10).Execution.Name);
            Assert.AreEqual("PrivateExit", content.ElementAt(11).Name);
            Assert.AreEqual("Square", content.ElementAt(11).Execution.Name);
        }

        [Test]
        public void TestInternalFunction()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            var strb = new StringBuilder();
            derived.InternalMultipleParameter(ref strb, 1, "str", null);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(12, content.Count);
            Assert.AreEqual("InternalEntry", content.ElementAt(0).Name);
            Assert.AreEqual("InternalMultipleParameter", content.ElementAt(0).Execution.Name);
            Assert.AreEqual("InstanceEntry", content.ElementAt(1).Name);
            Assert.AreEqual("InternalMultipleParameter", content.ElementAt(1).Execution.Name);
            Assert.AreEqual("MethodEntry", content.ElementAt(2).Name);
            Assert.AreEqual("InternalMultipleParameter", content.ElementAt(2).Execution.Name);
            Assert.AreEqual("InternalEntry", content.ElementAt(3).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(3).Execution.Name);
            Assert.AreEqual("InstanceEntry", content.ElementAt(4).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(4).Execution.Name);
            Assert.AreEqual("PropertyGetterEntry", content.ElementAt(5).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(5).Execution.Name);
            Assert.AreEqual("PropertyGetterExit", content.ElementAt(6).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(6).Execution.Name);
            Assert.AreEqual("InstanceExit", content.ElementAt(7).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(7).Execution.Name);
            Assert.AreEqual("InternalExit", content.ElementAt(8).Name);
            Assert.AreEqual("get_InternalInstanceFunc", content.ElementAt(8).Execution.Name);
            Assert.AreEqual("MethodExit", content.ElementAt(9).Name);
            Assert.AreEqual("InternalMultipleParameter", content.ElementAt(9).Execution.Name);
            Assert.AreEqual("InstanceExit", content.ElementAt(10).Name);
            Assert.AreEqual("InternalMultipleParameter", content.ElementAt(10).Execution.Name);
            Assert.AreEqual("InternalExit", content.ElementAt(11).Name);
            Assert.AreEqual("InternalMultipleParameter", content.ElementAt(11).Execution.Name);
        }

        [Test]
        public void TestPublicFunction()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.PublicReturnObj(1, "a");
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(12, content.Count);
            Assert.AreEqual("PublicEntry", content.ElementAt(0).Name);
            Assert.AreEqual("PublicReturnObj", content.ElementAt(0).Execution.Name);
            Assert.AreEqual("InstanceEntry", content.ElementAt(1).Name);
            Assert.AreEqual("PublicReturnObj", content.ElementAt(1).Execution.Name);
            Assert.AreEqual("MethodEntry", content.ElementAt(2).Name);
            Assert.AreEqual("PublicReturnObj", content.ElementAt(2).Execution.Name);
            Assert.AreEqual("PrivateEntry", content.ElementAt(3).Name);
            Assert.AreEqual("get_StringBuilder", content.ElementAt(3).Execution.Name);
            Assert.AreEqual("InstanceEntry", content.ElementAt(4).Name);
            Assert.AreEqual("get_StringBuilder", content.ElementAt(4).Execution.Name);
            Assert.AreEqual("PropertyGetterEntry", content.ElementAt(5).Name);
            Assert.AreEqual("get_StringBuilder", content.ElementAt(5).Execution.Name);
            Assert.AreEqual("PropertyGetterExit", content.ElementAt(6).Name);
            Assert.AreEqual("get_StringBuilder", content.ElementAt(6).Execution.Name);
            Assert.AreEqual("InstanceExit", content.ElementAt(7).Name);
            Assert.AreEqual("get_StringBuilder", content.ElementAt(7).Execution.Name);
            Assert.AreEqual("PrivateExit", content.ElementAt(8).Name);
            Assert.AreEqual("get_StringBuilder", content.ElementAt(8).Execution.Name);
            Assert.AreEqual("MethodExit", content.ElementAt(9).Name);
            Assert.AreEqual("PublicReturnObj", content.ElementAt(9).Execution.Name);
            Assert.AreEqual("InstanceExit", content.ElementAt(10).Name);
            Assert.AreEqual("PublicReturnObj", content.ElementAt(10).Execution.Name);
            Assert.AreEqual("PublicExit", content.ElementAt(11).Name);
            Assert.AreEqual("PublicReturnObj", content.ElementAt(11).Execution.Name);
        }

        [Test]
        public void TestNotWeaved()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.Square(10);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
        }

        [Test]
        public void TestAbstractFunction()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.TestAbstract(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(12, content.Count);
            Assert.AreEqual("ProtectedEntry", content.ElementAt(0).Name);
            Assert.AreEqual("ProtectedReturnString", content.ElementAt(0).Execution.Name);
            Assert.AreEqual("StaticEntry", content.ElementAt(1).Name);
            Assert.AreEqual("ProtectedReturnString", content.ElementAt(1).Execution.Name);
            Assert.AreEqual("MethodEntry", content.ElementAt(2).Name);
            Assert.AreEqual("ProtectedReturnString", content.ElementAt(2).Execution.Name);
            Assert.AreEqual("PublicEntry", content.ElementAt(3).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(3).Execution.Name);
            Assert.AreEqual("StaticEntry", content.ElementAt(4).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(4).Execution.Name);
            Assert.AreEqual("PropertyGetterEntry", content.ElementAt(5).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(5).Execution.Name);
            Assert.AreEqual("PropertyGetterExit", content.ElementAt(6).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(6).Execution.Name);
            Assert.AreEqual("StaticExit", content.ElementAt(7).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(7).Execution.Name);
            Assert.AreEqual("PublicExit", content.ElementAt(8).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(8).Execution.Name);
            Assert.AreEqual("MethodExit", content.ElementAt(9).Name);
            Assert.AreEqual("ProtectedReturnString", content.ElementAt(9).Execution.Name);
            Assert.AreEqual("StaticExit", content.ElementAt(10).Name);
            Assert.AreEqual("ProtectedReturnString", content.ElementAt(10).Execution.Name);
            Assert.AreEqual("ProtectedExit", content.ElementAt(11).Name);
            Assert.AreEqual("ProtectedReturnString", content.ElementAt(11).Execution.Name);
        }

        [Test]
        public void TestStatic()
        {
            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(OptionsTestTargetBase.PublicStaticInt);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(content.Count, 6);
            Assert.AreEqual("PublicEntry", content.ElementAt(0).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(0).Execution.Name);
            Assert.AreEqual("StaticEntry", content.ElementAt(1).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(1).Execution.Name);
            Assert.AreEqual("PropertyGetterEntry", content.ElementAt(2).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(2).Execution.Name);
            Assert.AreEqual("PropertyGetterExit", content.ElementAt(3).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(3).Execution.Name);
            Assert.AreEqual("StaticExit", content.ElementAt(4).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(4).Execution.Name);
            Assert.AreEqual("PublicExit", content.ElementAt(5).Name);
            Assert.AreEqual("get_PublicStaticInt", content.ElementAt(5).Execution.Name);
        }
    }
}

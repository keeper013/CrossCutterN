// <copyright file="OptionsTest.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OptionsTest
{
    using System;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Utilities;

    /// <summary>
    /// Options test.
    /// </summary>
    [TestFixture]
    public sealed class OptionsTest
    {
        /// <summary>
        /// "Aa" is a work around to make sure that this test runs first because this test contains static constructor triggering test.
        /// Tests construction injection.
        /// </summary>
        [Test]
        public void TestAaConstructor()
        {
            MethodAdviceContainer.Clear();
            var derived = new OptionsTestTargetDerived();
            Console.Out.WriteLine(derived);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(18, content.Count);

            // private static constructor
            Assert.AreEqual("PrivateEntry", content.ElementAt(0).Name);
            Assert.AreEqual(".cctor", content.ElementAt(0).Execution.Name);
            Assert.AreEqual(0, content.ElementAt(0).Execution.HashCode);
            Assert.AreEqual("StaticEntry", content.ElementAt(1).Name);
            Assert.AreEqual(".cctor", content.ElementAt(1).Execution.Name);
            Assert.AreEqual(0, content.ElementAt(1).Execution.HashCode);
            Assert.AreEqual("MethodEntry", content.ElementAt(2).Name);
            Assert.AreEqual(".cctor", content.ElementAt(2).Execution.Name);
            Assert.AreEqual(0, content.ElementAt(2).Execution.HashCode);
            Assert.AreEqual("MethodExit", content.ElementAt(3).Name);
            Assert.AreEqual(".cctor", content.ElementAt(3).Execution.Name);
            Assert.AreEqual(0, content.ElementAt(3).Execution.HashCode);
            Assert.AreEqual("StaticExit", content.ElementAt(4).Name);
            Assert.AreEqual(".cctor", content.ElementAt(4).Execution.Name);
            Assert.AreEqual(0, content.ElementAt(4).Execution.HashCode);
            Assert.AreEqual("PrivateExit", content.ElementAt(5).Name);
            Assert.AreEqual(".cctor", content.ElementAt(5).Execution.Name);
            Assert.AreEqual(0, content.ElementAt(5).Execution.HashCode);

            // base class constructor
            Assert.AreEqual("ProtectedEntry", content.ElementAt(6).Name);
            Assert.AreEqual(".ctor", content.ElementAt(6).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(6).Execution.HashCode);
            Assert.AreEqual("InstanceEntry", content.ElementAt(7).Name);
            Assert.AreEqual(".ctor", content.ElementAt(7).Execution.Name);
            Assert.AreNotEqual(7, content.ElementAt(6).Execution.HashCode);
            Assert.AreEqual("MethodEntry", content.ElementAt(8).Name);
            Assert.AreEqual(".ctor", content.ElementAt(8).Execution.Name);
            Assert.AreNotEqual(8, content.ElementAt(6).Execution.HashCode);
            Assert.AreEqual("InternalEntry", content.ElementAt(9).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(9).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(9).Execution.HashCode);
            Assert.AreEqual("InstanceEntry", content.ElementAt(10).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(10).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(10).Execution.HashCode);
            Assert.AreEqual("PropertySetterEntry", content.ElementAt(11).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(11).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(11).Execution.HashCode);
            Assert.AreEqual("PropertySetterExit", content.ElementAt(12).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(12).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(12).Execution.HashCode);
            Assert.AreEqual("InstanceExit", content.ElementAt(13).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(13).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(13).Execution.HashCode);
            Assert.AreEqual("InternalExit", content.ElementAt(14).Name);
            Assert.AreEqual("set_InternalInstanceFunc", content.ElementAt(14).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(14).Execution.HashCode);
            Assert.AreEqual("MethodExit", content.ElementAt(15).Name);
            Assert.AreEqual(".ctor", content.ElementAt(15).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(15).Execution.HashCode);
            Assert.AreEqual("InstanceExit", content.ElementAt(16).Name);
            Assert.AreEqual(".ctor", content.ElementAt(16).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(16).Execution.HashCode);
            Assert.AreEqual("ProtectedExit", content.ElementAt(17).Name);
            Assert.AreEqual(".ctor", content.ElementAt(17).Execution.Name);
            Assert.AreNotEqual(0, content.ElementAt(17).Execution.HashCode);

            // derived class constructor is not concerned.
        }

        /// <summary>
        /// Tests internal property weaving.
        /// </summary>
        [Test]
        public void TestInternalProperty()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.InternalInstanceFunc(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
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

        /// <summary>
        /// Tests internal method weaving .
        /// </summary>
        [Test]
        public void TestInternalMethod()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            var strb = new StringBuilder();
            derived.InternalMultipleParameter(ref strb, 1, "str", null);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
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

        /// <summary>
        /// Tests public method weaving.
        /// </summary>
        [Test]
        public void TestPublicMethod()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.PublicReturnObj(1, "a");
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
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

        /// <summary>
        /// Tests Not weaved method.
        /// </summary>
        [Test]
        public void TestNotWeaved()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.Square(10);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// Tests abstract method weaving.
        /// </summary>
        [Test]
        public void TestAbstractMethod()
        {
            var derived = new OptionsTestTargetDerived();
            MethodAdviceContainer.Clear();
            derived.TestAbstract(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
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

        /// <summary>
        /// Tests static property weaving.
        /// </summary>
        [Test]
        public void TestStatic()
        {
            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(OptionsTestTargetBase.PublicStaticInt);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
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

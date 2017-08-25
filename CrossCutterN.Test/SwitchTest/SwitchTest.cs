// <copyright file="SwitchTest.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.SwitchTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CrossCutterN.Base.Switch;
    using NUnit.Framework;
    using Utilities;

    /// <summary>
    /// Switch test.
    /// </summary>
    [TestFixture]
    public sealed class SwitchTest
    {
        private const string SwitchAspect = "switchAspect";
        private const string Switch2 = "switch2";
        private const string Switch3 = "switch3";
        private const string Switch4 = "switch4";
        private const string Switch5 = "switch5";
        private const string Switch6 = "switch6";
        private const string Switch7 = "switch7";
        private const string Switch8 = "switch8";
        private const string Switch9 = "switch9";
        private const string Switch10 = "switch10";

        /// <summary>
        /// Tests wwitch class aspect.
        /// </summary>
        [Test]
        public void TestSwitchClassAspect()
        {
            SwitchFacade.Controller.SwitchOff(typeof(SwitchClassAspectTestTarget), Switch2);
            MethodAdviceContainer.Clear();
            SwitchClassAspectTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            SwitchFacade.Controller.Switch(typeof(SwitchClassAspectTestTarget), Switch2);
            MethodAdviceContainer.Clear();
            SwitchClassAspectTestTarget.Test(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        /// <summary>
        /// Tests switch method aspect.
        /// </summary>
        [Test]
        public void TestSwitchMethodAspect()
        {
            SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodAspectTestTarget).GetMethod("Test1"), Switch2);
            SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodAspectTestTarget).GetMethod("Test2"), Switch3);
            SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodAspectTestTarget).GetProperty("Value").SetMethod, Switch2);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test2(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Exit2", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Value = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchMethodAspectTestTarget.Value);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            SwitchFacade.Controller.Switch(typeof(SwitchMethodAspectTestTarget).GetMethod("Test1"), Switch2);
            SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodAspectTestTarget).GetMethod("Test2"), Switch3);
            SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodAspectTestTarget).GetProperty("Value").SetMethod, Switch2);
            SwitchFacade.Controller.Switch(typeof(SwitchMethodAspectTestTarget).GetProperty("Value").GetMethod, Switch3);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test2(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Value = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchMethodAspectTestTarget.Value);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Exit2", content.ElementAt(1).Name);
        }

        /// <summary>
        /// Tests switch property aspect.
        /// </summary>
        [Test]
        public void TestSwitchPropertyAspect()
        {
            SwitchFacade.Controller.Switch(typeof(SwitchPropertyAspectTestTarget).GetProperty("Value"), Switch2);
            MethodAdviceContainer.Clear();
            SwitchPropertyAspectTestTarget.Value = 1;
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            Console.Out.Write(SwitchPropertyAspectTestTarget.Value);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            SwitchFacade.Controller.SwitchOn(typeof(SwitchPropertyAspectTestTarget).GetProperty("Value"), Switch2);
            MethodAdviceContainer.Clear();
            SwitchPropertyAspectTestTarget.Value = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.Write(SwitchPropertyAspectTestTarget.Value);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        /// <summary>
        /// Tests switch aspect.
        /// </summary>
        [Test]
        public void TestSwitchAspect()
        {
            SwitchFacade.Controller.SwitchOff(SwitchAspect);
            MethodAdviceContainer.Clear();
            SwitchAspectTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            SwitchFacade.Controller.SwitchOn(SwitchAspect);
            MethodAdviceContainer.Clear();
            SwitchAspectTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry1", content.ElementAt(0).Name);
            Assert.AreEqual("Exit1", content.ElementAt(1).Name);
        }

        /// <summary>
        /// Tests switch class.
        /// </summary>
        [Test]
        public void TestSwitchClass()
        {
            SwitchFacade.Controller.SwitchOff(typeof(SwitchClassTestTarget));
            MethodAdviceContainer.Clear();
            SwitchClassTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            SwitchFacade.Controller.Switch(typeof(SwitchClassTestTarget));
            MethodAdviceContainer.Clear();
            SwitchClassTestTarget.Test(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        /// <summary>
        /// Tests switch method.
        /// </summary>
        [Test]
        public void TestSwitchMethod()
        {
            SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodTestTarget).GetMethod("Test1"));
            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test2(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodTestTarget).GetMethod("Test1"));
            SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodTestTarget).GetMethod("Test2"));
            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test2(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// Tests switch property.
        /// </summary>
        [Test]
        public void TestSwitchProperty()
        {
            SwitchFacade.Controller.SwitchOff(typeof(SwitchPropertyTestTarget).GetProperty("Value1"));
            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value1 = 1;
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value2 = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value2);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            SwitchFacade.Controller.SwitchOn(typeof(SwitchPropertyTestTarget).GetProperty("Value1"));
            SwitchFacade.Controller.Switch(typeof(SwitchPropertyTestTarget).GetProperty("Value2"));
            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value2 = 1;
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value2);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value1 = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        /// <summary>
        /// Tests switch status lookup.
        /// </summary>
        [Test]
        public void TestSwitchStatus()
        {
            var clazz = typeof(SwitchStatusLookUpTestTarget);
            var method = clazz.GetMethod("Test1");
            var property = clazz.GetProperty("Value1");
            var getter = property.GetMethod;
            Assert.IsFalse(SwitchFacade.Controller.GetSwitchStatus(method, Switch4).HasValue);

            SwitchFacade.Controller.Switch(Switch4);
            SwitchFacade.Controller.Switch(method);
            SwitchFacade.Controller.SwitchOff(clazz);
            SwitchFacade.Controller.Switch(method, Switch4);
            SwitchFacade.Controller.Switch(Switch4);
            SwitchFacade.Controller.Switch(clazz, Switch4);
            SwitchFacade.Controller.Switch(property, Switch5);
            SwitchFacade.Controller.Switch(Switch5);
            Assert.IsFalse(SwitchFacade.Controller.GetSwitchStatus(method, Switch4).HasValue);

            SwitchStatusLookUpTestTarget.Test1(1);
            var value = SwitchFacade.Controller.GetSwitchStatus(method, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);
            value = SwitchFacade.Controller.GetSwitchStatus(getter, Switch5);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);
            value = SwitchFacade.Controller.GetSwitchStatus(method, Switch5);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            SwitchFacade.Controller.Switch(Switch4);
            value = SwitchFacade.Controller.GetSwitchStatus(getter, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            SwitchFacade.Controller.Switch(clazz);
            value = SwitchFacade.Controller.GetSwitchStatus(method, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            SwitchFacade.Controller.Switch(method);
            value = SwitchFacade.Controller.GetSwitchStatus(method, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);

            SwitchFacade.Controller.Switch(property);
            value = SwitchFacade.Controller.GetSwitchStatus(getter, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            SwitchFacade.Controller.Switch(method, Switch4);
            value = SwitchFacade.Controller.GetSwitchStatus(method, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            SwitchFacade.Controller.Switch(property, Switch4);
            value = SwitchFacade.Controller.GetSwitchStatus(getter, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);

            SwitchFacade.Controller.Switch(clazz, Switch4);
            value = SwitchFacade.Controller.GetSwitchStatus(method, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);
            value = SwitchFacade.Controller.GetSwitchStatus(getter, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            SwitchFacade.Controller.SwitchOn(Switch4);
            value = SwitchFacade.Controller.GetSwitchStatus(method, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);
            value = SwitchFacade.Controller.GetSwitchStatus(getter, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            SwitchFacade.Controller.SwitchOff(clazz);
            value = SwitchFacade.Controller.GetSwitchStatus(method, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);
            value = SwitchFacade.Controller.GetSwitchStatus(getter, Switch4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);

            MethodAdviceContainer.Clear();
            SwitchFacade.Controller.SwitchOff(Switch4);
            SwitchFacade.Controller.SwitchOff(Switch5);
            SwitchStatusLookUpTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// Tests switch exception.
        /// </summary>
        [Test]
        public void TestSwitchException()
        {
            var clazz = typeof(SwitchExceptionTestTarget);
            SwitchFacade.Controller.SwitchOff(clazz, Switch6);
            IReadOnlyCollection<MethodAdviceRecord> content;

            MethodAdviceContainer.Clear();
            try
            {
                SwitchExceptionTestTarget.Test1(2);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                content = MethodAdviceContainer.Content;
                MethodAdviceContainer.PrintContent(Console.Out);
                Assert.AreEqual(1, content.Count);
                Assert.IsNull(content.ElementAt(0).Return);
                Assert.IsTrue(content.ElementAt(0).HasException.HasValue && content.ElementAt(0).HasException.Value);
            }

            MethodAdviceContainer.Clear();
            SwitchFacade.Controller.SwitchOff(clazz, Switch7);
            SwitchFacade.Controller.SwitchOn(clazz, Switch6);
            SwitchExceptionTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(1, content.Count);
            Assert.IsFalse(content.ElementAt(0).HasException);
            Assert.IsNotNull(content.ElementAt(0).Return);
            Assert.IsTrue(content.ElementAt(0).Return.HasReturn);
            Assert.AreEqual(1, content.ElementAt(0).Return.Value);

            MethodAdviceContainer.Clear();
            try
            {
                SwitchExceptionTestTarget.Test1(2);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                MethodAdviceContainer.PrintContent(Console.Out);
                Assert.AreEqual(1, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Return);
                Assert.IsTrue(content.ElementAt(0).HasException.HasValue && content.ElementAt(0).HasException.Value);
                Assert.IsFalse(content.ElementAt(0).Return.HasReturn);
            }

            MethodAdviceContainer.Clear();
            SwitchFacade.Controller.SwitchOff(clazz, Switch6);
            Assert.AreEqual(0, content.Count);
            try
            {
                SwitchExceptionTestTarget.Test1(2);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                MethodAdviceContainer.PrintContent(Console.Out);
                Assert.AreEqual(0, content.Count);
            }

            MethodAdviceContainer.Clear();
            SwitchExceptionTestTarget.Test1(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// Tests switchable section, with no <see cref="CrossCutterN.Base.Metadata.IReturn"/> parameter initilaization.
        /// </summary>
        [Test]
        public void TestSwitchNoReturn()
        {
            var clazz = typeof(SwitchNoReturnTestTarget);
            MethodAdviceContainer.Clear();
            SwitchFacade.Controller.SwitchOff(clazz, Switch8);
            SwitchNoReturnTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
            SwitchFacade.Controller.SwitchOn(clazz, Switch8);
            SwitchNoReturnTestTarget.Test(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Exit6", content.ElementAt(1).Name);
            Assert.IsTrue(content.ElementAt(1).HasException.HasValue);
            Assert.IsFalse(content.ElementAt(1).HasException);
        }

        /// <summary>
        /// Tests switchable section, with no <see cref="CrossCutterN.Base.Metadata.IExecution"/> parameter initilaization.
        /// </summary>
        [Test]
        public void TestSwitchNoExecution()
        {
            var clazz = typeof(SwitchNoExecutionTestTarget);
            MethodAdviceContainer.Clear();
            SwitchFacade.Controller.SwitchOff(clazz, Switch9);
            SwitchNoExecutionTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
            SwitchFacade.Controller.SwitchOn(clazz, Switch9);
            SwitchNoExecutionTestTarget.Test(1);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Exit7", content.ElementAt(1).Name);
            Assert.IsTrue(content.ElementAt(1).HasException.HasValue);
            Assert.IsFalse(content.ElementAt(1).HasException.Value);
            Assert.IsNotNull(content.ElementAt(1).Return);
            Assert.IsTrue(content.ElementAt(1).Return.HasReturn);
            Assert.AreEqual(1, content.ElementAt(1).Return.Value);
        }

        /// <summary>
        /// Tests switchable section, with only <see cref="CrossCutterN.Base.Metadata.IExecution"/> parameter initilaization.
        /// </summary>
        [Test]
        public void TestSwitchOnlyExecution()
        {
            var clazz = typeof(SwitchOnlyExecutionTestTarget);
            MethodAdviceContainer.Clear();
            SwitchFacade.Controller.SwitchOff(clazz, Switch10);
            SwitchOnlyExecutionTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
            SwitchFacade.Controller.SwitchOn(clazz, Switch10);
            SwitchOnlyExecutionTestTarget.Test(1);
            content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual("Entry8", content.ElementAt(0).Name);
            Assert.AreEqual("Exit8", content.ElementAt(1).Name);
        }
    }
}

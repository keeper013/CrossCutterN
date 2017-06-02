/**
 * Description: switch test
 * Author: David Cui
 */

namespace CrossCutterN.Test.SwitchTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    [TestFixture]
    public class SwitchTest
    {
        [Test]
        public void TestSwitchClassAspect()
        {
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchClassAspectTestTarget), "NameExpressionSwitch2");
            MethodAdviceContainer.Clear();
            SwitchClassAspectTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            Advice.Switch.SwitchFacade.Controller.Switch(typeof(SwitchClassAspectTestTarget), "NameExpressionSwitch2");
            MethodAdviceContainer.Clear();
            SwitchClassAspectTestTarget.Test(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        [Test]
        public void TestSwitchMethodAspect()
        {
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodAspectTestTarget).GetMethod("Test1"), "NameExpressionSwitch2");
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodAspectTestTarget).GetMethod("Test2"), "NameExpressionSwitch3");
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof (SwitchMethodAspectTestTarget).GetProperty("Value").SetMethod, "NameExpressionSwitch2");

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test2(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Exit2", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Value = 1;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchMethodAspectTestTarget.Value);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            Advice.Switch.SwitchFacade.Controller.Switch(typeof(SwitchMethodAspectTestTarget).GetMethod("Test1"), "NameExpressionSwitch2");
            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodAspectTestTarget).GetMethod("Test2"), "NameExpressionSwitch3");
            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodAspectTestTarget).GetProperty("Value").SetMethod, "NameExpressionSwitch2");
            Advice.Switch.SwitchFacade.Controller.Switch(typeof(SwitchMethodAspectTestTarget).GetProperty("Value").GetMethod, "NameExpressionSwitch3");

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test1(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Test2(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodAspectTestTarget.Value = 1;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchMethodAspectTestTarget.Value);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Exit2", content.ElementAt(1).Name);
        }

        [Test]
        public void TestSwitchPropertyAspect()
        {
            Advice.Switch.SwitchFacade.Controller.Switch(typeof(SwitchPropertyAspectTestTarget).GetProperty("Value"), "NameExpressionSwitch2");
            MethodAdviceContainer.Clear();
            SwitchPropertyAspectTestTarget.Value = 1;
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            Console.Out.Write(SwitchPropertyAspectTestTarget.Value);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry3", content.ElementAt(0).Name);
            Assert.AreEqual("Exit3", content.ElementAt(1).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchPropertyAspectTestTarget).GetProperty("Value"), "NameExpressionSwitch2");
            MethodAdviceContainer.Clear();
            SwitchPropertyAspectTestTarget.Value = 1;
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.Write(SwitchPropertyAspectTestTarget.Value);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        [Test]
        public void TestSwitchAspect()
        {
            Advice.Switch.SwitchFacade.Controller.SwitchOff("NameExpressionSwitchAspect");
            MethodAdviceContainer.Clear();
            SwitchAspectTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            Advice.Switch.SwitchFacade.Controller.SwitchOn("NameExpressionSwitchAspect");
            MethodAdviceContainer.Clear();
            SwitchAspectTestTarget.Test1(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry1", content.ElementAt(0).Name);
            Assert.AreEqual("Exit1", content.ElementAt(1).Name);
        }

        [Test]
        public void TestSwitchClass()
        {
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchClassTestTarget));
            MethodAdviceContainer.Clear();
            SwitchClassTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            Advice.Switch.SwitchFacade.Controller.Switch(typeof(SwitchClassTestTarget));
            MethodAdviceContainer.Clear();
            SwitchClassTestTarget.Test(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        [Test]
        public void TestSwitchMethod()
        {
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodTestTarget).GetMethod("Test1"));
            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test2(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchCompareTestTarget.Test1(1);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodTestTarget).GetMethod("Test1"));
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodTestTarget).GetMethod("Test2"));
            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test1(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            SwitchMethodTestTarget.Test2(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
        }

        [Test]
        public void TestSwitchProperty()
        {
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchPropertyTestTarget).GetProperty("Value1"));
            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value1 = 1;
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value2 = 1;
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value2);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchPropertyTestTarget).GetProperty("Value1"));
            Advice.Switch.SwitchFacade.Controller.Switch(typeof(SwitchPropertyTestTarget).GetProperty("Value2"));
            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value2 = 1;
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value2);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            MethodAdviceContainer.Clear();
            SwitchPropertyTestTarget.Value1 = 1;
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);

            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(SwitchPropertyTestTarget.Value1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry2", content.ElementAt(0).Name);
            Assert.AreEqual("Entry3", content.ElementAt(1).Name);
            Assert.AreEqual("Exit3", content.ElementAt(2).Name);
            Assert.AreEqual("Exit2", content.ElementAt(3).Name);
        }

        [Test]
        public void TestSwitchLookUp()
        {
            var clazz = typeof (SwitchLookUpTestTarget);
            var method = clazz.GetMethod("Test1");
            var property = clazz.GetProperty("Value1");
            var getter = property.GetMethod;
            const string aspect4 = "NameExpressionSwitch4";
            const string aspect5 = "NameExpressionSwitch5";
            Assert.IsFalse(Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4).HasValue);

            Advice.Switch.SwitchFacade.Controller.Switch(aspect4);
            Advice.Switch.SwitchFacade.Controller.Switch(method);
            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz);
            Advice.Switch.SwitchFacade.Controller.Switch(method, aspect4);
            Advice.Switch.SwitchFacade.Controller.Switch(aspect4);
            Advice.Switch.SwitchFacade.Controller.Switch(clazz, aspect4);
            Advice.Switch.SwitchFacade.Controller.Switch(property, aspect5);
            Advice.Switch.SwitchFacade.Controller.Switch(aspect5);
            Assert.IsFalse(Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4).HasValue);

            SwitchLookUpTestTarget.Test1(1);
            var value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(getter, aspect5);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect5);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            Advice.Switch.SwitchFacade.Controller.Switch(aspect4);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(getter, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            Advice.Switch.SwitchFacade.Controller.Switch(clazz);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            Advice.Switch.SwitchFacade.Controller.Switch(method);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);

            Advice.Switch.SwitchFacade.Controller.Switch(property);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(getter, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            Advice.Switch.SwitchFacade.Controller.Switch(method, aspect4);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            Advice.Switch.SwitchFacade.Controller.Switch(property, aspect4);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(getter, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);

            Advice.Switch.SwitchFacade.Controller.Switch(clazz, aspect4);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(getter, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            Advice.Switch.SwitchFacade.Controller.SwitchOn(aspect4);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(getter, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsTrue(value.Value);

            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(method, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);
            value = Advice.Switch.SwitchFacade.Controller.GetSwitchStatus(getter, aspect4);
            Assert.IsTrue(value.HasValue);
            Assert.IsFalse(value.Value);

            MethodAdviceContainer.Clear();
            Advice.Switch.SwitchFacade.Controller.SwitchOff(aspect4);
            Advice.Switch.SwitchFacade.Controller.SwitchOff(aspect5);
            SwitchLookUpTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
        }

        [Test]
        public void TestSwitchException()
        {
            var clazz = typeof (SwitchExceptionTestTarget);
            const string aspect6 = "NameExpressionSwitch6";
            const string aspect7 = "NameExpressionSwitch7";
            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz, aspect6);
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
                Assert.AreEqual(1, content.Count);
                Assert.IsNull(content.ElementAt(0).Return);
                Assert.IsTrue(content.ElementAt(0).HasException.HasValue && content.ElementAt(0).HasException.Value);
            }

            MethodAdviceContainer.Clear();
            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz, aspect7);
            Advice.Switch.SwitchFacade.Controller.SwitchOn(clazz, aspect6);
            SwitchExceptionTestTarget.Test1(1);
            content = MethodAdviceContainer.Content;
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
                content = MethodAdviceContainer.Content;
                Assert.AreEqual(1, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Return);
                Assert.IsTrue(content.ElementAt(0).HasException.HasValue && content.ElementAt(0).HasException.Value);
                Assert.IsFalse(content.ElementAt(0).Return.HasReturn);
            }

            MethodAdviceContainer.Clear();
            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz, aspect6);
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
                content = MethodAdviceContainer.Content;
                Assert.AreEqual(0, content.Count);
            }

            MethodAdviceContainer.Clear();
            SwitchExceptionTestTarget.Test1(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
        }

        [Test]
        public void TestSwitchNoReturn()
        {
            var clazz = typeof(SwitchNoReturnTestTarget);
            const string aspect8 = "NameExpressionSwitch8";
            MethodAdviceContainer.Clear();
            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz, aspect8);
            SwitchNoReturnTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
            Advice.Switch.SwitchFacade.Controller.SwitchOn(clazz, aspect8);
            SwitchNoReturnTestTarget.Test(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Exit6", content.ElementAt(1).Name);
            Assert.IsTrue(content.ElementAt(1).HasException.HasValue);
            Assert.IsFalse(content.ElementAt(1).HasException);
        }

        [Test]
        public void TestSwitchNoExecution()
        {
            var clazz = typeof(SwitchNoExecutionTestTarget);
            const string aspect9 = "NameExpressionSwitch9";
            MethodAdviceContainer.Clear();
            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz, aspect9);
            SwitchNoExecutionTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
            Advice.Switch.SwitchFacade.Controller.SwitchOn(clazz, aspect9);
            SwitchNoExecutionTestTarget.Test(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Exit7", content.ElementAt(1).Name);
            Assert.IsTrue(content.ElementAt(1).HasException.HasValue);
            Assert.IsFalse(content.ElementAt(1).HasException.Value);
            Assert.IsNotNull(content.ElementAt(1).Return);
            Assert.IsTrue(content.ElementAt(1).Return.HasReturn);
            Assert.AreEqual(1, content.ElementAt(1).Return.Value);
        }

        [Test]
        public void TestSwitchOnlyExecution()
        {
            var clazz = typeof(SwitchOnlyExecutionTestTarget);
            const string aspect10 = "NameExpressionSwitch10";
            MethodAdviceContainer.Clear();
            Advice.Switch.SwitchFacade.Controller.SwitchOff(clazz, aspect10);
            SwitchOnlyExecutionTestTarget.Test(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
            Advice.Switch.SwitchFacade.Controller.SwitchOn(clazz, aspect10);
            SwitchOnlyExecutionTestTarget.Test(1);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry8", content.ElementAt(0).Name);
            Assert.AreEqual("Exit8", content.ElementAt(1).Name);
        }
    }
}

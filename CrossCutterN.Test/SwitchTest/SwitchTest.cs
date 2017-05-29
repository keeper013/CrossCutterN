/**
 * Description: switch test
 * Author: David Cui
 */

namespace CrossCutterN.Test.SwitchTest
{
    using System;
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

            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchClassAspectTestTarget), "NameExpressionSwitch2");
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

            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodAspectTestTarget).GetMethod("Test1"), "NameExpressionSwitch2");
            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodAspectTestTarget).GetMethod("Test2"), "NameExpressionSwitch3");
            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchMethodAspectTestTarget).GetProperty("Value").SetMethod, "NameExpressionSwitch2");
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchMethodAspectTestTarget).GetProperty("Value").GetMethod, "NameExpressionSwitch3");

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
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchPropertyAspectTestTarget).GetProperty("Value"), "NameExpressionSwitch2");
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

            Advice.Switch.SwitchFacade.Controller.SwitchOn(typeof(SwitchClassTestTarget));
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
            Advice.Switch.SwitchFacade.Controller.SwitchOff(typeof(SwitchPropertyTestTarget).GetProperty("Value2"));
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
    }
}

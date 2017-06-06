/**
 * Description: context test
 * Author: David Cui
 */

namespace CrossCutterN.Test.ContextTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    [TestFixture]
    class ConTextTest
    {
        [Test]
        public void Test1()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.IsNotNull(content.ElementAt(0).Context);
            Assert.IsNotNull(content.ElementAt(1).Context);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value1);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value2);
        }

        [Test]
        public void Test2()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ContextTestTarget.Test2(1);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(3, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Context);
                Assert.IsNotNull(content.ElementAt(1).Context);
                Assert.IsNotNull(content.ElementAt(2).Context);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Exception1")).Value1, ((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value1);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Exception1")).Value2, ((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value2);
                Assert.AreEqual(((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value1, ((TestObj)content.ElementAt(2).Context.Get("Exception1")).Value1);
                Assert.AreEqual(((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value2, ((TestObj)content.ElementAt(2).Context.Get("Exception1")).Value2);
            }
        }

        [Test]
        public void Test3()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test3(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.IsNotNull(content.ElementAt(0).Context);
            Assert.IsNull(content.ElementAt(1).Context);
            Assert.AreEqual(1, ((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1);
            Assert.AreEqual("Entry1", ((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2);
        }

        [Test]
        public void Test4()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ContextTestTarget.Test4(1);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(3, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Context);
                Assert.IsNotNull(content.ElementAt(1).Context);
                Assert.IsNull(content.ElementAt(2).Context);
                Assert.IsFalse(content.ElementAt(1).Context.Exist("Entry1"));
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Exception1")).Value1, ((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value1);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Exception1")).Value2, ((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value2);
            }
        }

        [Test]
        public void Test5()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test5(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.IsNotNull(content.ElementAt(0).Context);
            Assert.IsNotNull(content.ElementAt(1).Context);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value1);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value2);
        }

        [Test]
        public void Test6()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ContextTestTarget.Test6(1);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(3, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Context);
                Assert.IsNull(content.ElementAt(1).Context);
                Assert.IsNotNull(content.ElementAt(2).Context);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1, ((TestObj)content.ElementAt(2).Context.Get("Entry1")).Value1);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2, ((TestObj)content.ElementAt(2).Context.Get("Entry1")).Value2);
            }
        }

        [Test]
        public void Test7()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test7(1);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.IsNull(content.ElementAt(0).Context);
            Assert.IsNotNull(content.ElementAt(1).Context);
            Assert.IsFalse(content.ElementAt(1).Context.Exist("Entry1"));
            Assert.IsFalse(content.ElementAt(1).Context.Exist("Exception1"));
        }

        [Test]
        public void Test8()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ContextTestTarget.Test8(1);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(3, content.Count);
                Assert.IsNull(content.ElementAt(0).Context);
                Assert.IsNotNull(content.ElementAt(1).Context);
                Assert.IsNotNull(content.ElementAt(2).Context);
                Assert.IsFalse(content.ElementAt(1).Context.Exist("Entry1"));
                Assert.AreEqual(((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value1, ((TestObj)content.ElementAt(2).Context.Get("Exception1")).Value1);
                Assert.AreEqual(((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value2, ((TestObj)content.ElementAt(2).Context.Get("Exception1")).Value2);
            }
        }
    }
}

// <copyright file="ContextTest.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.ContextTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    /// <summary>
    /// Context parameter related test.
    /// </summary>
    [TestFixture]
    public sealed class ContextTest
    {
        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value set upon entry, no exception, and used upon exit.
        /// </summary>
        [Test]
        public void Test1()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test1(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.IsNotNull(content.ElementAt(0).Context);
            Assert.IsNotNull(content.ElementAt(1).Context);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value1);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value2);
        }

        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value set upon entry, update upon exception and used upon exit.
        /// </summary>
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
                MethodAdviceContainer.PrintContent(Console.Out);
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

        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value set upon entry, no exception and not used upon exit.
        /// </summary>
        [Test]
        public void Test3()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test3(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.IsNotNull(content.ElementAt(0).Context);
            Assert.IsNull(content.ElementAt(1).Context);
            Assert.AreEqual(1, ((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1);
            Assert.AreEqual("Entry1", ((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2);
        }

        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value set upon entry, no exception and not used upon exit.
        /// </summary>
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
                MethodAdviceContainer.PrintContent(Console.Out);
                Assert.AreEqual(3, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Context);
                Assert.IsNotNull(content.ElementAt(1).Context);
                Assert.IsNull(content.ElementAt(2).Context);
                Assert.IsFalse(content.ElementAt(1).Context.Exist("Entry1"));
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Exception1")).Value1, ((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value1);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Exception1")).Value2, ((TestObj)content.ElementAt(1).Context.Get("Exception1")).Value2);
            }
        }

        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value set upon entry, no exception and used upon exit.
        /// </summary>
        [Test]
        public void Test5()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test5(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.IsNotNull(content.ElementAt(0).Context);
            Assert.IsNotNull(content.ElementAt(1).Context);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value1);
            Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2, ((TestObj)content.ElementAt(1).Context.Get("Entry1")).Value2);
        }

        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value set upon entry, exception happened but not used, and used upon exit.
        /// </summary>
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
                MethodAdviceContainer.PrintContent(Console.Out);
                Assert.AreEqual(3, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Context);
                Assert.IsNull(content.ElementAt(1).Context);
                Assert.IsNotNull(content.ElementAt(2).Context);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value1, ((TestObj)content.ElementAt(2).Context.Get("Entry1")).Value1);
                Assert.AreEqual(((TestObj)content.ElementAt(0).Context.Get("Entry1")).Value2, ((TestObj)content.ElementAt(2).Context.Get("Entry1")).Value2);
            }
        }

        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value not set upon entry, exception not happened, and not detected upon exit.
        /// </summary>
        [Test]
        public void Test7()
        {
            MethodAdviceContainer.Clear();
            ContextTestTarget.Test7(1);
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.IsNull(content.ElementAt(0).Context);
            Assert.IsNotNull(content.ElementAt(1).Context);
            Assert.IsFalse(content.ElementAt(1).Context.Exist("Entry1"));
            Assert.IsFalse(content.ElementAt(1).Context.Exist("Exception1"));
        }

        /// <summary>
        /// Tests <see cref="CrossCutterN.Base.Metadata.IExecutionContext"/> value not set upon entry, set upon exceptoin, and used upon exit.
        /// </summary>
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
                MethodAdviceContainer.PrintContent(Console.Out);
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

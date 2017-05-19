/**
 * Description: mixed test
 * Author: David Cui
 */

namespace CrossCutterN.Test.ParameterTest
{
    using System;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Utilities;

    [TestFixture]
    public class ParameterTest
    {
        private int Square(int i)
        {
            return i*i;
        }

        [Test]
        public void TestAllException()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ParameterTestTarget.AllExceptionTest(10, new StringBuilder("abc"), Square);
            }
            catch (Exception e)
            {
                Assert.AreEqual("abc100", e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(3, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Execution);
                Assert.AreEqual(3, content.ElementAt(0).Execution.Parameters.Count);
                Assert.AreEqual(10, content.ElementAt(0).Execution.Parameters.ElementAt(0).Value);
                Assert.AreEqual("abc", content.ElementAt(0).Execution.Parameters.ElementAt(1).Value.ToString());
                Assert.AreEqual(typeof(Func<int, int>), content.ElementAt(0).Execution.Parameters.ElementAt(2).Value.GetType());
                Assert.IsNotNull(content.ElementAt(1).Execution);
                Assert.AreEqual(3, content.ElementAt(1).Execution.Parameters.Count);
                Assert.AreEqual("abc100", content.ElementAt(1).Exception.Message);
                Assert.IsNotNull(content.ElementAt(2).Execution);
                Assert.AreEqual(3, content.ElementAt(2).Execution.Parameters.Count);
                Assert.IsNotNull(content.ElementAt(2).Return);
                Assert.IsFalse(content.ElementAt(2).Return.HasReturn);
                var hasException = content.ElementAt(2).HasException;
                Assert.IsTrue(hasException != null && (hasException.HasValue && hasException.Value));
            }
        }

        [Test]
        public void TestAll()
        {
            MethodAdviceContainer.Clear();
            var x = ParameterTestTarget.AllTest();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.IsTrue(content.ElementAt(1).Return.HasReturn);
            Assert.AreEqual(x, content.ElementAt(1).Return.Value);
        }

        [Test]
        public void TestVoidReturn()
        {
            MethodAdviceContainer.Clear();
            ParameterTestTarget.AllVoidReturnTest();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.IsNotNull(content.ElementAt(1).Return);
            Assert.IsFalse(content.ElementAt(1).Return.HasReturn);
            var hasException = content.ElementAt(1).HasException;
            Assert.IsFalse(hasException != null && (hasException.HasValue && hasException.Value));
        }

        [Test]
        public void TestNoExecution()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ParameterTestTarget.NoExecutionTest();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(3, content.Count);
                Assert.IsNull(content.ElementAt(0).Execution);
                Assert.IsNull(content.ElementAt(1).Execution);
                Assert.IsNotNull(content.ElementAt(1).Exception);
                Assert.IsNull(content.ElementAt(2).Execution);
                Assert.IsNotNull(content.ElementAt(2).Return);
                Assert.IsFalse(content.ElementAt(2).Return.HasReturn);
                var hasException = content.ElementAt(2).HasException;
                Assert.IsTrue(hasException != null && (hasException.HasValue && hasException.Value));
            }
        }

        [Test]
        public void TestNoReturn()
        {
            MethodAdviceContainer.Clear();
            ParameterTestTarget.NoReturnTest(10, Square);
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.IsNull(content.ElementAt(1).Return);
        }

        [Test]
        public void TestNoHasException()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ParameterTestTarget.NoHasExceptionTest();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(1, content.Count);
                Assert.IsFalse(content.ElementAt(0).HasException.HasValue);
            }
        }

        [Test]
        public void TestNoException()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ParameterTestTarget.NoExceptionTest();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(1, content.Count);
                Assert.IsNull(content.ElementAt(0).Exception);
            }
        }

        [Test]
        public void TestOnlyExecution()
        {
            MethodAdviceContainer.Clear();
            try
            {
                ParameterTestTarget.OnlyExecutionTest();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }
            finally
            {
                var content = MethodAdviceContainer.Content;
                Assert.AreEqual(2, content.Count);
                Assert.IsNotNull(content.ElementAt(0).Execution);
                Assert.AreEqual(0, content.ElementAt(0).Execution.Parameters.Count);
                Assert.IsNull(content.ElementAt(0).Exception);
                Assert.IsNotNull(content.ElementAt(1).Execution);
                Assert.IsNull(content.ElementAt(1).Return);
                Assert.IsFalse(content.ElementAt(1).HasException.HasValue);
            }
        }
    }
}

/**
 * Description: Overwrite test
 * Author: David Cui
 */

namespace CrossCutterN.Test.OverwriteTest
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilities;

    [TestClass]
    public class OverwriteTest
    {
        [TestMethod]
        public void TestClassOverwrittenByOther()
        {
            // Constructor test
            MethodAdviceContainer.Clear();
            var tester = new OverwriteTestTarget();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // no concern method
            MethodAdviceContainer.Clear();
            tester.NoConcernMethod();
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // point cut overwrite & exception
            MethodAdviceContainer.Clear();
            try
            {
                tester.ThrowException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                content = MethodAdviceContainer.Content;
                Assert.AreEqual(1, content.Count);
                Assert.AreEqual("Exception", content.ElementAt(0).Name);
            }

            // overwrite by method concern and overwrite by property no concern
            MethodAdviceContainer.Clear();
            OverwriteTestTarget.OverwriteByMethodConcern();
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
            Assert.AreEqual("Entry", content.ElementAt(1).Name);
            Assert.AreEqual("Exit", content.ElementAt(2).Name);
            Assert.AreEqual("Exit", content.ElementAt(3).Name);
        }

        [TestMethod]
        public void TestNoClassAttribute()
        {
            // Constructor test
            MethodAdviceContainer.Clear();
            var tester = new OverwriteTestClassNotMarked();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // internal method and attribute concerned by attribute
            MethodAdviceContainer.Clear();
            tester.InternalMethodConceredByAttribute();
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
            Assert.AreEqual("Entry", content.ElementAt(1).Name);
            Assert.AreEqual("Exit", content.ElementAt(2).Name);
            Assert.AreEqual("Exit", content.ElementAt(3).Name);

            // only exit advice
            MethodAdviceContainer.Clear();
            tester.OnlyExit();
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Exit", content.ElementAt(0).Name);

            // not concerned by property attribute
            MethodAdviceContainer.Clear();
            tester.InternalProperty = 1;
            Assert.AreEqual(0, content.Count);
        }

        [TestMethod]
        public void TestClassAttributeOverwrite()
        {
            // Constructor test
            MethodAdviceContainer.Clear();
            var tester = new OverwriteTestClassPropertyConcerned();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // setter not concerned
            MethodAdviceContainer.Clear();
            tester.InernalProperty = 1;
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // getter concerned
            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(tester.InernalProperty);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
            Assert.AreEqual("Exit", content.ElementAt(1).Name);

            //method not concerned
            MethodAdviceContainer.Clear();
            tester.NotConcernedMethod();
            Assert.AreEqual(0, content.Count);
        }
    }
}

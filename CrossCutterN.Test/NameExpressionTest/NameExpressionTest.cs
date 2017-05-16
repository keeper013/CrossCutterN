/**
 * Description: mixed test
 * Author: David Cui
 */

namespace CrossCutterN.Test.NameExpressionTest
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilities;

    [TestClass]
    public class NameExpressionTest
    {
        [TestMethod]
        public void TestNameExpression()
        {
            // not mentioned method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodNotMentioned();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // not mentioned property
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.PropertyNotMentioned = 1;
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // excluded method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodNotToBeConcerned();
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // excluded property
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.PropertyNotToBeConcerned = 2;
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // included method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodToBeConcerned();
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);

            // included property
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.PropertyToBeConcerned = 3;
            Console.Out.WriteLine(NameExpressionTestTarget.PropertyToBeConcerned);
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
        }
    }
}

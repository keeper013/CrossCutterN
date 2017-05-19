/**
 * Description: mixed test
 * Author: David Cui
 */

namespace CrossCutterN.Test.NameExpressionTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    [TestFixture]
    public class NameExpressionTest
    {
        [Test]
        public void TestNotMentioned()
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
        }

        [Test]
        public void TextEcluded()
        {
            // excluded method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodNotToBeConcerned();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);

            // excluded property
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.PropertyNotToBeConcerned = 2;
            content = MethodAdviceContainer.Content;
            Assert.AreEqual(0, content.Count);
        }

        [Test]
        public void TestIncluded()
        {
            // included method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodToBeConcerned();
            var content = MethodAdviceContainer.Content;
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

        [Test]
        public void TestExactMatch()
        {
            // matched by exact overwrites all
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.NotMentionedToTestExactOverwrite();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
        }
    }
}

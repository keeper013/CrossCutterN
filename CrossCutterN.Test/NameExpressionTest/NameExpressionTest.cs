// <copyright file="NameExpressionTest.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.NameExpressionTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    /// <summary>
    /// Name expression weaving test.
    /// </summary>
    [TestFixture]
    public sealed class NameExpressionTest
    {
        /// <summary>
        /// Test case that not mentioned methods are not weaved.
        /// </summary>
        [Test]
        public void TestNotMentioned()
        {
            // not mentioned method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodNotMentioned();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            // not mentioned property
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.PropertyNotMentioned = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// Test case that excluded methods are not weaved.
        /// </summary>
        [Test]
        public void TextExcluded()
        {
            // excluded method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodNotToBeConcerned();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            // excluded property
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.PropertyNotToBeConcerned = 2;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// Test case that included methods are weaved.
        /// </summary>
        [Test]
        public void TestIncluded()
        {
            // included method
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.MethodToBeConcerned();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);

            // included property
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.PropertyToBeConcerned = 3;
            Console.Out.WriteLine(NameExpressionTestTarget.PropertyToBeConcerned);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
        }

        /// <summary>
        /// Test case that methods included by exact match will be weaved even though excluded by not exact match.
        /// </summary>
        [Test]
        public void TestExactMatch()
        {
            // matched by exact overwrites all
            MethodAdviceContainer.Clear();
            NameExpressionTestTarget.NotMentionedToTestExactOverwrite();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
        }
    }
}

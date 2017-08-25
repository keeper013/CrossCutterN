// <copyright file="OverwriteTest.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.OverwriteTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    /// <summary>
    /// Overwrite test.
    /// </summary>
    [TestFixture]
    public sealed class OverwriteTest
    {
        /// <summary>
        /// Tests class concern overwritten.
        /// </summary>
        [Test]
        public void TestClassOverwrittenByOther()
        {
            // Constructor test
            MethodAdviceContainer.Clear();
            var tester = new OverwriteTestTarget();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            // no concern method
            MethodAdviceContainer.Clear();
            tester.NoConcernMethod();
            MethodAdviceContainer.PrintContent(Console.Out);
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
                MethodAdviceContainer.PrintContent(Console.Out);
                Assert.AreEqual(3, content.Count);
                Assert.AreEqual("Entry", content.ElementAt(0).Name);
                Assert.AreEqual("Exception", content.ElementAt(1).Name);
                Assert.AreEqual("Exit", content.ElementAt(2).Name);
            }

            // overwrite by method concern and overwrite by property no concern
            MethodAdviceContainer.Clear();
            OverwriteTestTarget.OverwriteByMethodConcern();
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
            Assert.AreEqual("Entry", content.ElementAt(1).Name);
            Assert.AreEqual("Exit", content.ElementAt(2).Name);
            Assert.AreEqual("Exit", content.ElementAt(3).Name);
        }

        /// <summary>
        /// Tests no class concern, concerned by method or property attribute.
        /// </summary>
        [Test]
        public void TestNoClassAttribute()
        {
            // Constructor test
            MethodAdviceContainer.Clear();
            var tester = new OverwriteTestClassNotMarked();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            // internal method and attribute concerned by attribute
            MethodAdviceContainer.Clear();
            tester.InternalMethodConceredByAttribute();
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(4, content.Count);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
            Assert.AreEqual("Entry", content.ElementAt(1).Name);
            Assert.AreEqual("Exit", content.ElementAt(2).Name);
            Assert.AreEqual("Exit", content.ElementAt(3).Name);

            // only exit advice
            MethodAdviceContainer.Clear();
            tester.EntryExit();
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
            Assert.AreEqual("Exit", content.ElementAt(1).Name);

            // not concerned by property attribute
            MethodAdviceContainer.Clear();
            tester.InternalProperty = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }

        /// <summary>
        /// Tests class concern attribute overwritten.
        /// </summary>
        [Test]
        public void TestClassAttributeOverwrite()
        {
            // Constructor test
            MethodAdviceContainer.Clear();
            var tester = new OverwriteTestClassPropertyConcerned();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            // setter not concerned
            MethodAdviceContainer.Clear();
            tester.InernalProperty = 1;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);

            // getter concerned
            MethodAdviceContainer.Clear();
            Console.Out.WriteLine(tester.InernalProperty);
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(2, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
            Assert.AreEqual("Exit", content.ElementAt(1).Name);

            // method not concerned
            MethodAdviceContainer.Clear();
            tester.NotConcernedMethod();
            content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(0, content.Count);
        }
    }
}

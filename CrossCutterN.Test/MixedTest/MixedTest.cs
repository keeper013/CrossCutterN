// <copyright file="MixedTest.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.MixedTest
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    /// <summary>
    /// Mixed aspect test.
    /// </summary>
    [TestFixture]
    public sealed class MixedTest
    {
        /// <summary>
        /// Tests mixed aspect weaving.
        /// </summary>
        [Test]
        public void TestMixed()
        {
            MethodAdviceContainer.Clear();
            MixedTestTarget.ConcernedByAttributeAndClassName();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(7, content.Count);
            Assert.AreEqual("EntryByAttribute1", content.ElementAt(0).Name);
            Assert.AreEqual("EntryByAttribute2", content.ElementAt(1).Name);
            Assert.AreEqual("EntryByAttribute3", content.ElementAt(2).Name);
            Assert.AreEqual("ExitByAttribute1", content.ElementAt(3).Name);
            Assert.AreEqual("ExitByNameExpression", content.ElementAt(4).Name);
            Assert.AreEqual("ExitByAttribute2", content.ElementAt(5).Name);
            Assert.AreEqual("ExitByAttribute3", content.ElementAt(6).Name);
        }
    }
}

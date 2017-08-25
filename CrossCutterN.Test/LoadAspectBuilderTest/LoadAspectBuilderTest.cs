// <copyright file="LoadAspectBuilderTest.cs" company="Cui Ziqiang">
// Copyright (c) 2017 Cui Ziqiang
// </copyright>

namespace CrossCutterN.Test.LoadAspectBuilderTest
{
    using System;
    using System.Linq;
    using CrossCutterN.Test.Utilities;
    using NUnit.Framework;

    /// <summary>
    /// Load aspect builder test
    /// </summary>
    [TestFixture]
    public sealed class LoadAspectBuilderTest
    {
        /// <summary>
        /// Test loaded aspect builder
        /// </summary>
        [Test]
        public void TestLoadedAspectBuilder()
        {
            // included method
            MethodAdviceContainer.Clear();
            LoadAspectBuilderTestTarget.MethodToBeConcerned();
            var content = MethodAdviceContainer.Content;
            MethodAdviceContainer.PrintContent(Console.Out);
            Assert.AreEqual(1, content.Count);
            Assert.AreEqual("Entry", content.ElementAt(0).Name);
        }
    }
}

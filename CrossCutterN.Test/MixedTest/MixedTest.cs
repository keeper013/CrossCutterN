/**
 * Description: mixed test
 * Author: David Cui
 */

namespace CrossCutterN.Test.MixedTest
{
    using System.Linq;
    using NUnit.Framework;
    using Utilities;

    [TestFixture]
    public class MixedTest
    {
        [Test]
        public void TestMixed()
        {
            MethodAdviceContainer.Clear();
            MixedTestTarget.ConcernedByAttributeAndClassName();
            var content = MethodAdviceContainer.Content;
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

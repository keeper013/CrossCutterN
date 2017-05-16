/**
 * Description: mixed test
 * Author: David Cui
 */

namespace CrossCutterN.Test.MixedTest
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Utilities;

    [TestClass]
    public class MixedTest
    {
        [TestMethod]
        public void TestMixed()
        {
            MethodAdviceContainer.Clear();
            MixedTestTarget.ConcernedByAttributeAndClassName();
            var content = MethodAdviceContainer.Content;
            Assert.AreEqual(3, content.Count);
            Assert.AreEqual("EntryByAttribute", content.ElementAt(0).Name);
            Assert.AreEqual("ConcernedByAttributeAndClassName", content.ElementAt(0).Execution.Name);
            Assert.AreEqual("ExitByAttribute", content.ElementAt(1).Name);
            Assert.AreEqual("ExitByNameExpression", content.ElementAt(2).Name);
            Assert.AreEqual("ConcernedByAttributeAndClassName", content.ElementAt(2).Execution.Name);
        }
    }
}

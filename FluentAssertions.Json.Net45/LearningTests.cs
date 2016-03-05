using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FluentAssertions.Json
{
    [TestFixture]
    internal class LearningTests
    {
        [Test]
        [TestCase("{id:1,admin:true}", "{admin:true,id:1}", true)]
        [TestCase("{id:2,admin:true}", "{admin:true,id:1}", false)]
        public void DiffTest(string actualJson, string expectedJson, bool equal)
        {
            var actual = JToken.Parse(actualJson);
            var expected = JToken.Parse(expectedJson);

            var diff = ObjectDiffPatch.GenerateDiff(expected, actual);
            diff.AreEqual.Should().Be(equal);
        }
    }
}
using NUnit.Framework;

namespace FluentAssertions.Json
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class JsonAssert_Should
    {
        [Test]
        [TestCase("{friends:[{id:123,name:\"Corby Page\"},{id:456,name:\"Carter Page\"}]}",
            "{friends:[{name:\"Corby Page\",id:123},{id:456,name:\"Carter Page\"}]}",
            Description = "reorder inner items")]
        [TestCase("{id:1,admin:true}", "{admin:true,id:1}")]
        public void EqualJson(string actualJson, string expectedJson)
        {
            actualJson.ShouldEqualJson(expectedJson);
        }

        [Test, Ignore("for now")]
        [TestCase("{id:1,admin:true}", "{admin:false,id:1}", "Expected property 'admin' to be 'false' but was 'true'.")]
        public void PrintPrettyFailure(string actualJson, string nonMatchingJson, string expectedMessage)
        {
            actualJson.Invoking(x => x.ShouldEqualJson(nonMatchingJson))
                .ShouldThrow<AssertionException>()
                .WithMessage(expectedMessage);
        }
    }
}
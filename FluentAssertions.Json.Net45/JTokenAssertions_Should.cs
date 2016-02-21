using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FluentAssertions.Json
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class JTokenAssertions_Should
    {
        [Test]
        [TestCase("{friends:[{id:123,name:\"Corby Page\"},{id:456,name:\"Carter Page\"}]}",
            "{friends:[{name:\"Corby Page\",id:123},{id:456,name:\"Carter Page\"}]}",
            Description = "reorder inner items")]
        [TestCase("{id:2,admin:true}", "{admin:true,id:2}")]
        public void BeEquivalentTo(string actualJson, string expectedJson)
        {
            var actual = JToken.Parse(actualJson);
            var expected = JToken.Parse(expectedJson);

            actual.Should().Be(expected);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        [TestCase("{id:1,admin:true}", "{id:1,admin:false}", "Expected property 'admin' to be 'false' but was 'true'.")]
        public void NotBeEquivalentTo(string actualJson, string expectedJson, string expectedMessage)
        {
            var actual = JToken.Parse(actualJson);
            var expected = JToken.Parse(expectedJson);

            actual.Should().NotBeEquivalentTo(expected);
            actual.Should().Invoking(x => x.BeEquivalentTo(expected))
                .ShouldThrow<AssertionException>()
                .WithMessage(expectedMessage);
        }

        [Test]
        public void Fail_with_descriptive_message_when_child_element_differs()
        {
            var subject = JToken.Parse("{child:{subject:'foo'}}");
            var expected = JToken.Parse("{child:{expected:'bar'}}");

            subject.Should().Invoking(x => x.BeEquivalentTo(expected, "we want to test the failure {0}", "message"))
                .ShouldThrow<AssertionException>()
                .WithMessage(
                    "Expected local name of element at '/child' to be 'expected' because we want to test the failure message, but found 'subject'.");
        }
    }
}
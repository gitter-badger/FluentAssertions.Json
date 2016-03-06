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
        [TestCase("{id:1,admin:true}", "{id:1,admin:false}")]
        public void NotBeEquivalentTo(string actualJson, string expectedJson)
        {
            var actual = JToken.Parse(actualJson);
            var expected = JToken.Parse(expectedJson);

            actual.Should().NotBeEquivalentTo(expected);

            var expectedMessage = ExpectedMessage(actual, expected);

            actual.Should().Invoking(x => x.BeEquivalentTo(expected))
                .ShouldThrow<AssertionException>()
                .WithMessage(expectedMessage);
        }

        [Test]
        public void Fail_with_descriptive_message_when_child_element_differs()
        {
            var subject = JToken.Parse("{child:{subject:'foo'}}");
            var expected = JToken.Parse("{child:{expected:'bar'}}");

            var expectedMessage = ExpectedMessage(subject, expected, "we want to test the failure {0}", "message");

            subject.Should().Invoking(x => x.BeEquivalentTo(expected, "we want to test the failure {0}", "message"))
                .ShouldThrow<AssertionException>()
                .WithMessage(expectedMessage);
        }

        private static string ExpectedMessage(JToken actual, JToken expected,
            string reason = null, params string[] reasonArgs)
        {
            var diff = ObjectDiffPatch.GenerateDiff(actual, expected);
            var key = diff.NewValues?.First ?? diff.OldValues?.First;
            var formatter = new JTokenFormatter();

            var because = string.Empty;
            if (!string.IsNullOrWhiteSpace(reason))
                because = " because " + string.Format(reason, reasonArgs);

            var expectedMessage = $"Expected JSON document {formatter.ToString(actual, false)}" +
                                  $" to be equivalent to {formatter.ToString(expected, false)}" +
                                  $"{because}, but differs at {formatter.ToString(key, false)}.";
            return expectedMessage;
        }
    }
}
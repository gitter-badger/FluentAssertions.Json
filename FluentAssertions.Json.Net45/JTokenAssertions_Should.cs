using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FluentAssertions.Json
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class JTokenAssertions_Should
    {
        [Test]
        public void Be()
        {
            var a = JToken.Parse("{\"id\":1}");
            var b = JToken.Parse("{\"id\":1}");

            a.Should().Be(a);
            b.Should().Be(b);
            a.Should().Be(b);

            b = JToken.Parse("{\"id\":2}");
            a.Invoking(x => x.Should().Be(b))
                .ShouldThrow<AssertionException>()
                .WithMessage($"Expected JSON document to be {_formatter.ToString(b)}, but found {_formatter.ToString(a)}.");
        }

        [Test]
        public void NotBe()
        {
            var a = JToken.Parse("{\"id\":1}");

            a.Should().NotBeNull();
            a.Should().NotBe(null);

            var b = JToken.Parse("{\"id\":1}");
            a.Invoking(x => x.Should().NotBe(b))
                .ShouldThrow<AssertionException>()
                .WithMessage($"Expected JSON document not to be {_formatter.ToString(b)}.");

            b = JToken.Parse("{\"id\":2}");
            a.Should().NotBe(b);
        }

        [Test]
        [TestCase("{friends:[{id:123,name:\"Corby Page\"},{id:456,name:\"Carter Page\"}]}",
            "{friends:[{name:\"Corby Page\",id:123},{id:456,name:\"Carter Page\"}]}",
            Description = "reorder inner items")]
        [TestCase("{id:2,admin:true}", "{admin:true,id:2}")]
        public void BeEquivalentTo(string actualJson, string expectedJson)
        {
            var a = JToken.Parse(actualJson);
            var b = JToken.Parse(expectedJson);
            a.Should().BeEquivalentTo(b);
        }

        [Test]
        [TestCase("{id:1,admin:true}", "{id:1,admin:false}")]
        public void NotBeEquivalentTo(string actualJson, string expectedJson)
        {
            var a = JToken.Parse(actualJson);
            var b = JToken.Parse(expectedJson);

            a.Should().NotBeEquivalentTo(b);

            var expectedMessage = GetNotEquivalentMessage(a, b);

            a.Should().Invoking(x => x.BeEquivalentTo(b))
                .ShouldThrow<AssertionException>()
                .WithMessage(expectedMessage);

            expectedMessage =
                $"Expected JSON document to be {_formatter.ToString(b)}, but found {_formatter.ToString(a)}.";
            a.Should().Invoking(x => x.Be(b))
                .ShouldThrow<AssertionException>()
                .WithMessage(expectedMessage);
        }

        [Test]
        public void Fail_with_descriptive_message_when_child_element_differs()
        {
            var subject = JToken.Parse("{child:{subject:'foo'}}");
            var expected = JToken.Parse("{child:{expected:'bar'}}");

            var expectedMessage = GetNotEquivalentMessage(subject, expected, "we want to test the failure {0}", "message");

            subject.Should().Invoking(x => x.BeEquivalentTo(expected, "we want to test the failure {0}", "message"))
                .ShouldThrow<AssertionException>()
                .WithMessage(expectedMessage);
        }

        [Test]
        public void HaveValue()
        {
            var subject = JToken.Parse("{ 'id':42}");
            subject["id"].Should().HaveValue("42");

            subject["id"].Should().Invoking(x => x.HaveValue("43", "because foo"))
                .ShouldThrow<AssertionException>()
                .WithMessage("Expected JSON property \"id\" to have value \"43\" because foo, but found \"42\".");
        }

        [Test]
        public void HaveElement()
        {
            var subject = JToken.Parse("{ 'id':42}");
            subject.Should().HaveElement("id");

            subject.Should().Invoking(x => x.HaveElement("name", "because foo"))
                .ShouldThrow<AssertionException>()
                .WithMessage($"Expected JSON document {_formatter.ToString(subject)} to have element \"name\" because foo, but no such element was found.");
        }

        private static readonly JTokenFormatter _formatter = new JTokenFormatter();

        private static string GetNotEquivalentMessage(JToken actual, JToken expected,
            string reason = null, params object[] reasonArgs)
        {
            var diff = ObjectDiffPatch.GenerateDiff(actual, expected);
            var key = diff.NewValues?.First ?? diff.OldValues?.First;

            var because = string.Empty;
            if (!string.IsNullOrWhiteSpace(reason))
                because = " because " + string.Format(reason, reasonArgs);

            var expectedMessage = $"Expected JSON document {_formatter.ToString(actual)}" +
                                  $" to be equivalent to {_formatter.ToString(expected)}" +
                                  $"{because}, but differs at {_formatter.ToString(key)}.";
            return expectedMessage;
        }
    }
}
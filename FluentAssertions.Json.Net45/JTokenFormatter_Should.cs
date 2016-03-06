using FluentAssertions.Common;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FluentAssertions.Json
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class JTokenFormatter_Should
    {
        [Test]
        public void Handle_JToken()
        {
            var sut = new JTokenFormatter();
            sut.CanHandle(JToken.Parse("{}")).Should().BeTrue();
            sut.CanHandle(null).Should().BeFalse();
            sut.CanHandle("").Should().BeFalse();
        }

        [Test]
        public void Remove_line_breaks_and_indenting()
        {
            var json = JToken.Parse("{ \"id\":1 }");
            var sut = new JTokenFormatter();
            sut.ToString(json, false).Should().Be(json.ToString().RemoveNewLines());
            sut.ToString(json, true).Should().Be(json.ToString(Newtonsoft.Json.Formatting.Indented));
        }
    }
}
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    [DebuggerNonUserCode]
    public static class JsonAssertionExtensions
    {
        public static void ShouldEqualJson(this string jsonResponse, string expectedJson, string because = "")
        {
            var actual = JToken.Parse(jsonResponse);
            var expected = JToken.Parse(expectedJson);
            actual.Should().Be(expected, because);
        }

        public static JTokenAssertions Should(this JToken jToken)
        {
            return new JTokenAssertions(jToken);
        }
    }
}
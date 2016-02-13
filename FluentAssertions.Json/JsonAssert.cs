using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    public static class JsonAssert
    {
        public static void ShouldEqualJson(this string jsonResponse, string expectedJson)
        {
            var a = JToken.Parse(jsonResponse);
            var b = JToken.Parse(expectedJson);
            JToken.DeepEquals(a, b).Should().BeTrue();
        }
    }
}

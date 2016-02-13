﻿using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    public static class JsonAssert
    {
        public static void ShouldEqualJson(this string jsonResponse, string expectedJson, string because = "")
        {
            var a = JToken.Parse(jsonResponse);
            var b = JToken.Parse(expectedJson);
            if (JToken.DeepEquals(a, b)) return;

            // prettify failure using FluentAssertion goodness
            var aStr = a.ToString(Newtonsoft.Json.Formatting.Indented);
            var bStr = b.ToString(Newtonsoft.Json.Formatting.Indented);
            aStr.Should().Be(bStr, because);
        }
    }
}
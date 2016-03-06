using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    [DebuggerNonUserCode]
    public static class JsonAssertionExtensions
    {
        public static JTokenAssertions Should(this JToken jToken)
        {
            return new JTokenAssertions(jToken);
        }
    }
}
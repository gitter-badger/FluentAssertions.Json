using System;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    public class JTokenAssertions
    {
        private readonly JToken _jToken;

        public JTokenAssertions(JToken jToken)
        {
            if (jToken == null) throw new ArgumentNullException(nameof(jToken));
            _jToken = jToken;
        }
    }
}
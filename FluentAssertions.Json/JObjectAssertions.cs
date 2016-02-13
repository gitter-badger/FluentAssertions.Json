using System;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    public class JObjectAssertions
    {
        private readonly JObject _jObject;

        public JObjectAssertions(JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));
            _jObject = jObject;
        }
    }
}
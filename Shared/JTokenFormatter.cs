using System.Collections.Generic;
using FluentAssertions.Common;
using FluentAssertions.Formatting;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    public class JTokenFormatter : IValueFormatter
    {
        public bool CanHandle(object value)
        {
            return value is JToken;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value for which to create a <see cref="string"/>.</param>
        /// <param name="useLineBreaks"> </param>
        /// <param name="processedObjects">
        /// A collection of objects that 
        /// </param>
        /// <param name="nestedPropertyLevel">
        /// The level of nesting for the supplied value. This is used for indenting the format string for objects that have
        /// no <see cref="object.ToString()"/> override.
        /// </param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(object value, bool useLineBreaks, IList<object> processedObjects = null,
            int nestedPropertyLevel = 0)
        {
            var jToken = (JToken)value;
            var formatted = jToken.ToString(Newtonsoft.Json.Formatting.Indented);
            if (!useLineBreaks)
                formatted = formatted.RemoveNewLines();
            return formatted;
        }
    }
}
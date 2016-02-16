using System;
using System.Diagnostics;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    /// <summary>
    ///     Contains a number of methods to assert that an <see cref="JObject" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class JObjectAssertions : ReferenceTypeAssertions<JObject, JObjectAssertions>
    {
        public JObjectAssertions(JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException(nameof(jObject));
            Subject = jObject;
        }


        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context => nameof(JObject);

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> equals the <paramref name="expected" /> document,
        ///     using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">The expected document</param>
        public AndConstraint<JObjectAssertions> Be(JObject expected)
        {
            return Be(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> equals the <paramref name="expected" /> document,
        ///     using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="expected">The expected document</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JObjectAssertions> Be(JObject expected, string because, params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.IsSameOrEqualTo(expected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected JSON document to be {0}{reason}, but found {1}", expected, Subject);

            return new AndConstraint<JObjectAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> does not equal the <paramref name="unexpected" /> document,
        ///     using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected document</param>
        public AndConstraint<JObjectAssertions> NotBe(JObject unexpected)
        {
            return NotBe(unexpected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> does not equal the <paramref name="unexpected" /> document,
        ///     using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected document</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JObjectAssertions> NotBe(JObject unexpected, string because, params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(!ReferenceEquals(Subject, null))
                .BecauseOf(because, reasonArgs)
                .FailWith("Did not expect JSON document to be {0}, but found <null>.", unexpected);

            Execute.Assertion
                .ForCondition(!Subject.Equals(unexpected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Did not expect JSON document to be {0}{reason}.", unexpected);

            return new AndConstraint<JObjectAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> is equivalent to the <paramref name="expected" /> document,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="expected">The expected document</param>
        public AndConstraint<JObjectAssertions> BeEquivalentTo(JObject expected)
        {
            return BeEquivalentTo(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> is equivalent to the <paramref name="expected" /> document,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="expected">The expected document</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JObjectAssertions> BeEquivalentTo(JObject expected, string because,
            params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(JToken.DeepEquals(Subject, expected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected JSON document {0} to be equivalent to {1}{reason}.",
                    Subject, expected);

            return new AndConstraint<JObjectAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> is not equivalent to the <paramref name="unexpected" /> document,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected document</param>
        public AndConstraint<JObjectAssertions> NotBeEquivalentTo(JObject unexpected)
        {
            return NotBeEquivalentTo(unexpected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> is not equivalent to the <paramref name="unexpected" /> document,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected document</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JObjectAssertions> NotBeEquivalentTo(JObject unexpected, string because,
            params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(!JToken.DeepEquals(Subject, unexpected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Did not expect JSON document {0} to be equivalent to {1}{reason}.",
                    Subject, unexpected);

            return new AndConstraint<JObjectAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> has a root element with the specified
        ///     <paramref name="expected" /> name.
        /// </summary>
        /// <param name="expected">The name of the expected root element of the current document.</param>
        public AndWhichConstraint<JObjectAssertions, JToken> HaveRoot(string expected)
        {
            return HaveRoot(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JObject" /> has a root element with the specified
        ///     <paramref name="expected" /> name.
        /// </summary>
        /// <param name="expected">The full name of the expected root element of the current document.</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<JObjectAssertions, JToken> HaveRoot(string expected, string because,
            params object[] reasonArgs)
        {
            if (Subject == null)
            {
                throw new ArgumentNullException(nameof(Subject),
                    "Cannot assert the document has a root element if the document itself is <null>.");
            }

            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected),
                    "Cannot assert the document has a root element if the element name is <null>*");
            }

            var root = Subject.Root;

            Execute.Assertion
                .ForCondition((root != null) && (root.Path == expected))
                .BecauseOf(because, reasonArgs)
                .FailWith(
                    "Expected JSON document to have root element \"" + expected.Escape(true) + "\"{reason}" +
                    ", but found {0}.", Subject);

            return new AndWhichConstraint<JObjectAssertions, JToken>(this, root);
        }

        /// <summary>
        ///     Asserts that the <see cref="JObject.Root" /> element of the current <see cref="JObject" /> has a direct
        ///     child element with the specified <paramref name="expected" /> name.
        /// </summary>
        /// <param name="expected">
        ///     The name of the expected child element of the current document's Root <see cref="JObject.Root" /> element.
        /// </param>
        public AndWhichConstraint<JObjectAssertions, JToken> HaveElement(string expected)
        {
            return HaveElement(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the <see cref="JObject.Root" /> element of the current <see cref="JObject" /> has a direct
        ///     child element with the specified <paramref name="expected" /> name.
        /// </summary>
        /// <param name="expected">
        ///     The full name of the expected child element of the current document's Root
        ///     <see cref="JObject.Root" /> element.
        /// </param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<JObjectAssertions, JToken> HaveElement(string expected, string because,
            params object[] reasonArgs)
        {
            if (Subject == null)
            {
                throw new ArgumentNullException(nameof(Subject),
                    "Cannot assert the document has an element if the document itself is <null>.");
            }

            if (expected == null)
            {
                throw new ArgumentNullException(nameof(expected),
                    "Cannot assert the document has an element if the element name is <null>*");
            }

            var expectedText = expected.Escape(true);

            Execute.Assertion
                .ForCondition(Subject.Root != null)
                .BecauseOf(because, reasonArgs)
                .FailWith(
                    "Expected JSON document {0} to have root element with child \"" + expectedText + "\"{reason}" +
                    ", but JSON document has no Root element.", Subject);

            var jToken = Subject.Root[expected];
            Execute.Assertion
                .ForCondition(jToken != null)
                .BecauseOf(because, reasonArgs)
                .FailWith(
                    "Expected JSON document {0} to have root element with child \"" + expectedText + "\"{reason}" +
                    ", but no such child element was found.", Subject);

            return new AndWhichConstraint<JObjectAssertions, JToken>(this, jToken);
        }
    }
}
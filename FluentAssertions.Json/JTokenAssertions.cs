﻿using System;
using System.Diagnostics;
using FluentAssertions.Common;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using Newtonsoft.Json.Linq;

namespace FluentAssertions.Json
{
    /// <summary>
    ///     Contains a number of methods to assert that an <see cref="JToken" /> is in the expected state.
    /// </summary>
    [DebuggerNonUserCode]
    public class JTokenAssertions : ReferenceTypeAssertions<JToken, JTokenAssertions>
    {
        public JTokenAssertions(JToken jToken)
        {
            if (jToken == null) throw new ArgumentNullException(nameof(jToken));
            Subject = jToken;
        }

        /// <summary>
        ///     Returns the type of the subject the assertion applies on.
        /// </summary>
        protected override string Context => nameof(JToken);

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> equals the <paramref name="expected" /> element.
        /// </summary>
        /// <param name="expected">The expected element</param>
        public AndConstraint<JTokenAssertions> Be(JToken expected)
        {
            return Be(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> equals the <paramref name="expected" /> element.
        /// </summary>
        /// <param name="expected">The expected element</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JTokenAssertions> Be(JToken expected, string because, params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(JToken.DeepEquals(Subject, expected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected JSON element to be {0}{reason}, but found {1}.", expected, Subject);

            return new AndConstraint<JTokenAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> does not equal the <paramref name="unexpected" /> element,
        ///     using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected element</param>
        public AndConstraint<JTokenAssertions> NotBe(JToken unexpected)
        {
            return NotBe(unexpected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> does not equal the <paramref name="unexpected" /> element,
        ///     using its <see cref="object.Equals(object)" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected element</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JTokenAssertions> NotBe(JToken unexpected, string because, params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition((ReferenceEquals(Subject, null) && !ReferenceEquals(unexpected, null)) ||
                              !JToken.DeepEquals(Subject, unexpected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected JSON element not to be {0}{reason}.", unexpected);

            return new AndConstraint<JTokenAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> is equivalent to the <paramref name="expected" /> element,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="expected">The expected element</param>
        public AndConstraint<JTokenAssertions> BeEquivalentTo(JToken expected)
        {
            return BeEquivalentTo(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> is equivalent to the <paramref name="expected" /> element,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="expected">The expected element</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JTokenAssertions> BeEquivalentTo(JToken expected, string because,
            params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(JToken.DeepEquals(Subject, expected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected JSON element {0} to be equivalent to {1}{reason}.",
                    Subject, expected);

            return new AndConstraint<JTokenAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> is not equivalent to the <paramref name="unexpected" /> element,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected element</param>
        public AndConstraint<JTokenAssertions> NotBeEquivalentTo(JToken unexpected)
        {
            return NotBeEquivalentTo(unexpected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> is not equivalent to the <paramref name="unexpected" /> element,
        ///     using its <see cref="JToken.DeepEquals()" /> implementation.
        /// </summary>
        /// <param name="unexpected">The unexpected element</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JTokenAssertions> NotBeEquivalentTo(JToken unexpected, string because,
            params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(!JToken.DeepEquals(Subject, unexpected))
                .BecauseOf(because, reasonArgs)
                .FailWith("Did not expect JSON element {0} to be equivalent to {1}{reason}.",
                    Subject, unexpected);

            return new AndConstraint<JTokenAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> has the specified <paramref name="expected" /> value.
        /// </summary>
        /// <param name="expected">The expected value</param>
        public AndConstraint<JTokenAssertions> HaveValue(string expected)
        {
            return HaveValue(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> has the specified <paramref name="expected" /> value.
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndConstraint<JTokenAssertions> HaveValue(string expected, string because, params object[] reasonArgs)
        {
            Execute.Assertion
                .ForCondition(Subject.Value<string>() == expected)
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected JSON element '{0}' to have value {1}{reason}, but found {2}.",
                    Subject.Path, expected, Subject.Value<string>());

            return new AndConstraint<JTokenAssertions>(this);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> has a direct child element with the specified
        ///     <paramref name="expected" /> name.
        /// </summary>
        /// <param name="expected">The name of the expected child element</param>
        public AndWhichConstraint<JTokenAssertions, JToken> HaveElement(string expected)
        {
            return HaveElement(expected, string.Empty);
        }

        /// <summary>
        ///     Asserts that the current <see cref="JToken" /> has a direct child element with the specified
        ///     <paramref name="expected" /> name.
        /// </summary>
        /// <param name="expected">The name of the expected child element</param>
        /// <param name="because">
        ///     A formatted phrase as is supported by <see cref="string.Format(string,object[])" /> explaining why the assertion
        ///     is needed. If the phrase does not start with the word <i>because</i>, it is prepended automatically.
        /// </param>
        /// <param name="reasonArgs">
        ///     Zero or more objects to format using the placeholders in <see cref="because" />.
        /// </param>
        public AndWhichConstraint<JTokenAssertions, JToken> HaveElement(string expected, string because,
            params object[] reasonArgs)
        {
            var jToken = Subject[expected];
            Execute.Assertion
                .ForCondition(jToken != null)
                .BecauseOf(because, reasonArgs)
                .FailWith("Expected JSON element {0} to have child element \"" + expected.Escape(true) + "\"{reason}" +
                          ", but no such child element was found.", Subject);

            return new AndWhichConstraint<JTokenAssertions, JToken>(this, jToken);
        }
    }
}
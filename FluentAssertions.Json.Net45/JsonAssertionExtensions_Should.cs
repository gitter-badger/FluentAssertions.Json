using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FluentAssertions.Json
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class JsonAssertionExtensions_Should
    {
        [Test]
        public void Provide_assertions()
        {
            var sut = JToken.Parse("{}").Should();
            sut.Should().BeOfType<JTokenAssertions>();
        }
    }
}
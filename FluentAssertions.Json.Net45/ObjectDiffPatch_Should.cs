using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace FluentAssertions.Json
{
    [TestFixture]
    // ReSharper disable InconsistentNaming
    internal class ObjectDiffPatch_Should
    {
        [Test]
        public void Generate_Diffs_and_Patch_for_POCOs_and_JObjects()
        {
            var sut = ObjectDiffPatch.GenerateDiff<Dummy>(null, null);
            sut.AreEqual.Should().BeTrue();
            sut.OldValues.Should().BeNull();
            sut.NewValues.Should().BeNull();

            var a = new Dummy { Id = "foo" };
            var ja = JObject.FromObject(a);

            sut = ObjectDiffPatch.GenerateDiff(a, null);
            sut.AreEqual.Should().BeFalse();
            sut.OldValues.Should().Be(ja);
            sut.NewValues.Should().BeNull();

            sut = ObjectDiffPatch.GenerateDiff(null, a);
            sut.AreEqual.Should().BeFalse();
            sut.OldValues.Should().BeNull();
            sut.NewValues.Should().Be(ja);

            var b = new Dummy { Id = "bar" };
            var jb = JObject.FromObject(b);
            sut = ObjectDiffPatch.GenerateDiff(a, b);
            sut.AreEqual.Should().BeFalse();
            JToken.DeepEquals(sut.OldValues, ja).Should().BeTrue();
            JToken.DeepEquals(sut.NewValues, jb).Should().BeTrue();

            var c = ObjectDiffPatch.PatchObject(a, sut.NewValues);
            c.ShouldBeEquivalentTo(b);

            c = ObjectDiffPatch.PatchObject(a, sut.NewValues.ToString());
            c.ShouldBeEquivalentTo(b);

            // now for JObjects
            sut = ObjectDiffPatch.GenerateDiff(ja, jb);
            sut.AreEqual.Should().BeFalse();
            JToken.DeepEquals(sut.OldValues, ja).Should().BeTrue();
            JToken.DeepEquals(sut.NewValues, jb).Should().BeTrue();
        }


        private class Dummy
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public string Id { get; set; }
        }
    }
}
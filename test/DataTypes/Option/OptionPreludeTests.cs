using FluentAssertions;
using NUnit.Framework;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class OptionPreludeTests
    {
        [Test]
        public void Some_CreaateSome()
            => Some("not-empty")
                .IsSome
                .Should().BeTrue();

        [Test]
        public void None_CreateNone()
            => None<string>()
                .IsNone
                .Should().BeTrue();
    }
}
using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class EotherPreludeTests
    {
        [Test]
        public void Right_CreaateRight()
            => Right<string, int>(10)
                .IsRight
                .Should().BeTrue();

        [Test]
        public void Fail_CreateFailValidation()
            => Left<string, int>("failed")
                .IsLeft
                .Should().BeTrue();
    }
}
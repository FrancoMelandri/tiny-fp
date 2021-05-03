using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class ValidationPreludeTests
    {
        [Test]
        public void Success_CreateSuccessValidation()
            => Success<string, Unit>(Unit.Default)
                .IsSuccess
                .Should().BeTrue();

        [Test]
        public void Fail_CreateFailValidation()
            => Fail<string, Unit>("failed")
                .IsFail
                .Should().BeTrue();
    }
}
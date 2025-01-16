using NUnit.Framework;
using Shouldly;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class ValidationPreludeTests
{
    [Test]
    public void Success_CreateSuccessValidation()
        => Success<string, Unit>(Unit.Default)
            .IsSuccess
            .ShouldBeTrue();

    [Test]
    public void Fail_CreateFailValidation()
        => Fail<string, Unit>("failed")
            .IsFail
            .ShouldBeTrue();
}
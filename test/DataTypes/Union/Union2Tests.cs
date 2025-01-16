using NUnit.Framework;
using Shouldly;
using TinyFp;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class Union2Tests
{
    [Test]
    public void FromT1_CreateT1Object()
    {
        var t1 = Union<int, string>.FromT1(42);

        t1.IsT1.ShouldBeTrue();
        t1.IsT2.ShouldBeFalse();
    }

    [Test]
    public void FromT1_WhenNull_RaiseException()
    {
        Action act = () => Union<object, object>.FromT1(null);

        act.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void FromT2_CreateT1Object()
    {
        var t2 = Union<int, string>.FromT2("t2");

        t2.IsT1.ShouldBeFalse();
        t2.IsT2.ShouldBeTrue();
    }

    [Test]
    public void FromT2_WhenNull_RaiseException()
    {
        Action act = () => Union<object, object>.FromT2(null);

        act.ShouldThrow<ArgumentNullException>();
    }

    [Test]
    public void Match_WhenT1_ToOutput()
        => Union<int, string>.FromT1(42)
            .Match(_ => true, _ => false)
            .ShouldBeTrue();

    [Test]
    public void Match_WhenT2_ToOutput()
        => Union<int, string>.FromT2("union")
            .Match(_ => false, _ => true)
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_WhenT1_ToOutput()
        => Union<int, string>.FromT1(42)
            .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_WhenT2_ToOutput()
        => Union<int, string>.FromT2("union")
            .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void WhenT1_ImplicitCast()
    {
        Union<int, string> result = 1;

        result.IsT1.ShouldBeTrue();
        result.IsT2.ShouldBeFalse();
    }

    [Test]
    public void WhenT2_ImplicitCast()
    {
        Union<int, string> result = "union";

        result.IsT2.ShouldBeTrue();
        result.IsT1.ShouldBeFalse();
    }
}
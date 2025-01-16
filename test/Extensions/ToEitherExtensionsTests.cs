using NUnit.Framework;
using Shouldly;
using static TinyFp.Extensions.Functional;

namespace TinyFpTest.Extensions;

[TestFixture]
public class ToEitherExtensionsTests
{
    [Test]
    public void ToEither_WithMapAndWhen_WhenNoValue_AndWhenFalse_Left()
    {
        var sut = ((string)null).ToEither(_ => 10, _ => false, 0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_WithMapAndWhen_WhenNoValue_AndWhenTrue_Left()
    {
        var sut = ((string)null).ToEither(_ => 10, _ => true, 0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_WithMapAndWhen_WhenValue_AndWhenTrue_Left()
    {
        var sut = "not-empty".ToEither(_ => 10, _ => true, 0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_WithMapAndWhen_WhenValue_AndWhenFalse_Right()
    {
        var sut = "not-empty".ToEither(_ => 10, _ => false, 0);

        sut.IsRight.ShouldBeTrue();
        sut.OnRight(_ => _.ShouldBe(10));
    }

    [Test]
    public void ToEither_Func_WithMapAndWhen_WhenNoValue_AndWhenFalse_Left()
    {
        var sut = ((string)null).ToEither(_ => 10, _ => false, () => 0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_Func_WithMapAndWhen_WhenNoValue_AndWhenTrue_Left()
    {
        var sut = ((string)null).ToEither(_ => 10, _ => true, () => 0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_Func_WithMapAndWhen_WhenValue_AndWhenTrue_Left()
    {
        var sut = "not-empty".ToEither(_ => 10, _ => true, () => 0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_Func_WithMapAndWhen_WhenValue_AndWhenFalse_Right()
    {
        var sut = "not-empty".ToEither(_ => 10, _ => false, () => 0);

        sut.IsRight.ShouldBeTrue();
        sut.OnRight(_ => _.ShouldBe(10));
    }

    [Test]
    public void ToEither_WhenNoValue_Left()
    {
        var sut = ((string)null).ToEither(0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_WhenValue_Right()
        => "not-empty".ToEither(0)
            .IsRight.ShouldBeTrue();

    [Test]
    public void ToEither_Func_WhenNoValue_Left()
    {
        var sut = ((string)null).ToEither(() => 0);

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEither_Func_WhenValue_Right()
        => "not-empty".ToEither(() => 0)
            .IsRight.ShouldBeTrue();

    [Test]
    public void ToEitherAsync_WithMapAndWhen_WhenNoValue_AndWhenFalse_Left()
    {
        var sut = Task.FromResult((string)null).ToEitherAsync(_ => 10, _ => false, 0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_WithMapAndWhen_WhenNoValue_AndWhenTrue_Left()
    {
        var sut = Task.FromResult((string)null).ToEitherAsync(_ => 10, _ => true, 0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_WithMapAndWhen_WhenValue_AndWhenTrue_Left()
    {
        var sut = Task.FromResult("not-empty").ToEitherAsync(_ => 10, _ => true, 0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_WithMapAndWhen_WhenValue_AndWhenFalse_Right()
    {
        var sut = Task.FromResult("not-empty").ToEitherAsync(_ => 10, _ => false, 0).Result;

        sut.IsRight.ShouldBeTrue();
        sut.OnRight(_ => _.ShouldBe(10));
    }

    [Test]
    public void ToEitherAsync_Func_WithMapAndWhen_WhenNoValue_AndWhenFalse_Left()
    {
        var sut = Task.FromResult((string)null).ToEitherAsync(_ => 10, _ => false, () => 0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_Func_WithMapAndWhen_WhenNoValue_AndWhenTrue_Left()
    {
        var sut = Task.FromResult((string)null).ToEitherAsync(_ => 10, _ => true, () => 0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_Func_WithMapAndWhen_WhenValue_AndWhenTrue_Left()
    {
        var sut = Task.FromResult("not-empty").ToEitherAsync(_ => 10, _ => true, () => 0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_Func_WithMapAndWhen_WhenValue_AndWhenFalse_Right()
    {
        var sut = Task.FromResult("not-empty").ToEitherAsync(_ => 10, _ => false, () => 0).Result;

        sut.IsRight.ShouldBeTrue();
        sut.OnRight(_ => _.ShouldBe(10));
    }

    [Test]
    public void ToEitherAsync_WhenNoValue_Left()
    {
        var sut = Task.FromResult((string)null).ToEitherAsync(0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_WhenValue_Right()
        => Task.FromResult("not-empty").ToEitherAsync(0).Result
            .IsRight.ShouldBeTrue();

    [Test]
    public void ToEitherAsync_Func_WhenNoValue_Left()
    {
        var sut = Task.FromResult((string)null).ToEitherAsync(() => 0).Result;

        sut.IsLeft.ShouldBeTrue();
        sut.OnLeft(_ => _.ShouldBe(0));
    }

    [Test]
    public void ToEitherAsync_Func_WhenValue_Right()
        => Task.FromResult("not-empty").ToEitherAsync(() => 0).Result
            .IsRight.ShouldBeTrue();
}
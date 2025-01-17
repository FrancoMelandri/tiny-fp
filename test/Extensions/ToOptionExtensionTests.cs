﻿using NUnit.Framework;
using Shouldly;
using TinyFp.Extensions;

namespace TinyFpTest.Extensions;

[TestFixture]
public class ToOptionExtensionTests
{
    [Test]
    public void ToOption_WithMapAndWhenNone_WhenNoValue_AndWhenNoneFalse_None()
    {
        var value = (string)null;
        var option = value.ToOption(_ => Convert.ToInt32(_), _ => false);

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WithMapAndWhenNone_WhenNoValue_AndWhenNoneTrue_None()
    {
        var value = (string)null;
        var option = value.ToOption(_ => Convert.ToInt32(_), _ => true);

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WithMapAndWhenNone_WhenValue_AndWhenNoneTrue_None()
    {
        var value = "42";
        var option = value.ToOption(_ => Convert.ToInt32(_), _ => true);

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WithMapAndWhenNone_WhenValue_AndWhenNoneFalse_SomeConverted()
    {
        var value = "42";
        var option = value.ToOption(_ => Convert.ToInt32(_), _ => false);

        option.IsSome.ShouldBeTrue();
        option.OnSome(_ => _.ShouldBe(42));
    }

    [Test]
    public void ToOption_WithMapAndWhenNone_WhenValue_AndWhenNoneFalse_Some()
    {
        var value = "42";
        var option = value.ToOption(_ => Convert.ToInt32(_), _ => false);

        option.IsSome.ShouldBeTrue();
        option.OnSome(_ => _.ShouldBe(42));
    }

    [Test]
    public void ToOption_WithWhenNone_WhenNoValue_AndWhenNoneTrue_None()
    {
        var value = (string)null;
        var option = value.ToOption(_ => true);

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WithWhenNone_WhenNoValue_AndWhenNoneFalse_None()
    {
        var value = (string)null;
        var option = value.ToOption(_ => false);

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WithWhenNone_WhenValue_AndWhenNoneTrue_None()
    {
        var value = "42";
        var option = value.ToOption(_ => true);

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WithWhenNone_WhenValue_AndWhenNoneFalse_Some()
    {
        var value = "42";
        var option = value.ToOption(_ => false);

        option.IsSome.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WhenNoValue_None()
    {
        var value = (string)null;
        var option = value.ToOption();

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOption_WhenValue_Some()
    {
        var value = "42";
        var option = value.ToOption(_ => false);

        option.IsSome.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WithMapAndWhenNone_WhenNoValue_AndWhenNoneFalse_None()
    {
        var value = Task.FromResult((string)null);
        var option = value.ToOptionAsync(_ => Convert.ToInt32(_), _ => false).Result;

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WithMapAndWhenNone_WhenNoValue_AndWhenNoneTrue_None()
    {
        var value = Task.FromResult((string)null);
        var option = value.ToOptionAsync(_ => Convert.ToInt32(_), _ => true).Result;

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WithMapAndWhenNone_WhenValue_AndWhenNoneTrue_None()
    {
        var value = Task.FromResult("42");
        var option = value.ToOptionAsync(_ => Convert.ToInt32(_), _ => true).Result;

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WithMapAndWhenNone_WhenValue_AndWhenNoneFalse_SomeConverted()
    {
        var value = Task.FromResult("42");
        var option = value.ToOptionAsync(_ => Convert.ToInt32(_), _ => false).Result;

        option.IsSome.ShouldBeTrue();
        option.OnSome(_ => _.ShouldBe(42));
    }

    [Test]
    public void ToOptionAsync_WithMapAndWhenNone_WhenValue_AndWhenNoneFalse_Some()
    {
        var value = Task.FromResult("42");
        var option = value.ToOptionAsync(_ => Convert.ToInt32(_), _ => false).Result;

        option.IsSome.ShouldBeTrue();
        option.OnSome(_ => _.ShouldBe(42));
    }

    [Test]
    public void ToOptionAsync_WithWhenNone_WhenNoValue_AndWhenNoneTrue_None()
    {
        var value = Task.FromResult((string)null);
        var option = value.ToOptionAsync(_ => true).Result;

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WithWhenNone_WhenNoValue_AndWhenNoneFalse_None()
    {
        var value = Task.FromResult((string)null);
        var option = value.ToOptionAsync(_ => false).Result;

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WithWhenNone_WhenValue_AndWhenNoneTrue_None()
    {
        var value = Task.FromResult("42");
        var option = value.ToOptionAsync(_ => true).Result;

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WithWhenNone_WhenValue_AndWhenNoneFalse_Some()
    {
        var value = Task.FromResult("42");
        var option = value.ToOptionAsync(_ => false).Result;

        option.IsSome.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WhenNoValue_None()
    {
        var value = Task.FromResult((string)null);
        var option = value.ToOptionAsync().Result;

        option.IsNone.ShouldBeTrue();
    }

    [Test]
    public void ToOptionAsync_WhenValue_Some()
    {
        var value = Task.FromResult("42");
        var option = value.ToOptionAsync(_ => false).Result;

        option.IsSome.ShouldBeTrue();
    }
}
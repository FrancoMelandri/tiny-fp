using FluentAssertions;
using NUnit.Framework;
using TinyFp.Extensions;

namespace TinyFpTest.Extensions;

internal class ApplyExtensionsTest
{
    [Test]
    public void Apply_ShouldFixFirstParameter()
    {
        Func<int, int, int> addFunc = (x, y) => x + y;
        var fixedValue = 5;

        var partiallyAppliedFunc = addFunc.Apply(fixedValue);
        var result = partiallyAppliedFunc(3);

        result.Should().Be(8);
    }

    [Test]
    public void Apply_ShouldWorkWithDifferentTypes()
    {
        Func<string, int, string> repeatFunc = (str, count) => string.Concat(Enumerable.Repeat(str, count));
        var fixedValue = "a";

        var partiallyAppliedFunc = repeatFunc.Apply(fixedValue);
        var result = partiallyAppliedFunc(3);

        result.Should().Be("aaa");
    }

    [Test]
    public void Apply_ShouldWorkWithDifferentReturnType()
    {
        Func<string, int, bool> checkLengthFunc = (str, len) => str.Length == len;
        var fixedValue = "hello";

        var partiallyAppliedFunc = checkLengthFunc.Apply(fixedValue);
        var result = partiallyAppliedFunc(5);

        result.Should().BeTrue();
    }

    [Test]
    public void Apply_WithThreeParameters_ShouldFixFirstParameter()
    {
        Func<int, int, int, int> addFunc = (x, y, z) => x + y + z;
        var fixedValue = 5;

        var partiallyAppliedFunc = addFunc.Apply(fixedValue);
        var result = partiallyAppliedFunc(3, 2);

        result.Should().Be(10);
    }

    [Test]
    public void Apply_WithThreeParameters_ShouldWorkWithDifferentTypes()
    {
        Func<string, int, int, string> repeatFunc = (str, count1, count2) => string.Concat(Enumerable.Repeat(str, count1 + count2));
        var fixedValue = "a";

        var partiallyAppliedFunc = repeatFunc.Apply(fixedValue);
        var result = partiallyAppliedFunc(2, 3);

        result.Should().Be("aaaaa");
    }

    [Test]
    public void Apply_WithFourParameters_ShouldFixFirstParameter()
    {
        Func<int, int, int, int, int> addFunc = (w, x, y, z) => w + x + y + z;
        var fixedValue = 5;

        var partiallyAppliedFunc = addFunc.Apply(fixedValue);
        var result = partiallyAppliedFunc(3, 2, 1);

        result.Should().Be(11);
    }

    [Test]
    public void Apply_WithFourParameters_ShouldWorkWithDifferentTypes()
    {
        Func<string, int, int, int, string> repeatFunc = (str, count1, count2, count3) => string.Concat(Enumerable.Repeat(str, count1 + count2 + count3));
        var fixedValue = "a";

        var partiallyAppliedFunc = repeatFunc.Apply(fixedValue);
        var result = partiallyAppliedFunc(1, 2, 3);

        result.Should().Be("aaaaaa");
    }
}

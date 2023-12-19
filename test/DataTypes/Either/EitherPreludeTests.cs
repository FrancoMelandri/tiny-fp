using FluentAssertions;
using NUnit.Framework;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class EitherPreludeTests
{
    [Test]
    public void Right_CreateRight()
        => Right<string, int>(10)
            .IsRight
            .Should().BeTrue();

    [Test]
    public void Left_CreateLeft()
        => Left<string, int>("failed")
            .IsLeft
            .Should().BeTrue();
}
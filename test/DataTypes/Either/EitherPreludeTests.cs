using NUnit.Framework;
using Shouldly;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class EitherPreludeTests
{
    [Test]
    public void Right_CreateRight()
        => Right<string, int>(10)
            .IsRight
            .ShouldBeTrue();

    [Test]
    public void Left_CreateLeft()
        => Left<string, int>("failed")
            .IsLeft
            .ShouldBeTrue();
}
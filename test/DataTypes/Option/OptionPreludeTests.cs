using NUnit.Framework;
using Shouldly;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class OptionPreludeTests
{
    [Test]
    public void Some_CreateSome()
        => Some("not-empty")
            .IsSome
            .ShouldBeTrue();

    [Test]
    public void None_CreateNone()
        => None<string>()
            .IsNone
            .ShouldBeTrue();
}
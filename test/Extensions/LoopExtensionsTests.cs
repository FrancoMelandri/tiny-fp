using NUnit.Framework;
using Shouldly;
using TinyFp;
using static TinyFp.Extensions.Functional;

namespace TinyFpTest.Extensions;

[TestFixture]
public class LoopExtensionsTests
{
    [Test]
    public void While_Should_Aggregate()
    {
        var counter = While(() => 0,
            _ => _ < 10,
            _ => ++_);

        counter.ShouldBe(10);
    }

    [Test]
    public void WhileAsync_Should_Aggregate()
    {
        var counter = While(() => 0,
                _ => _ < 10,
                _ => (++_).AsTask())
            .Result;

        counter.ShouldBe(10);
    }

    [Test]
    public void WhileAsync_Should_Aggregate_WhenAsyncInit()
    {
        var counter = While(() => 0.AsTask(),
                _ => _ < 10,
                _ => (++_).AsTask())
            .Result;

        counter.ShouldBe(10);
    }
}
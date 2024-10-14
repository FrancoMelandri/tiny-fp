using NUnit.Framework;
using FluentAssertions;
using Moq;
using static TinyFp.Extensions.Functional;
using TinyFp;

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

        counter.Should().Be(10);
    }

    [Test]
    public void WhileAsync_Should_Aggregate()
    {
        var counter = While(() => 0,
                _ => _ < 10,
                _ => (++_).AsTask())
            .Result;

        counter.Should().Be(10);
    }

    [Test]
    public void WhileAsync_Should_Aggregate_WhenAsyncInit()
    {
        var counter = While(() => 0.AsTask(),
                _ => _ < 10,
                _ => (++_).AsTask())
            .Result;

        counter.Should().Be(10);
    }
}
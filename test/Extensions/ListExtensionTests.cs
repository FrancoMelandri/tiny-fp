using NUnit.Framework;
using TinyFp;
using static TinyFp.Extensions.Functional;
using Shouldly;

namespace TinyFpTest.Extensions;

[TestFixture]
public class ListExtensionTests
{
    [Test]
    public void Filter_ReturnsOnlySomeElements()
        => new[] { Option<int>.Some(1), Option<int>.None(), Option<int>.Some(3) }
            .Filter()
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 1, 3 });

    [Test]
    public void Filter_ReturnsOnlySomeElementsMatchingPredicate()
        => new[] { Option<int>.Some(1), Option<int>.None(), Option<int>.Some(3) }
            .Filter(_ => _ > 1)
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 3 });

    [Test]
    public void Filter_ReturnsOnlyRightElements()
        => new[] { Either<string, int>.Right(1), Either<string, int>.Left("2"), Either<string, int>.Right(3), Either<string, int>.Left("4") }
            .Filter()
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 1, 3 });

    [Test]
    public void Filter_ReturnsOnlyRightElementsMatchingPredicate()
        => new[] { Either<string, int>.Right(1), Either<string, int>.Left("2"), Either<string, int>.Right(3), Either<string, int>.Left("4") }
            .Filter(_ => _ > 1)
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 3 });

    [Test]
    public void FilterLeft_ReturnsOnlyLeftElements()
        => new[] { Either<string, int>.Right(1), Either<string, int>.Left("2"), Either<string, int>.Right(3), Either<string, int>.Left("4") }
            .FilterLeft()
            .ToArray()
            .ShouldBeEquivalentTo(new[] { "2", "4" });

    [Test]
    public void FilterLeft_ReturnsOnlyLeftElementsMatchingPredicate()
        => new[] { Either<string, int>.Right(1), Either<string, int>.Left("2"), Either<string, int>.Right(3), Either<string, int>.Left("10") }
            .FilterLeft(_ => _.Length > 1)
            .ToArray()
            .ShouldBeEquivalentTo(new[] { "10" });

    [Test]
    public void Filter_FilterUsingPredicate()
        => new[] { 1, 2, 3, 4, 5 }
            .Filter(_ => _ >= 4)
            .Count().ShouldBe(2);

    [TestCase(0, 10)]
    [TestCase(10, 20)]
    public void Fold_ApplyFolder_WithInitialState(int initial, int expected)
        => new[] { 1, 2, 3, 4 }
            .Fold(initial, (s, i) => s + i)
            .ShouldBe(expected);

    [TestCase(0, 10)]
    [TestCase(10, 20)]
    public void FoldAsync_ApplyFolder_WithInitialState(int initial, int expected)
        => new[] { 1, 2, 3, 4 }
            .FoldAsync(initial, (s, i) => (s + i).AsTask())
            .Result
            .ShouldBe(expected);

    [TestCase(0, 10)]
    [TestCase(10, 20)]
    public void FoldAsync_ApplyFolder_WithInitialStateTask(int initial, int expected)
        => new[] { 1, 2, 3, 4 }
            .FoldAsync(initial.AsTask(), (s, i) => (s + i).AsTask())
            .Result
            .ShouldBe(expected);

    [Test]
    public void Reduce_ApplyReducer()
        => new[] { 1, 2, 3, 4 }
            .Reduce((s, i) => s + i)
            .ShouldBe(10);

    [Test]
    public void ReduceAsync_ApplyFolder()
        => new[] { 1, 2, 3, 4 }
            .ReduceAsync( (s, i) => (s + i).AsTask())
            .Result
            .ShouldBe(10);
    
    [Test]
    public void Map_ApplyMap()
        => new[] { 1, 2, 3, 4 }
            .Map(_ => _ * 2)
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 2, 4, 6, 8 });

    [Test]
    public void Map_ApplyMapOnlyToSomeElements()
        => new[] { Option<int>.Some(1), Option<int>.None(), Option<int>.Some(3) }
            .Map(_ => _ * 2)
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 2, 6 });

    [Test]
    public void Map_ApplyMapOnlyRightElements()
        => new[] { Either<string, int>.Right(1), Either<string, int>.Left("2"), Either<string, int>.Right(3), Either<string, int>.Left("4") }
            .Map(_ => _ * 2)
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 2, 6 });

    [Test]
    public void MapLeft_ApplyMapOnlyLeftElements()
        => new[] { Either<string, int>.Right(1), Either<string, int>.Left("2"), Either<string, int>.Right(3), Either<string, int>.Left("4") }
            .MapLeft(_ => int.Parse(_) * 2)
            .ToArray()
            .ShouldBeEquivalentTo(new[] { 4, 8 });

    [Test]
    public void ForEach_ApplyForEach()
    {
        var counter = 0;
        _ = new[] { 1, 2, 3, 4 }
            .ForEach(_ => counter += _);
        counter.ShouldBe(10);
    }
}
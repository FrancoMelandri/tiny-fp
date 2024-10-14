using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using TinyFp.Extensions;

namespace TinyFpTest.Extensions;

internal class TraverseExtensionsTest
{
    [Test]
    public void Traverse_Either_WithTransformation_ShouldReturnTransformedArray()
    {
        var values = new[]
        {
            Either<string, int>.Right(1),
            Either<string, int>.Right(2),
            Either<string, int>.Right(3)
        };

        var result = values.Traverse(x => x.ToString());

        result.IsRight.Should().BeTrue();
        result.Unwrap().Should().BeEquivalentTo(["1", "2", "3"]);
    }

    [Test]
    public void Traverse_Either_WithLeftValue_ShouldReturnLeft()
    {
        var values = new[]
        {
            Either<string, int>.Right(1),
            Either<string, int>.Left("Error"),
            Either<string, int>.Right(3)
        };

        var result = values.Traverse(x => x.ToString());

        result.IsLeft.Should().BeTrue();
        result.UnwrapLeft().Should().Be("Error");
    }

    [Test]
    public void Traverse_Either_WithoutTransformation_ShouldReturnSameArray()
    {
        var values = new[]
        {
            Either<string, int>.Right(1),
            Either<string, int>.Right(2),
            Either<string, int>.Right(3)
        };

        var result = values.Traverse();

        result.IsRight.Should().BeTrue();
        result.Unwrap().Should().BeEquivalentTo(new[] { 1, 2, 3 });
    }

    [Test]
    public void Traverse_Either_WithoutTransformation_WithLeftValue_ShouldReturnLeft()
    {
        var values = new[]
        {
            Either<string, int>.Right(1),
            Either<string, int>.Left("Error"),
            Either<string, int>.Right(3)
        };

        var result = values.Traverse();

        result.IsLeft.Should().BeTrue();
        result.UnwrapLeft().Should().Be("Error");
    }

    [Test]
    public async Task Traverse_AsyncFuncs_ShouldReturnArrayOfEithers()
    {
        Func<int, Task<Either<string, string>>>[] funcs = {
            async x => await Task.FromResult(Either<string, string>.Right($"Value {x}")),
            async x => await Task.FromResult(Either<string, string>.Right($"Value {x + 1}")),
            async x => await Task.FromResult(Either<string, string>.Right($"Value {x + 2}"))
        };

        var value = Task.FromResult(Either<string, int>.Right(1));

        var result = await funcs.Traverse(value);

        result.Should().BeEquivalentTo(new[] {
            Either<string, string>.Right("Value 1"),
            Either<string, string>.Right("Value 2"),
            Either<string, string>.Right("Value 3")
        });
    }

    [Test]
    public async Task Traverse_AsyncFuncs_WithLeftValue_ShouldReturnArrayWithLeft()
    {
        Func<int, Task<Either<string, string>>>[] funcs = {
            async x => await Task.FromResult(Either<string, string>.Right($"Value {x}")),
            async x => await Task.FromResult(Either<string, string>.Left("Error")),
            async x => await Task.FromResult(Either<string, string>.Right($"Value {x + 2}"))
        };

        var value = Task.FromResult(Either<string, int>.Right(1));

        var result = await funcs.Traverse(value);

        result[1].IsLeft.Should().BeTrue();
        result[1].UnwrapLeft().Should().Be("Error");
    }
}
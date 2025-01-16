using System.Diagnostics;
using NUnit.Framework;
using Shouldly;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class TaskTests
{
    [Test]
    public void AsTask_ReturnTask()
        => new object()
            .AsTask()
            .ShouldBeOfType(typeof(Task<object>));

    [Test]
    public void MatchAsync_WhenLeft_CallLeft()
        => Task.FromResult(Left<string, int>("left"))
            .MatchAsync(_ => { Assert.Fail(); return Task.CompletedTask; }, 
                _ => { _.ShouldBe("left"); return Task.CompletedTask; })
            .Wait();

    [Test]
    public void MatchAsync_WhenRight_CallRight()
        => Task.FromResult(Right<string, int>(42))
            .MatchAsync(_ => { _.ShouldBe(42); return Task.CompletedTask; }, 
                _ => { Assert.Fail(); return Task.CompletedTask; })
            .Wait();

    [Test]
    public void MatchAsync_1_WhenRight_ToOuput()
        => Task.FromResult(Either<int, string>.Right("either"))
            .MatchAsync(_ => true, _ => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_1_WhenLeft_ToOuput()
        => Task.FromResult(Either<int, string>.Left(42))
            .MatchAsync(_ => false, _ => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_2_WhenRight_ToOuput()
        => Task.FromResult(Either<int, string>.Right("either"))
            .MatchAsync(_ => Task.FromResult(true), _ => false)
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_2_WhenLeft_ToOuput()
        => Task.FromResult(Either<int, string>.Left(42))
            .MatchAsync(_ => Task.FromResult(false), _ => true)
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_3_WhenRight_ToOuput()
        => Task.FromResult(Either<int, string>.Right("either"))
            .MatchAsync(_ => true, _ => false)
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_3_WhenLeft_ToOuput()
        => Task.FromResult(Either<int, string>.Left(42))
            .MatchAsync(_ => false, _ => true)
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_4_WhenRight_ToOuput()
        => Task.FromResult(Either<int, string>.Right("either"))
            .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_4_WhenLeft_ToOuput()
        => Task.FromResult(Either<int, string>.Left(42))
            .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MapAsync_MapToOutput()
        => Task.FromResult("42")
            .MapAsync(_ => Task.FromResult(Convert.ToInt32(_)))
            .Result
            .ShouldBe(42);

    [Test]
    public void BindAsync_WhenRight_CallTheRightFunc()
    {
        var either = Task.FromResult((Either<int, string>)"right");

        var result = either.BindAsync(right => (Either<int, string>)right.ToUpper()).Result;

        result.IsRight.ShouldBeTrue();
        result.OnRight(_ => _.ShouldBe("RIGHT"));
    }

    [Test]
    public void BindAsync_WhenLeft_DontCallTheRightFunc()
    {
        var either = Task.FromResult((Either<int, string>)42);

        var result = either.BindAsync(right => (Either<int, string>)right.ToUpper()).Result;

        result.IsLeft.ShouldBeTrue();
    }

    [Test]
    public void BindAsync_Task_WhenRight_CallTheRightFunc()
    {
        var either = Task.FromResult((Either<int, string>)"right");

        var result = either.BindAsync(right => Task.FromResult((Either<int, string>)right.ToUpper())).Result;

        result.IsRight.ShouldBeTrue();
        result.OnRight(_ => _.ShouldBe("RIGHT"));
    }

    [Test]
    public void BindAsync_Task_WhenLeft_DontCallTheRightFunc()
    {
        var either = Task.FromResult((Either<int, string>)42);

        var result = either.BindAsync(right => Task.FromResult((Either<int, string>)right.ToUpper())).Result;

        result.IsLeft.ShouldBeTrue();
    }

    [Test]
    public void BindLeftAsync_WhenLeft_CallTheLeftFunc()
    {
        var either = Task.FromResult((Either<int, string>)10);

        var result = either.BindLeftAsync(intLeft => (Either<int, string>)(intLeft * 10)).Result;

        result.IsLeft.ShouldBeTrue();
        result.OnLeft(_ => _.ShouldBe(100));
    }

    [Test]
    public void MapAsync_WhenRight_MapToOutput()
        => Task.FromResult(Either<int, string>.Right("either"))
            .MapAsync(_ => _ == "either")
            .Result
            .IsRight
            .ShouldBeTrue();

    [Test]
    public void MapAsync_WhenLeft_DontMapToOutput()
        => Task.FromResult(Either<int, string>.Left(0))
            .MapAsync(_ => _ == "either")
            .Result
            .IsLeft
            .ShouldBeTrue();

    [Test]
    public void MapAsync_Task_WhenRight_MapToOutput()
        => Task.FromResult(Either<int, string>.Right("either"))
            .MapAsync(_ => Task.FromResult(_ == "either"))
            .Result
            .IsRight
            .ShouldBeTrue();

    [Test]
    public void MapAsync_Task_WhenLeft_DontMapToOutput()
        => Task.FromResult(Either<int, string>.Left(0))
            .MapAsync(_ => Task.FromResult(_ == "either"))
            .Result
            .IsLeft
            .ShouldBeTrue();

    [Test]
    public void MapLeftAsync_WhenRight_DontMapToOutput()
    {
        var called = false;
        Task.FromResult(Either<int, string>.Right("either"))
            .MapLeftAsync(_ => called = true).Wait();

        called.ShouldBeFalse();
    }

    [Test]
    public void MapLeftAsync_WhenLeft_MapToOutput()
    {
        var called = false;
        Task.FromResult(Either<int, string>.Left(0))
            .MapLeftAsync(_ => called = true).Wait();

        called.ShouldBeTrue();
    }

    [Test]
    public void MatchAsync_IfSome_ToOutput()
        => Task.FromResult(Option<string>
                .Some("not-empty"))
            .MatchAsync(_ => _ == "not-empty",
                () => false)
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_1_IfSome_ToOutput()
        => Task.FromResult(Option<string>
                .Some("not-empty"))
            .MatchAsync(_ => _ == "not-empty",
                () => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_1_IfNone_ToOutput()
        => Task.FromResult(Option<string>
                .None())
            .MatchAsync(_ => false,
                () => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_2_IfSome_ToOutput()
        => Task.FromResult(Option<string>
                .Some("not-empty"))
            .MatchAsync(_ => Task.FromResult(_ == "not-empty"),
                () => false)
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_2_IfNone_ToOutput()
        => Task.FromResult(Option<string>
                .None())
            .MatchAsync(_ => Task.FromResult(false),
                () => true)
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_3_IfSome_ToOutput()
        => Task.FromResult(Option<string>
                .Some("not-empty"))
            .MatchAsync(_ => Task.FromResult(_ == "not-empty"),
                () => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_3_IfNone_ToOutput()
        => Task.FromResult(Option<string>
                .None())
            .MatchAsync(_ => Task.FromResult(false),
                () => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_IfNone_ToOutput()
        => Task.FromResult(Option<string>
                .None())
            .MatchAsync(_ => false,
                () => true)
            .Result
            .ShouldBeTrue();

    [Test]
    public void BindAsync_MapInputInOutput()
        => Task.FromResult(Option<string>
                .Some("not-empty"))
            .BindAsync(_ => Option<bool>.Some(_ == "not-empty"))
            .Result
            .OrElse(false)
            .ShouldBeTrue();

    [Test]
    public void BindAsync_MapNoneInOutput()
        => Task.FromResult(Option<string>
                .None())
            .BindAsync(_ => Option<bool>.Some(true))
            .Result
            .IsNone
            .ShouldBeTrue();

    [Test]
    public void MapAsync_MapInputInOutput()
        => Task.FromResult(Option<string>
                .Some("not-empty"))
            .MapAsync(_ => _ == "not-empty")
            .Result
            .OrElse(false)
            .ShouldBeTrue();

    [Test]
    public void MapAsync_MapNoneInOutput()
        => Task.FromResult(Option<string>
                .None())
            .MapAsync(_ => true)
            .Result
            .IsNone
            .ShouldBeTrue();
    
    [Test]
    public void TeeAsync_TeeToOutput()
    {
        var result = 0;
        Task.FromResult("42")
            .TeeAsync(_ => result = Convert.ToInt32(_))
            .Result
            .ShouldBe("42");
        result.ShouldBe(42);
    }

    [Test]
    public void TeeAsync_AsyncTeeToOutput()
    {
        var result = Task.FromResult(0);
        Task.FromResult("42")
            .TeeAsync(_ => result = Convert.ToInt32(_).AsTask())
            .Result
            .ShouldBe("42");
        result.Result.ShouldBe(42);
    }

    [Test]
    public async Task TeeAsync_AsyncDelayAwaited()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        _ = await Unit.Default
            .AsTask()
            .TeeAsync(_ => Task.Delay(TimeSpan.FromSeconds(2)));

        stopWatch.Stop();
        stopWatch.Elapsed.Seconds.ShouldBeGreaterThan(1);
    }

    [Test]
    public void TeeAsync_TeeToOutputChangingResult()
        => Task.FromResult("0")
            .TeeAsync(_ => Task.FromResult("42"))
            .Result
            .ShouldBe("42");
}
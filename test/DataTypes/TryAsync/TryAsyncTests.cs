using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class TryAsyncTests
{
    static Task<int> GetValueAsync(int value)
        => Task.FromResult(100 / value);

    [Test]
    public void TryAsyncMatch_WhenNoException_Success()
        => TryAsync(() => GetValueAsync(10))
            .Match(_ => { _.Should().Be(10); return Unit.Default; },
                _ => { Assert.Fail(); return Unit.Default; })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
            .Match(_ => { Assert.Fail(); return Unit.Default; },
                _ => { Assert.Pass(); return Unit.Default; })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncSuccTask_WhenNoException_Success()
        => TryAsync(() => GetValueAsync(10))
            .Match(_ => { _.Should().Be(10); return Task.FromResult(Unit.Default); },
                _ => { Assert.Fail(); return Unit.Default; })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncSuccTask_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
            .Match(_ => { Assert.Fail(); return Task.FromResult(Unit.Default); },
                _ => { Assert.Pass(); return Unit.Default; })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncFailTask_WhenNoException_Success()
        => TryAsync(() => GetValueAsync(10))
            .Match(_ => { _.Should().Be(10); return Unit.Default; },
                _ => { Assert.Fail(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncFailTask_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
            .Match(_ => { Assert.Fail(); return Unit.Default; },
                _ => { Assert.Pass(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncTask_WhenNoException_Success()
        => TryAsync(() => GetValueAsync(10))
            .Match(_ => { _.Should().Be(10); return Task.FromResult(Unit.Default); },
                _ => { Assert.Fail(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncTask_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
            .Match(_ => { Assert.Fail(); return Task.FromResult(Unit.Default); },
                _ => { Assert.Pass(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncSucc_WhenNoException_Success()
        => TryAsync(() => GetValueAsync(10))
            .Match(_ => { _.Should().Be(10); return Unit.Default; },
                Unit.Default)
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncSucc_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
            .Match(_ => { Assert.Fail(); return Unit.Default; },
                Unit.Default)
            .GetAwaiter().GetResult()
            .Should().Be(Unit.Default);

    [Test]
    public void TryAsyncMatch_FuncTaskSucc_WhenNoException_Success()
        => TryAsync(() => GetValueAsync(10))
            .Match(_ => { _.Should().Be(10); return Task.FromResult(Unit.Default); },
                Unit.Default)
            .GetAwaiter().GetResult();

    [Test]
    public void TryAsyncMatch_FuncTaskSucc_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
            .Match(_ => { Assert.Fail(); return Task.FromResult(Unit.Default); },
                Unit.Default)
            .GetAwaiter().GetResult()
            .Should().Be(Unit.Default);

    [Test]
    public void TryAsyncOnFail_WhenNoException_Value()
        => TryAsync(() => GetValueAsync(10))                
            .OnFail(0)
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_WhenException_Fallback()
        => TryAsync(() => GetValueAsync(0))                
            .OnFail(10)
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_FuncTask_WhenNoException_Value()
        => TryAsync(() => GetValueAsync(10))
            .OnFail(() => Task.FromResult(0))
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_FuncTask_WhenException_Fallback()
        => TryAsync(() => GetValueAsync(0))
            .OnFail(() => Task.FromResult(10))
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_Func_WhenNoException_Value()
        => TryAsync(() => GetValueAsync(10))
            .OnFail(() => 0)
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_Func_WhenException_Fallback()
        => TryAsync(() => GetValueAsync(0))
            .OnFail(() => 10)
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_FuncEx_WhenNoException_Value()
        => TryAsync(() => GetValueAsync(10))
            .OnFail(ex => 0)
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_FuncEx_WhenException_Fallback()
        => TryAsync(() => GetValueAsync(0))
            .OnFail(ex => ex.Message == "Attempted to divide by zero." ? 10 : 0)
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_FuncExTask_WhenNoException_Value()
        => TryAsync(() => GetValueAsync(10))
            .OnFail(ex => Task.FromResult(0))
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncOnFail_FuncExTask_WhenException_Fallback()
        => TryAsync(() => GetValueAsync(0))
            .OnFail(ex => Task.FromResult(ex.Message == "Attempted to divide by zero." ? 10 : 0))
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncToEither_WhenNoException_Right()
        => TryAsync(() => GetValueAsync(10))
            .ToEither()
            .Result
            .IsRight
            .Should().BeTrue();

    [Test]
    public void TryAsyncToEither_WhenException_Left()
        => TryAsync(() => GetValueAsync(0))
            .ToEither()
            .Result
            .IsLeft
            .Should().BeTrue();

    [Test]
    public void TryAsyncBind_WhenNoException_Call()
        => TryAsync(() => GetValueAsync(10))
            .Bind(_ => TryAsync(() => GetValueAsync(5)))                
            .OnFail(0)
            .Result
            .Should().Be(20);

    [Test]
    public void TryAsyncBind_WhenFirstException_DontCall()
        => TryAsync(() => GetValueAsync(0))
            .Bind(_ => { Assert.Fail(); return TryAsync(() => GetValueAsync(5)); })
            .OnFail(0)
            .Result
            .Should().Be(0);

    [Test]
    public void TryAsyncDo_WhenNoException_Call()
        => TryAsync(() => GetValueAsync(10))
            .Do(_ => { })
            .OnFail(0)
            .Result
            .Should().Be(10);

    [Test]
    public void TryAsyncDo_WhenException_DontCall()
        => TryAsync(() => GetValueAsync(0))
            .Do(_ => { })
            .OnFail(0)
            .Result
            .Should().Be(0);

    [Test]
    public void TryAsyncMemo_WhenNoExcpetion_MemoizeTheTryCall()
    {
        var counter = 0;
        var @try = TryAsync(() => Task.FromResult(counter++));
        var memo = @try.Memo();
        memo().Wait();
        memo().Wait();
        memo().Wait();
        counter.Should().Be(1);
    }

    [Test]
    public void TryAsyncMemo_WhenExcpetion_DontMemoizeTheTryCall()
    {
        var counter = 0;
        var @try = TryAsync(() => Task.FromResult(counter++ == 0 ? throw new Exception() : counter));
        var memo = @try.Memo();
        memo().Wait();
        memo().Wait();
        memo().Wait();
        counter.Should().Be(2);
    }

}
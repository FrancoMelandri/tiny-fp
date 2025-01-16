using Moq;
using NUnit.Framework;
using Shouldly;
using TinyFp;
using static TinyFp.Extensions.Functional;

namespace TinyFpTest.Extensions;

[TestFixture]
public class UsingExtensionsTests
{
    public interface ILog
    {
        void Log(string message);
    }

    public class CanBeDisposed(ILog logger) : IDisposable
    {
        public void Dispose()
        {
            logger.Log("dispose");
        }

        public bool Mock { get; set; }

        public bool GetTrue() => true;

        public Task<bool> GetTrueAsync() => Task.FromResult(true);

        public Task SetTrueAsync(ref bool value)
        {
            Task.Delay(100);
            value = true;
            return Task.CompletedTask;
        }
    }

    public class CanBeDisposedAlt(CanBeDisposed innerDisposable, ILog logger) : IDisposable
    {
        public string GetTrue() => innerDisposable.GetTrue().ToString();

        public Task<string> GetTrueAsync() => Task.FromResult(innerDisposable.GetTrue().ToString());

        public Task SetTrueAsync(ref string value)
        {
            Task.Delay(1000);
            value = innerDisposable.GetTrue().ToString();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            logger.Log("dispose-alt");
        }
    }

    [Test]
    public void Using_WithAction_ShouldCallDispose()
    {
        var called = false;
        Action action = () => called = true;
        var log = new Mock<ILog>();
        var disposable = new CanBeDisposed(log.Object);

        Using(disposable, action);

        called.ShouldBeTrue();
        log.Verify(m => m.Log("dispose"), Times.Once);
    }

    [Test]
    public void Using_WithFunction_ShouldCallDispose()
    {            
        var action = () => true;
        var log = new Mock<ILog>();
        var disposable = new CanBeDisposed(log.Object);

        var called = Using(disposable, action);

        called.ShouldBeTrue();
        log.Verify(m => m.Log("dispose"), Times.Once);
    }

    [Test]
    public void Using_WithFunctionlnDisposable_ShouldCallWithObject()
    {
        Func<CanBeDisposed, bool> action = _ =>
        {
            _.GetType().ShouldBe(typeof(CanBeDisposed));
            return true;
        };
        var log = new Mock<ILog>();
        var disposable = new CanBeDisposed(log.Object);

        var called = Using(disposable, action);

        called.ShouldBeTrue();
        log.Verify(m => m.Log("dispose"), Times.Once);
    }

    [Test]
    public void Using_WithActiolnDisposable_ShouldCallWithObject()
    {
        var called = false;
        Action<IDisposable> action = _ =>
        {
            _.GetType().ShouldBe(typeof(CanBeDisposed));
            called = true;
        };
        var log = new Mock<ILog>();
        var disposable = new CanBeDisposed(log.Object);

        Using(disposable, action);

        called.ShouldBeTrue();
        log.Verify(m => m.Log("dispose"), Times.Once);
    }

    [Test]
    public void UsingAsync_WithActionDisposable_ShouldCallWithObject()
    {
        var log = new Mock<ILog>();
        var called = false;

        var result = UsingAsync(new CanBeDisposed(log.Object),
            cbd => cbd.SetTrueAsync(ref called)).Result;

        result.ShouldBe(Unit.Default);
        called.ShouldBeTrue();
        log.Verify(m => m.Log("dispose"), Times.Once);
    }

    [Test]
    public void UsingAsync_WithFunctionDisposable_ShouldCallWithObject()
    {
        var log = new Mock<ILog>();

        var result = UsingAsync(new CanBeDisposed(log.Object),
            cbd => cbd.GetTrueAsync()).Result;

        result.ShouldBeTrue();
        log.Verify(m => m.Log("dispose"), Times.Once);
    }

    [Test]
    public void UsingAsync_WithActionWithTwoDisposable_ShouldCallWithObjects()
    {
        var log = new Mock<ILog>();
        var called = false;
        var calledAlt = "false";

        var result = UsingAsync(new CanBeDisposed(log.Object),
            cbd => new CanBeDisposedAlt(cbd, log.Object),
            async (cbd, cbda) =>
            {
                await cbd.SetTrueAsync(ref called);
                await cbda.SetTrueAsync(ref calledAlt);
            }).Result;

        result.ShouldBe(Unit.Default);
        called.ShouldBeTrue();
        calledAlt.ShouldBe("True");
        log.Verify(m => m.Log("dispose"), Times.Once);
        log.Verify(m => m.Log("dispose-alt"), Times.Once);
    }

    [Test]
    public void UsingAsync_WithFunctionWithTwoDisposable_ShouldCallWithObject()
    {
        var log = new Mock<ILog>();

        var (result1, result2) = UsingAsync(new CanBeDisposed(log.Object),
            cbd => new CanBeDisposedAlt(cbd, log.Object),
            async (cbd, cbda) =>
            (
                await cbd.GetTrueAsync(),
                await cbda.GetTrueAsync()
            )).Result;

        result1.ShouldBeTrue();
        result2.ShouldBe("True");
        log.Verify(m => m.Log("dispose"), Times.Once);
        log.Verify(m => m.Log("dispose-alt"), Times.Once);
    }

}
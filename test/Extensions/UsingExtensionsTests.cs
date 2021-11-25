using NUnit.Framework;
using FluentAssertions;
using Moq;
using static TinyFp.Extensions.Functional;
using TinyFp;

namespace TinyFpTest.Extensions
{
    [TestFixture]
    public class UsingExtensionsTests
    {
        public interface ILog
        {
            void Log(string message);
        }

        public class CanBeDisposed : IDisposable
        {
            private readonly ILog _logger;

            public CanBeDisposed(ILog logger)
            {
                _logger = logger;
            }

            public void Dispose()
            {
                _logger.Log("dispose");
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

        public class CanBeDisposedAlt : IDisposable
        {
            private readonly CanBeDisposed _innerDisposable;
            private readonly ILog _logger;

            public CanBeDisposedAlt(CanBeDisposed innerDisposable, ILog logger)
            {
                _innerDisposable = innerDisposable;
                _logger = logger;
            }

            public string GetTrue() => _innerDisposable.GetTrue().ToString();

            public Task<string> GetTrueAsync() => Task.FromResult(_innerDisposable.GetTrue().ToString());

            public Task SetTrueAsync(ref string value)
            {
                Task.Delay(1000);
                value = _innerDisposable.GetTrue().ToString();
                return Task.CompletedTask;
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
                _logger.Log("dispose-alt");
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

            called.Should().BeTrue();
            log.Verify(m => m.Log("dispose"), Times.Once);
        }

        [Test]
        public void Using_WithFunction_ShouldCallDispose()
        {            
            Func<bool> action = () => true;
            var log = new Mock<ILog>();
            var disposable = new CanBeDisposed(log.Object);

            var called = Using(disposable, action);

            called.Should().BeTrue();
            log.Verify(m => m.Log("dispose"), Times.Once);
        }

        [Test]
        public void Using_WithFunctionlnDisposable_ShouldCallWithObject()
        {
            Func<CanBeDisposed, bool> action = _ =>
            {
                _.GetType().Should().Be(typeof(CanBeDisposed));
                return true;
            };
            var log = new Mock<ILog>();
            var disposable = new CanBeDisposed(log.Object);

            var called = Using(disposable, action);

            called.Should().BeTrue();
            log.Verify(m => m.Log("dispose"), Times.Once);
        }

        [Test]
        public void Using_WithActiolnDisposable_ShouldCallWithObject()
        {
            var called = false;
            Action<IDisposable> action = _ =>
            {
                _.GetType().Should().Be(typeof(CanBeDisposed));
                called = true;
            };
            var log = new Mock<ILog>();
            var disposable = new CanBeDisposed(log.Object);

            Using(disposable, action);

            called.Should().BeTrue();
            log.Verify(m => m.Log("dispose"), Times.Once);
        }

        [Test]
        public void UsingAsync_WithActionDisposable_ShouldCallWithObject()
        {
            var log = new Mock<ILog>();
            var called = false;

            var result = UsingAsync(new CanBeDisposed(log.Object),
                                    cbd => cbd.SetTrueAsync(ref called)).Result;

            result.Should().Be(Unit.Default);
            called.Should().BeTrue();
            log.Verify(m => m.Log("dispose"), Times.Once);
        }

        [Test]
        public void UsingAsync_WithFunctionDisposable_ShouldCallWithObject()
        {
            var log = new Mock<ILog>();

            var result = UsingAsync(new CanBeDisposed(log.Object),
                                    cbd => cbd.GetTrueAsync()).Result;

            result.Should().BeTrue();
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

            result.Should().Be(Unit.Default);
            called.Should().BeTrue();
            calledAlt.Should().Be("True");
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

            result1.Should().BeTrue();
            result2.Should().Be("True");
            log.Verify(m => m.Log("dispose"), Times.Once);
            log.Verify(m => m.Log("dispose-alt"), Times.Once);
        }

    }
}

using NUnit.Framework;
using FluentAssertions;
using System;
using Moq;
using static TinyFp.Extensions.FunctionalExtension;

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
    }
}

using NUnit.Framework;
using FluentAssertions;
using System;
using Moq;
using static TinyFp.Extensions.FunctionalExtension;

namespace TinyFpTest.Extensions
{
    [TestFixture]
    public class ToEitherExtensions
    {
        [Test]
        public void ToEither_WithMapAndWhen_WhenNoValue_AndWhenFalse_Left()
        {
            var sut = ((string)null).ToEither(_ => 10, _ => false, 0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_WithMapAndWhen_WhenNoValue_AndWhenTrue_Left()
        {
            var sut = ((string)null).ToEither(_ => 10, _ => true, 0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_WithMapAndWhen_WhenValue_AndWhenTrue_Left()
        {
            var sut = "not-empty".ToEither(_ => 10, _ => true, 0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_WithMapAndWhen_WhenValue_AndWhenFalse_Right()
        {
            var sut = "not-empty".ToEither(_ => 10, _ => false, 0);

            sut.IsRight.Should().BeTrue();
            sut.OnRight(_ => _.Should().Be(10));
        }

        [Test]
        public void ToEither_Func_WithMapAndWhen_WhenNoValue_AndWhenFalse_Left()
        {
            var sut = ((string)null).ToEither(_ => 10, _ => false, () => 0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_Func_WithMapAndWhen_WhenNoValue_AndWhenTrue_Left()
        {
            var sut = ((string)null).ToEither(_ => 10, _ => true, () => 0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_Func_WithMapAndWhen_WhenValue_AndWhenTrue_Left()
        {
            var sut = "not-empty".ToEither(_ => 10, _ => true, () => 0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_Func_WithMapAndWhen_WhenValue_AndWhenFalse_Right()
        {
            var sut = "not-empty".ToEither(_ => 10, _ => false, () => 0);

            sut.IsRight.Should().BeTrue();
            sut.OnRight(_ => _.Should().Be(10));
        }

        [Test]
        public void ToEither_WhenNoValue_Left()
        {
            var sut = ((string)null).ToEither(0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_WhenValue_Right()
            => "not-empty".ToEither(0)
                .IsRight.Should().BeTrue();

        [Test]
        public void ToEither_Func_WhenNoValue_Left()
        {
            var sut = ((string)null).ToEither(() => 0);

            sut.IsLeft.Should().BeTrue();
            sut.OnLeft(_ => _.Should().Be(0));
        }

        [Test]
        public void ToEither_Func_WhenValue_Right()
            => "not-empty".ToEither(() => 0)
                .IsRight.Should().BeTrue();

    }

    [TestFixture]
    public class UsingExtensions
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

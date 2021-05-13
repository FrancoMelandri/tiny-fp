using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using System.Threading.Tasks;
using static TinyFp.Prelude;
using System;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void AsTask_ReturnTask()
            => new object()
                .AsTask()
                .Should().BeOfType(typeof(Task<object>));

        [Test]
        public void MatchAsync_WhenLeft_CallLeft()
            => Task.FromResult(Left<string, int>("left"))
                .MatchAsync(_ => { Assert.Fail(); return Task.CompletedTask; }, 
                            _ => { _.Should().Be("left"); return Task.CompletedTask; })
                .Wait();

        [Test]
        public void MatchAsync_WhenRight_CallRight()
            => Task.FromResult(Right<string, int>(42))
                .MatchAsync(_ => { _.Should().Be(42); return Task.CompletedTask; }, 
                            _ => { Assert.Fail(); return Task.CompletedTask; })
                .Wait();

        [Test]
        public void MatchAsync_1_WhenRight_ToOuput()
            => Task.FromResult(Either<int, string>.Right("either"))
                .MatchAsync(_ => true, _ => Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_1_WhenLeft_ToOuput()
            => Task.FromResult(Either<int, string>.Left(42))
                .MatchAsync(_ => false, _ => Task.FromResult(true))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_2_WhenRight_ToOuput()
            => Task.FromResult(Either<int, string>.Right("either"))
                .MatchAsync(_ => Task.FromResult(true), _ => false)
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_2_WhenLeft_ToOuput()
            => Task.FromResult(Either<int, string>.Left(42))
                .MatchAsync(_ => Task.FromResult(false), _ => true)
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_3_WhenRight_ToOuput()
            => Task.FromResult(Either<int, string>.Right("either"))
                .MatchAsync(_ => true, _ => false)
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_3_WhenLeft_ToOuput()
            => Task.FromResult(Either<int, string>.Left(42))
                .MatchAsync(_ => false, _ => true)
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_4_WhenRight_ToOuput()
            => Task.FromResult(Either<int, string>.Right("either"))
                .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_4_WhenLeft_ToOuput()
            => Task.FromResult(Either<int, string>.Left(42))
                .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
                .Result
                .Should().BeTrue();

        [Test]
        public void MapAsync_MapToOutput()
            => Task.FromResult("42")
                .MapAsync(_ => Task.FromResult(Convert.ToInt32(_)))
                .Result
                .Should().Be(42);

        [Test]
        public void BindAsync_WhenRight_CallTheRightFunc()
        {
            var either = Task.FromResult((Either<int, string>)"right");

            var result = either.BindAsync(right => (Either<int, string>)right.ToUpper()).Result;

            result.IsRight.Should().BeTrue();
            result.OnRight(_ => _.Should().Be("RIGHT"));
        }

        [Test]
        public void BindAsync_WhenLeft_DontCallTheRightFunc()
        {
            var either = Task.FromResult((Either<int, string>)42);

            var result = either.BindAsync(right => (Either<int, string>)right.ToUpper()).Result;

            result.IsLeft.Should().BeTrue();
        }

        [Test]
        public void BindAsync_Task_WhenRight_CallTheRightFunc()
        {
            var either = Task.FromResult((Either<int, string>)"right");

            var result = either.BindAsync(right => Task.FromResult((Either<int, string>)right.ToUpper())).Result;

            result.IsRight.Should().BeTrue();
            result.OnRight(_ => _.Should().Be("RIGHT"));
        }

        [Test]
        public void BindAsync_Task_WhenLeft_DontCallTheRightFunc()
        {
            var either = Task.FromResult((Either<int, string>)42);

            var result = either.BindAsync(right => Task.FromResult((Either<int, string>)right.ToUpper())).Result;

            result.IsLeft.Should().BeTrue();
        }

        [Test]
        public void BindLeftAsync_WhenLeft_CallTheLeftFunc()
        {
            var either = Task.FromResult((Either<int, string>)10);

            var result = either.BindLeftAsync(intLeft => (Either<int, string>)(intLeft * 10)).Result;

            result.IsLeft.Should().BeTrue();
            result.OnLeft(_ => _.Should().Be(100));
        }

        [Test]
        public void MapAsync_WhenRight_MapToOutput()
            => Task.FromResult(Either<int, string>.Right("either"))
                .MapAsync(_ => _ == "either")
                .Result
                .IsRight
                .Should().BeTrue();

        [Test]
        public void MapAsync_WhenLeft_DontMapToOutput()
            => Task.FromResult(Either<int, string>.Left(0))
                .MapAsync(_ => _ == "either")
                .Result
                .IsLeft
                .Should().BeTrue();

        [Test]
        public void MapAsync_Task_WhenRight_MapToOutput()
            => Task.FromResult(Either<int, string>.Right("either"))
                .MapAsync(_ => Task.FromResult(_ == "either"))
                .Result
                .IsRight
                .Should().BeTrue();

        [Test]
        public void MapAsync_Task_WhenLeft_DontMapToOutput()
            => Task.FromResult(Either<int, string>.Left(0))
                .MapAsync(_ => Task.FromResult(_ == "either"))
                .Result
                .IsLeft
                .Should().BeTrue();

        [Test]
        public void MapLeftAsync_WhenRight_DontMapToOutput()
        {
            var called = false;
            Task.FromResult(Either<int, string>.Right("either"))
                .MapLeftAsync(_ => called = true).Wait();

            called.Should().BeFalse();
        }

        [Test]
        public void MapLeftAsync_WhenLeft_MapToOutput()
        {
            var called = false;
            Task.FromResult(Either<int, string>.Left(0))
                .MapLeftAsync(_ => called = true).Wait();

            called.Should().BeTrue();
        }

        [Test]
        public void MatchAsync_IfSome_ToOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .MatchAsync(_ => _ == "not-empty",
                           () => false)
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_1_IfSome_ToOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .MatchAsync(_ => _ == "not-empty",
                           () => Task.FromResult(false))
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_1_IfNone_ToOutput()
            => Task.FromResult(Option<string>
                    .None())
                    .MatchAsync(_ => false,
                           () => Task.FromResult(true))
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_2_IfSome_ToOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .MatchAsync(_ => Task.FromResult(_ == "not-empty"),
                           () => false)
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_2_IfNone_ToOutput()
            => Task.FromResult(Option<string>
                    .None())
                    .MatchAsync(_ => Task.FromResult(false),
                           () => true)
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_3_IfSome_ToOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .MatchAsync(_ => Task.FromResult(_ == "not-empty"),
                           () => Task.FromResult(false))
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_3_IfNone_ToOutput()
            => Task.FromResult(Option<string>
                    .None())
                    .MatchAsync(_ => Task.FromResult(false),
                           () => Task.FromResult(true))
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_IfNone_ToOutput()
            => Task.FromResult(Option<string>
                    .None())
                    .MatchAsync(_ => false,
                           () => true)
                    .Result
                .Should().BeTrue();

        [Test]
        public void BindAsync_MapInputInOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .BindAsync(_ => Option<bool>.Some(_ == "not-empty"))
                    .Result
                    .OnNone(false)
                .Should().BeTrue();

        [Test]
        public void BindAsync_MapNoneInOutput()
            => Task.FromResult(Option<string>
                    .None())
                    .BindAsync(_ => Option<bool>.Some(true))
                    .Result
                    .IsNone
                .Should().BeTrue();

        [Test]
        public void MapAsync_MapInputInOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .MapAsync(_ => _ == "not-empty")
                    .Result
                    .OnNone(false)
                .Should().BeTrue();

        [Test]
        public void MapAsync_MapNoneInOutput()
            => Task.FromResult(Option<string>
                    .None())
                    .MapAsync(_ => true)
                    .Result
                    .IsNone
                .Should().BeTrue();
    }
}

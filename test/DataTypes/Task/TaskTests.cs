﻿using FluentAssertions;
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
        public void MapAsync_MapToOutput()
            => Task.FromResult("42")
                .MapAsync(_ => Task.FromResult(Convert.ToInt32(_)))
                .Result
                .Should().Be(42);

        [Test]
        public void BindLeftAsync_WhenRight_CallTheRightFunc()
        {
            var either = Task.FromResult((Either<int, string>)"right");

            var result = either.BindAsync(right => (Either<int, string>)right.ToUpper()).Result;

            result.IsRight.Should().BeTrue();
            result.OnRight(_ => _.Should().Be("RIGHT"));
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
        public void MatchAsync_IfSome_ToOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .MatchAsync(_ => _ == "not-empty",
                           () => false)
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
        public void BindaSYNC_MapInputInOutput()
            => Task.FromResult(Option<string>
                    .Some("not-empty"))
                    .BindAsync(_ => Option<bool>.Some(_ == "not-empty"))
                    .Result
                    .OnNone(false)
                .Should().BeTrue();

        [Test]
        public void BindaSYNC_MapNoneInOutput()
            => Task.FromResult(Option<string>
                    .None())
                    .BindAsync(_ => Option<bool>.Some(true))
                    .Result
                    .IsNone
                .Should().BeTrue();
    }
}

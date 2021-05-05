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
        public void MapAsync_MapToOutput()
            => Task.FromResult("42")
                .MapAsync(_ => Task.FromResult(Convert.ToInt32(_)))
                .Result
                .Should().Be(42);
    }
}

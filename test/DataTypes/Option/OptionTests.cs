using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using System.Threading.Tasks;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void None_CreateNoneObject()
            => Option<string>
                    .None()
                    .IsNone
                .Should().BeTrue();

        [Test]
        public void Some_CreateSomeObject()
            => Option<string>
                    .Some("test")
                    .IsSome
                .Should().BeTrue();

        [Test]
        public void OnNone_CallTheAction()
        {
            var called = false;
            Option<string>
                .None()
                .OnNone(() => called = true);

           called.Should().BeTrue();
        }

        [Test]
        public void OnNone_CallTheFunction()
            => Option<string>
                    .None()
                    .OnNone(() => "empty")
                .Should().Be("empty");

        [Test]
        public void OnNone_ReturnSomeObject()
            => Option<string>
                    .None()
                    .OnNone("empty")
                .Should().Be("empty");

        [Test]
        public void OnNone_WhenSome_ReturnSome()
            => Option<string>
                    .Some("not-empty")
                    .OnNone("empty")
                .Should().Be("not-empty");

        [Test]
        public void OnSome_CallTheAction()
        {
            var called = false;
            Option<string>
                .Some("not-empty")
                .OnSome(_ => called = _ == "not-empty");

           called.Should().BeTrue();
        }

        [Test]
        public void Map_MapInputInOutput()
            => Option<string>
                    .Some("not-empty")
                    .Map(_ => _ == "not-empty")
                    .OnNone(false)
                .Should().BeTrue();

        [Test]
        public void Map_MapNoneInOutput()
            => Option<string>
                    .None()
                    .Map(_ => true)
                    .IsNone
                .Should().BeTrue();

        [Test]
        public void MapAsync_MapInputInOutput()
            => Option<string>
                    .Some("not-empty")
                    .MapAsync(_ => Task.FromResult(_ == "not-empty"))
                    .Result
                    .OnNone(false)
                .Should().BeTrue();

        [Test]
        public void MapAsync_MapNoneInOutput()
            => Option<string>
                    .None()
                    .MapAsync(_ => Task.FromResult(true))
                    .Result
                    .IsNone
                .Should().BeTrue();

        [Test]
        public void Bind_MapInputInOutput()
            => Option<string>
                    .Some("not-empty")
                    .Bind(_ => Option<bool>.Some(_ == "not-empty"))
                    .OnNone(false)
                .Should().BeTrue();

        [Test]
        public void Bind_MapNoneInOutput()
            => Option<string>
                    .None()
                    .Bind(_ => Option<bool>.Some(true))
                    .IsNone
                .Should().BeTrue();

        [Test]
        public void BindAsync_MapInputInOutput()
            => Option<string>
                    .Some("not-empty")
                    .BindAsync(_ => Task.FromResult(Option<bool>.Some(_ == "not-empty")))
                    .Result
                    .OnNone(false)
                .Should().BeTrue();

        [Test]
        public void BindAsync_MapNoneInOutput()
            => Option<string>
                    .None()
                    .BindAsync(_ => Task.FromResult(Option<bool>.Some(true)))
                    .Result
                    .IsNone
                .Should().BeTrue();

        [Test]
        public void Match_IfSome_ToOutput()
            => Option<string>
                    .Some("not-empty")
                    .Match(_ => _ == "not-empty",
                           () => false )                    
                .Should().BeTrue();

        [Test]
        public void Match_IfNone_ToOutput()
            => Option<string>
                    .None()
                    .Match(_ => false,
                           () => true)
                .Should().BeTrue();

        [Test]
        public void MatchAsync_IfSome_ToOutput()
            => Option<string>
                    .Some("not-empty")
                    .MatchAsync(_ => Task.FromResult(_ == "not-empty"),
                           () => Task.FromResult(false))
                    .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_IfNone_ToOutput()
            => Option<string>
                    .None()
                    .MatchAsync(_ => Task.FromResult(false),
                           () => Task.FromResult(true))
                    .Result
                .Should().BeTrue();
    }
}

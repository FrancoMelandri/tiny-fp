using NUnit.Framework;
using System.Threading.Tasks;
using TinyFp;
using FluentAssertions;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class EitherTests
    {
        [Test]
        public void Right_CreaateRight()
            => Either<int, string>.Right("either")
                .IsRight
                .Should().BeTrue();

        [Test]
        public void Left_CreaateLeft()
            => Either<int, string>.Left(0)
                .IsLeft
                .Should().BeTrue();

        [Test]
        public void OnRight_WhenRight_CallAction()
        {
            var called = false;
            Either<int, string>.Right("either")
                .OnRight(_ => called = true);

            called.Should().BeTrue();
        }

        [Test]
        public void OnRight_WhenLeft_DontCallAction()
        {
            var called = false;
            Either<int, string>.Left(0)
                .OnRight(_ => called = true);

            called.Should().BeFalse();
        }

        [Test]
        public void OnLeft_WhenRight_DontCallAction()
        {
            var called = false;
            Either<int, string>.Right("either")
                .OnLeft(_ => called = true);

            called.Should().BeFalse();
        }

        [Test]
        public void OnLeft_WhenLeft_CallAction()
        {
            var called = false;
            Either<int, string>.Left(0)
                .OnLeft(_ => called = true);

            called.Should().BeTrue();
        }

        [Test]
        public void Map_WhenRight_MapToOutput()
            => Either<int, string>.Right("either")
                .Map(_ => _ == "either")
                .IsRight
                .Should().BeTrue();

        [Test]
        public void Map_WhenLeft_DontMapToOutput()
            => Either<int, string>.Left(0)
                .Map(_ => _ == "either")
                .IsLeft
                .Should().BeTrue();

        [Test]
        public void MapAsync_WhenRight_MapToOutput()
            => Either<int, string>.Right("either")
                .MapAsync(_ => Task.FromResult(_ == "either"))
                .Result
                .IsRight
                .Should().BeTrue();

        [Test]
        public void MapAsync_WhenLeft_DontMapToOutput()
            => Either<int, string>.Left(0)
                .MapAsync(_ => Task.FromResult(_ == "either"))
                .Result
                .IsLeft
                .Should().BeTrue();

        [Test]
        public void MapLeft_WhenRight_DontMapToOutput()
        {
            var called = false;
            Either<int, string>.Right("either")
                .MapLeft(_ => called = true);

            called.Should().BeFalse();
        }

        [Test]
        public void MapLeft_WhenLeft_MapToOutput()
        {
            var called = false;
            Either<int, string>.Left(0)
                .MapLeft(_ => called = true);

            called.Should().BeTrue();
        }

        [Test]
        public void MapLeftAsync_WhenRight_DontMapToOutput()
        {
            var called = false;
            Either<int, string>.Right("either")
                .MapLeftAsync(_ => { called = true;  return Task.FromResult(0); });

            called.Should().BeFalse();
        }

        [Test]
        public void MapLeftAsync_WhenLeft_MapToOutput()
        {
            var called = false;
            Either<int, string>.Left(0)
                .MapLeftAsync(_ => { called = true; return Task.FromResult(0); });

            called.Should().BeTrue();
        }

        [Test]
        public void Bind_WhenRight_ChainCallRight()
            => Either<int, string>.Right("either")
                .Bind(_ => Either<int, bool>.Right(true))
                .OnRight(_ => _.Should().BeTrue())
                .OnLeft(_ => Assert.Fail());

        [Test]
        public void Bind_WhenRight_ChainCallLeft()
            => Either<int, string>.Right("either")
                .Bind(_ => Either<int, bool>.Left(42))
                .OnRight(_ => Assert.Fail())
                .OnLeft(_ => _.Should().Be(42));

        [Test]
        public void Bind_WhenLeft_DontChainCall()
        {
            var called = false;
            Either<int, string>.Left(0)
                .Bind(_ => { called = true; return Either<int, bool>.Right(true); })
                .OnRight(_ => Assert.Fail());
            
            called.Should().BeFalse();
        }

        [Test]
        public void BindAsync_WhenRight_ChainCallRight()
            => Either<int, string>.Right("either")
                .BindAsync(_ => Task.FromResult(Either<int, bool>.Right(true)))
                .Result
                .OnRight(_ => _.Should().BeTrue())
                .OnLeft(_ => Assert.Fail());

        [Test]
        public void BindAsync_WhenRight_ChainCallLeft()
            => Either<int, string>.Right("either")
                .BindAsync(_ => Task.FromResult(Either<int, bool>.Left(42)))
                .Result
                .OnRight(_ => Assert.Fail())
                .OnLeft(_ => _.Should().Be(42));

        [Test]
        public void BindAsync_WhenLeft_DontChainCall()
        {
            var called = false;
            Either<int, string>.Left(0)
                .BindAsync(_ => { called = true; return Task.FromResult(Either<int, bool>.Right(true)); })
                .Result
                .OnRight(_ => Assert.Fail());

            called.Should().BeFalse();
        }

        [Test]
        public void Match_WhenRight_ToOuput()
            => Either<int, string>.Right("either")
                .Match(_ => true, _ => false)
                .Should().BeTrue();

        [Test]
        public void Match_WhenLeft_ToOuput()
            => Either<int, string>.Left(42)
                .Match(_ => false, _ => true)
                .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenRight_ToOuput()
            => Either<int, string>.Right("either")
                .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenLeft_ToOuput()
            => Either<int, string>.Left(42)
                .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
                .Result
                .Should().BeTrue();
    }
}

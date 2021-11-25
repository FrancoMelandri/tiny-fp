using NUnit.Framework;
using TinyFp.Common;
using FluentAssertions;
using TinyFp.Exceptions;

namespace TinyFpTest.DataTypes.Common
{
    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void Ctor_CreateSuccess()
            => new Result<string>("result")
                .IsSuccess
                .Should().BeTrue();

        [Test]
        public void Ctor_CreateFaulted()
            => new Result<string>(new Exception("result"))
                .IsFaulted
                .Should().BeTrue();

        [Test]
        public void Ctor_CreateFaulted_Bottom_NullException()
            => new Result<string>((Exception)null)
                .IsBottom
                .Should().BeTrue();

        [Test]
        public void Ctor_CreateFaulted_Bottom_BottomException()
            => new Result<string>(new BottomException())
                .IsBottom
                .Should().BeTrue();

        [Test]
        public void Match_WhenSuccees_ToOuptut()
            => new Result<string>("result")
                .Match(_ => true, _ => false)
                .Should().BeTrue();

        [Test]
        public void Match_WhenFail_ToOuptut()
            => new Result<string>(new Exception("result"))
                .Match(
                _ => false, 
                _ => { _.Message.Should().Be("result"); return true; })
                .Should().BeTrue();

        [Test]
        public void Match_WhenFail_Bottom_ToOuptut()
            => new Result<string>((Exception)null)
                .Match(
                _ => false, 
                _ => { _.Should().BeOfType(typeof(BottomException)); return true; })
                .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenSuccees_ToOuptut()
            => new Result<string>("result")
                .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenFail_ToOuptut()
            => new Result<string>(new Exception("result"))
                .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
                .Result
                .Should().BeTrue();

        [Test]
        public void Map_WhenSuccees_ToOuptut()
            => new Result<string>("result")
                .Map(_ => _ == "result")
                .IsSuccess
                .Should().BeTrue();

        [Test]
        public void Map_WhenFail_ToOuptut()
            => new Result<string>(new Exception("result"))
                .Map(_ => _ == "result")
                .IsFaulted
                .Should().BeTrue();

        [Test]
        public void MapAsync_WhenSuccees_ToOuptut()
            => new Result<string>("result")
                .MapAsync(_ => Task.FromResult(_ == "result"))
                .Result
                .IsSuccess
                .Should().BeTrue();

        [Test]
        public void MapAsync_WhenFail_ToOuptut()
            => new Result<string>(new Exception("result"))
                .MapAsync(_ => Task.FromResult(_ == "result"))
                .Result
                .IsFaulted
                .Should().BeTrue();

        [Test]
        public void OnFail_WhenSuccees_NoAction()
            => new Result<string>("result")
                .OnFail("fail")
                .Should().Be("result");

        [Test]
        public void OnFail_WhenFaulted_DefaultValue()
            => new Result<string>(new Exception("result"))
                .OnFail("fail")
                .Should().Be("fail");

        [Test]
        public void OnFailFunc_WhenSuccees_NoAction()
            => new Result<string>("result")
                .OnFail(_ => "fail")
                .Should().Be("result");

        [Test]
        public void OnFailFunc_WhenFaulted_DefaultValue()
            => new Result<string>(new Exception("result"))
                .OnFail(_ => "fail")
                .Should().Be("fail");

    }
}

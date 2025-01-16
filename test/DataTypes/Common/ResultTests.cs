using NUnit.Framework;
using Shouldly;
using TinyFp.Common;
using TinyFp.Exceptions;

namespace TinyFpTest.DataTypes.Common;

[TestFixture]
public class ResultTests
{
    [Test]
    public void Ctor_CreateSuccess()
        => new Result<string>("result")
            .IsSuccess
            .ShouldBeTrue();

    [Test]
    public void Ctor_CreateFaulted()
        => new Result<string>(new Exception("result"))
            .IsFaulted
            .ShouldBeTrue();

    [Test]
    public void Ctor_CreateFaulted_Bottom_NullException()
        => new Result<string>((Exception)null)
            .IsBottom
            .ShouldBeTrue();

    [Test]
    public void Ctor_CreateFaulted_Bottom_BottomException()
        => new Result<string>(new BottomException())
            .IsBottom
            .ShouldBeTrue();

    [Test]
    public void Match_WhenSuccees_ToOuptut()
        => new Result<string>("result")
            .Match(_ => true, _ => false)
            .ShouldBeTrue();

    [Test]
    public void Match_WhenFail_ToOuptut()
        => new Result<string>(new Exception("result"))
            .Match(
                _ => false, 
                _ => { _.Message.ShouldBe("result"); return true; })
            .ShouldBeTrue();

    [Test]
    public void Match_WhenFail_Bottom_ToOuptut()
        => new Result<string>((Exception)null)
            .Match(
                _ => false, 
                _ => { _.ShouldBeOfType(typeof(BottomException)); return true; })
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_WhenSuccees_ToOuptut()
        => new Result<string>("result")
            .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_WhenFail_ToOuptut()
        => new Result<string>(new Exception("result"))
            .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void Map_WhenSuccees_ToOuptut()
        => new Result<string>("result")
            .Map(_ => _ == "result")
            .IsSuccess
            .ShouldBeTrue();

    [Test]
    public void Map_WhenFail_ToOuptut()
        => new Result<string>(new Exception("result"))
            .Map(_ => _ == "result")
            .IsFaulted
            .ShouldBeTrue();

    [Test]
    public void MapAsync_WhenSuccees_ToOuptut()
        => new Result<string>("result")
            .MapAsync(_ => Task.FromResult(_ == "result"))
            .Result
            .IsSuccess
            .ShouldBeTrue();

    [Test]
    public void MapAsync_WhenFail_ToOuptut()
        => new Result<string>(new Exception("result"))
            .MapAsync(_ => Task.FromResult(_ == "result"))
            .Result
            .IsFaulted
            .ShouldBeTrue();

    [Test]
    public void OnFail_WhenSuccees_NoAction()
        => new Result<string>("result")
            .OnFail("fail")
            .ShouldBe("result");

    [Test]
    public void OnFail_WhenFaulted_DefaultValue()
        => new Result<string>(new Exception("result"))
            .OnFail("fail")
            .ShouldBe("fail");

    [Test]
    public void OnFailFunc_WhenSuccees_NoAction()
        => new Result<string>("result")
            .OnFail(_ => "fail")
            .ShouldBe("result");

    [Test]
    public void OnFailFunc_WhenFaulted_DefaultValue()
        => new Result<string>(new Exception("result"))
            .OnFail(_ => "fail")
            .ShouldBe("fail");

}
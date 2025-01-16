using NUnit.Framework;
using Shouldly;
using TinyFp;
using TinyFp.Exceptions;

namespace TinyFpTest.DataTypes;

[TestFixture]
public class ValidationTests
{
    [Test]
    public void Success_CreateSuccessValidation()
        => Validation<string, Unit>.Success(Unit.Default)
            .IsSuccess
            .ShouldBeTrue();

    [Test]
    public void Fail_CreateFailValidation()
        => Validation<string, Unit>.Fail("failed")
            .IsFail
            .ShouldBeTrue();

    [Test]
    public void Success_ImplicitCast()
    {
        Validation<string, object> val = (object)42;

        val.IsSuccess.ShouldBeTrue();
    }

    [Test]
    public void Success_ImplicitCast_WhenNull_RaiseException()
    {
        var act = () => { Validation<string, object> val = (object)null; };

        act.ShouldThrow<ValueIsNullException>();
    }

    [Test]
    public void Fail_ImplicitCast()
    {
        Validation<string, object> val = "not-empty";

        val.IsFail.ShouldBeTrue();
    }

    [Test]
    public void Fail_ImplicitCast_WhenNull_RaiseException()
    {
        var act = () => { Validation<string, object> val = (string)null; };

        act.ShouldThrow<ValueIsNullException>();
    }

    [Test]
    public void Match_FuncFunc_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .Match(_ => true, _ => false)
            .ShouldBeTrue();

    [Test]
    public void Match_FuncFunc_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .Match(_ => false, _ => true)
            .ShouldBeTrue();

    [Test]
    public void Match_ValFunc_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .Match(true, _ => false)
            .ShouldBeTrue();

    [Test]
    public void Match_ValFunc_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .Match(false, _ => true)
            .ShouldBeTrue();

    [Test]
    public void Match_FuncVal_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .Match(_ => true, false)
            .ShouldBeTrue();

    [Test]
    public void Match_FuncVal_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .Match(_ => false, true)
            .ShouldBeTrue();

    [Test]
    public void Match_ValVal_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .Match(true, false)
            .ShouldBeTrue();

    [Test]
    public void Match_ValVal_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .Match(false, true)
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_FuncFunc_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_FuncFunc_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_FuncVal_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .MatchAsync(_ => Task.FromResult(true), Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_FuncVal_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .MatchAsync(_ => Task.FromResult(false), Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_ValFunc_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .MatchAsync(Task.FromResult(true), _ => Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_ValFunc_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .MatchAsync(Task.FromResult(false), _ => Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_ValVal_WhenSuccess_ToOuput()
        => Validation<string, int>.Success(42)
            .MatchAsync(Task.FromResult(true), Task.FromResult(false))
            .Result
            .ShouldBeTrue();

    [Test]
    public void MatchAsync_ValVal_WhenFail_ToOuput()
        => Validation<string, int>.Fail("fail")
            .MatchAsync(Task.FromResult(false), Task.FromResult(true))
            .Result
            .ShouldBeTrue();

    [Test]
    public void Bind_WhenSuccess_ChainCall_NewSuccess()
        => Validation<string, int>.Success(1)
            .Bind(_ => Validation<string, int>.Success(42))
            .Match(
                _ => { _.ShouldBe(42); return 0; }, 
                _ => { Assert.Fail(); return 0; });

    [Test]
    public void Bind_WhenSuccess_ChainCall_NewFail()
        => Validation<string, int>.Success(1)
            .Bind(_ => Validation<string, int>.Fail("error"))
            .Match(
                _ => { Assert.Fail(); return 0; }, 
                _ => { _.ShouldBe("error"); return 0; });

    [Test]
    public void Bind_WhenFail_DontChainCall()
        => Validation<string, int>.Fail("error")
            .Bind(_ => { Assert.Fail(); return Validation<string, int>.Success(42); })
            .Match(
                _ => { Assert.Fail(); return 0; }, 
                _ => { _.ShouldBe("error"); return 0; });

    [Test]
    public void BindFail_WhenSuccess_DontChainCall()
        => Validation<string, int>.Success(1)
            .BindFail(_ => { Assert.Fail(); return Validation<string, int>.Success(42); })
            .Match(
                _ => { _.ShouldBe(1); return 0; }, 
                _ => { Assert.Fail(); return 0; });

    [Test]
    public void BindFail_WhenFail_ChainCall_NewFail()   
        => Validation<string, int>.Fail("")
            .BindFail(_ => Validation<string, int>.Fail("error"))
            .Match(
                _ => { Assert.Fail(); return 0; }, 
                _ => { _.ShouldBe("error"); return 0; });

    [Test]
    public void BindFail_WhenFail_ChainCall_NewSuccessl()
        => Validation<string, int>.Fail("error")
            .BindFail(_ => Validation<string, int>.Success(42))
            .Match(
                _ => { _.ShouldBe(42); return 0; },
                _ => { Assert.Fail(); return 0; });

    [Test]
    public void Map_WhenSuccess_ToOutput()
        => Validation<string, int>.Success(1)
            .Map(_ => 42)
            .Match(
                _ => { _.ShouldBe(42); return 0; },
                _ => { Assert.Fail(); return 0; });

    [Test]
    public void Map_WhenFail_Dontmap()
        => Validation<string, int>.Fail("error")
            .Map(_ => { Assert.Fail(); return 42; })
            .Match(
                _ => { Assert.Fail(); return 0; },
                _ => { _.ShouldBe("error"); return 0; });

    [Test]
    public void MapFail_WhenSuccess_DontMap()
        => Validation<string, int>.Success(1)
            .MapFail(_ => { Assert.Fail(); return 42; })
            .Match(
                _ => { _.ShouldBe(1); return 0; },
                _ => { Assert.Fail(); return 0; });

    [Test]
    public void MapFail_WhenFail_ToOutput()
        => Validation<string, int>.Fail("error")
            .MapFail(_ => 42)
            .Match(
                _ => { Assert.Fail(); return 0; },
                _ => { _.ShouldBe(42); return 0; });

    [Test]
    public void OperatorTrue_WhenSuccess_IsTrue()
    {
        if (Validation<string, int>.Success(42))
            Assert.Pass();
        else
            Assert.Fail();
    }

    [Test]
    public void OperatorTrue_WhenFail_IsFalse()
    {
        if (Validation<string, int>.Fail("fail"))
            Assert.Fail();
        else
            Assert.Pass();
    }

    [Test]
    public void OperatorNot_WhenSuccess_IsFalse()
    {
        if (!Validation<string, int>.Success(42))
            Assert.Fail();
        else
            Assert.Pass();
    }

    [Test]
    public void OperatorNot_WhenSuccess_IsTrue()
    {
        if (!Validation<string, int>.Fail("left"))
            Assert.Pass();
        else
            Assert.Fail();
    }
}
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TinyFp;
using TinyFp.DataTypes;
using TinyFp.Exceptions;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void Success_CreateSuccessValidation()
            => Validation<string, Unit>.Success(Unit.Default)
                .IsSuccess
                .Should().BeTrue();

        [Test]
        public void Fail_CreateFailValidation()
            => Validation<string, Unit>.Fail("failed")
                .IsFail
                .Should().BeTrue();

        [Test]
        public void Success_ImplicitCast()
        {
            Validation<string, object> val = (object)42;

            val.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void Success_ImplicitCast_WhenNull_RaiseException()
        {
            Action act = () => { Validation<string, object> val = (object)null; };

            act.Should().Throw<ValueIsNullException>();
        }

        [Test]
        public void Fail_ImplicitCast()
        {
            Validation<string, object> val = "not-empty";

            val.IsFail.Should().BeTrue();
        }

        [Test]
        public void Fail_ImplicitCast_WhenNull_RaiseException()
        {
            Action act = () => { Validation<string, object> val = (string)null; };

            act.Should().Throw<ValueIsNullException>();
        }

        [Test]
        public void Match_FuncFunc_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .Match(_ => true, _ => false)
                .Should().BeTrue();

        [Test]
        public void Match_FuncFunc_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .Match(_ => false, _ => true)
                .Should().BeTrue();

        [Test]
        public void Match_ValFunc_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .Match(true, _ => false)
                .Should().BeTrue();

        [Test]
        public void Match_ValFunc_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .Match(false, _ => true)
                .Should().BeTrue();

        [Test]
        public void Match_FuncVal_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .Match(_ => true, false)
                .Should().BeTrue();

        [Test]
        public void Match_FuncVal_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .Match(_ => false, true)
                .Should().BeTrue();

        [Test]
        public void Match_ValVal_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .Match(true, false)
                .Should().BeTrue();

        [Test]
        public void Match_ValVal_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .Match(false, true)
                .Should().BeTrue();

        [Test]
        public void MatchAsync_FuncFunc_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_FuncFunc_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_FuncVal_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .MatchAsync(_ => Task.FromResult(true), Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_FuncVal_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .MatchAsync(_ => Task.FromResult(false), Task.FromResult(true))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_ValFunc_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .MatchAsync(Task.FromResult(true), _ => Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_ValFunc_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .MatchAsync(Task.FromResult(false), _ => Task.FromResult(true))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_ValVal_WhenSuccess_ToOuput()
            => Validation<string, int>.Success(42)
                .MatchAsync(Task.FromResult(true), Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_ValVal_WhenFail_ToOuput()
            => Validation<string, int>.Fail("fail")
                .MatchAsync(Task.FromResult(false), Task.FromResult(true))
                .Result
                .Should().BeTrue();
    }
}
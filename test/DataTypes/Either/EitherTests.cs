using NUnit.Framework;
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
        public void Right_WhenNull_RaiseException()
        {
            Action act = () => Either<object, object>.Right(null);

            act.Should()
                .Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("right");
        }

        [Test]
        public void Left_CreaateLeft()
            => Either<int, string>.Left(0)
                .IsLeft
                .Should().BeTrue();

        [Test]
        public void Left_WhenNull_RaiseException()
        {
            Action act = () => Either<object, object>.Left(null);

            act.Should()
                .Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("left");
        }

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
            _ = Either<int, string>.Right("either")
                .MapLeftAsync(_ => { called = true;  return Task.FromResult(0); })
                .Result;

            called.Should().BeFalse();
        }

        [Test]
        public void MapLeftAsync_WhenLeft_MapToOutput()
        {
            var called = false;
            _ = Either<int, string>.Left(0)
                .MapLeftAsync(_ => { called = true; return Task.FromResult(0); })
                .Result;

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
        public void BindLeft_WhenLeft_ChainCallRight()
            => Either<int, string>.Left(10)
                .BindLeft(_ => Either<int, string>.Right("not-empty"))
                .OnRight(_ => _.Should().Be("not-empty"))
                .OnLeft(_ => Assert.Fail());

        [Test]
        public void BindLeft_WhenLeft_ChainCallLeft()
            => Either<int, string>.Left(0)
                .BindLeft(_ => Either<int, string>.Left(42))
                .OnRight(_ => Assert.Fail())
                .OnLeft(_ => _.Should().Be(42));

        [Test]
        public void BindLeft_WhenRight_DontChainCall()
        {
            var called = false;
            Either<int, string>.Right("")
                .BindLeft(_ => { called = true; return Either<int, string>.Right("not-empty"); })
                .OnLeft(_ => Assert.Fail())
                .OnRight(_ => _.Should().Be(""));

            called.Should().BeFalse();
        }

        [Test]
        public void BindLeftAsync_WhenLeft_ChainCallRight()
            => Either<int, string>.Left(10)
                .BindLeftAsync(_ => Task.FromResult(Either<int, string>.Right("not-empty")))
                .Result
                .OnRight(_ => _.Should().Be("not-empty"))
                .OnLeft(_ => Assert.Fail());

        [Test]
        public void BindLeftAsync_WhenLeft_ChainCallLeft()
            => Either<int, string>.Left(0)
                .BindLeftAsync(_ => Task.FromResult(Either<int, string>.Left(42)))
                .Result
                .OnRight(_ => Assert.Fail())
                .OnLeft(_ => _.Should().Be(42));

        [Test]
        public void BindLeftAsync_WhenRight_DontChainCall()
        {
            var called = false;
            Either<int, string>.Right("")
                .BindLeftAsync(_ => { called = true; return Task.FromResult(Either<int, string>.Right("not-empty")); })
                .Result
                .OnLeft(_ => Assert.Fail())
                .OnRight(_ => _.Should().Be(""));

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

        [Test]
        public void MatchAsync_1_WhenRight_ToOuput()
            => Either<int, string>.Right("either")
                .MatchAsync(_ => true, _ => Task.FromResult(false))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_1_WhenLeft_ToOuput()
            => Either<int, string>.Left(42)
                .MatchAsync(_ => false, _ => Task.FromResult(true))
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_2_WhenRight_ToOuput()
            => Either<int, string>.Right("either")
                .MatchAsync(_ => Task.FromResult(true), _ => false)
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_2_WhenLeft_ToOuput()
            => Either<int, string>.Left(42)
                .MatchAsync(_ => Task.FromResult(false), _ => true)
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_3_WhenRight_ToOuput()
            => Either<int, string>.Right("either")
                .MatchAsync(_ => true, _ => false)
                .Result
                .Should().BeTrue();

        [Test]
        public void MatchAsync_3_WhenLeft_ToOuput()
            => Either<int, string>.Left(42)
                .MatchAsync(_ => false, _ => true)
                .Result
                .Should().BeTrue();

        [Test]
        public void WhenRight_ImplicitCast()
        {
            Either<int, string> result = "right";

            result.IsRight.Should().BeTrue();
            result.IsLeft.Should().BeFalse();
        }

        [Test]
        public void WhenLeft_ImplicitCast()
        {
            Either<int, string> result = 1;

            result.IsLeft.Should().BeTrue();
            result.IsRight.Should().BeFalse();
        }

        [Test]
        public void OperatorTrue_WhenRight_IsTrue()
        {
            if (Either<string, int>.Right(42))
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void OperatorTrue_WhenLeft_IsFalse()
        {
            if (Either<string, int>.Left("left"))
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void OperatorNot_WhenRight_IsFalse()
        {
            if (!Either<string, int>.Right(42))
                Assert.Fail();
            else
                Assert.Pass();
        }

        [Test]
        public void OperatorNot_WhenLeft_IsTrue()
        {
            if (!Either<string, int>.Left("left"))
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void Guard_Map_Default()
        {
            Either<int, Option<int>>.Right(Option<int>.Some(42))
                .GuardMap(
                        age => "NONE",
                        (age => age.IsSome, age => $"{age.Match(_ => _, () => 0)}")
                    )
                .OnRight(l => l.Should().Be("42"));

            Either<int, Option<int>>.Right(Option<int>.None)
                .GuardMap(
                        age => "NONE",
                        (age => age.IsSome, age => $"{age.Match(_ => _, () => 0)}")
                    )
                .OnRight(l => l.Should().Be("NONE"));
        }

        [Test]
        public void Guard_Map_Guards()
        {
            Either<int, int>.Right(42)
                .GuardMap(DefaultDelegate(), Guards())
                .OnLeft(_ => Assert.Fail())
                .OnRight(_ => _.Should().Be(AgeStage.JobTime(42)));

            Either<int, int>.Right(102)
                .GuardMap(DefaultDelegate(), Guards())
                .OnLeft(_ => Assert.Fail())
                .OnRight(_ => _.Should().NotBe(AgeStage.JobTime(105)))
                .OnRight(_ => _.Should().NotBe(AgeStage.JobTime(102)));
        }

        [Test]
        public void Guard_Bind_DrinkLicenseExample()
        {

            Either<string, Option<int>>.Right(Option<int>.Some(18))
                .GuardBind(
                    age => Either<string, string>.Right("Do you need some milk?"),
                    (age => age.IsNone, age => Either<string, string>.Left("Error, age not specified")),
                    (age => age.Match(_ => _, () => 0) >= 21, age => Either<string, string>.Right("OK"))
                )
                .OnRight(verification => verification.Should().Be("Do you need some milk?"));

            Either<string, Option<int>>.Right(Option<int>.Some(22))
                .GuardBind(
                    age => Either<string, string>.Right("Do you need some milk?"),
                    (age => age.IsNone, age => Either<string, string>.Left("Error, age not specified")),
                    (age => age.Match(_ => _, () => 0) >= 21, age => Either<string, string>.Right("OK"))
                )
                .OnRight(verification => verification.Should().Be("OK"));

            Either<string, Option<int>>.Right(Option<int>.None)
                .GuardBind(
                    age => Either<string, string>.Right("Do you need some milk?"),
                    (age => age.IsNone, age => Either<string, string>.Left("Error, age not specified")),
                    (age => age.Match(_ => _, () => 0) >= 21, age => Either<string, string>.Right("OK"))
                )
                .OnRight(verification => Assert.Fail("Didn't catch option.none"))
                .OnLeft(verification => verification.Should().Be("Error, age not specified"));

        }

        [Test]
        public void Guard_MapAsync_Default()
        {
            Either<int, Option<int>>.Right(Option<int>.Some(42))
                .GuardMapAsync(
                        age => Task.FromResult("NONE"),
                        (age => age.IsSome, age => Task.FromResult($"{age.Match(_ => _, () => 0)}"))
                    )
                .Result
                .OnRight(l => l.Should().Be("42"));

            Either<int, Option<int>>.Right(Option<int>.None)
                .GuardMapAsync(
                        age => Task.FromResult("NONE"),
                        (age => age.IsSome, age => Task.FromResult($"{age.Match(_ => _, () => 0)}"))
                    )
                .Result
                .OnRight(l => l.Should().Be("NONE"));
        }

        [Test]
        public void Guard_BindAsync_DrinkLicenseExample()
        {
            Either<string, Option<int>>.Right(Option<int>.Some(18))
                .GuardBindAsync(
                    age => Task.FromResult(Either<string, string>.Right("Do you need some milk?")),
                    (age => age.IsNone, age => Task.FromResult(Either<string, string>.Left("Error, age not specified"))),
                    (age => age.Match(_ => _, () => 0) >= 21, age => Task.FromResult(Either<string, string>.Right("OK")))
                )
                .Result
                .OnRight(verification => verification.Should().Be("Do you need some milk?"));

            Either<string, Option<int>>.Right(Option<int>.Some(22))
                .GuardBindAsync(
                    age => Task.FromResult(Either<string, string>.Right("Do you need some milk?")),
                    (age => age.IsNone, age => Task.FromResult(Either<string, string>.Left("Error, age not specified"))),
                    (age => age.Match(_ => _, () => 0) >= 21, age => Task.FromResult(Either<string, string>.Right("OK")))
                )
                .Result
                .OnRight(verification => verification.Should().Be("OK"));

            Either<string, Option<int>>.Right(Option<int>.None)
                .GuardBindAsync(
                    age => Task.FromResult(Either<string, string>.Right("Do you need some milk?")),
                    (age => age.IsNone, age => Task.FromResult(Either<string, string>.Left("Error, age not specified"))),
                    (age => age.Match(_ => _, () => 0) >= 21, age => Task.FromResult(Either<string, string>.Right("OK")))
                )
                .Result
                .OnRight(verification => Assert.Fail("Didn't catch option.none"))
                .OnLeft(verification => verification.Should().Be("Error, age not specified"));
        }
        public Func<int, string> DefaultDelegate() { return AgeStage.MilkTime; }
        public (Func<int, bool> evalExpressions, Func<int, string> delegateIfTrue)[] Guards() =>
            new (Func<int, bool> evalExpressions, Func<int, string> delegateIfTrue)[] {
                (_ => _ >= 5 && _ <= 10, AgeStage.PrimarySchool),
                (_ => _ >= 11 && _ <= 13, AgeStage.SecondarySchool),
                (_ => _ >= 14 && _ <= 19, AgeStage.HighSchool),
                (_ => _ == 20, AgeStage.Checkpoint),
                (_ => _ > 20 && _< 67, AgeStage.JobTime),
                (_ => _> 67 && _< 100, AgeStage.Retirement),
                (_ => _> 100, AgeStage.TimeToDie)
            };
        public static class AgeStage
        {
            public static string MilkTime(int age) => "Do you still need some milk?";
            public static string PrimarySchool(int age) => "Go to primary school";
            public static string SecondarySchool(int age) => $"You're {age}, time to secondary school";
            public static string HighSchool(int age) => $"You're {age}, go and graduate!";
            public static string Checkpoint(int age) => $"You're {age}...and still alive! Congrats";
            public static string JobTime(int age) => $"Find a job now, you're {age} already";
            public static string Retirement(int age) => $"You're {age}, we are proud of you! Enjoy your retirement";
            public static string TimeToDie(int age) => $"{age} years old...still alive!?";
        }
    }
}

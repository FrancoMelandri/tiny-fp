using FluentAssertions;
using NUnit.Framework;
using System;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class TryTests
    {
        static int GetValue(int value)
            => 100 / value;

        [Test]
        public void TryMatch_Func_WhenNoException_Success()
            => Try(() => GetValue(10))
                .Match(_ => 10 ,
                       _ => 0)
                 .Should().Be(10);

        [Test]
        public void TryMatch_Func_WhenException_Fail()
            => Try(() => GetValue(0))
                .Match(_ => 0,
                       _ => 10)
                .Should().Be(10);

        [Test]
        public void TryMatch_WhenNoException_Success()
            => Try(() => GetValue(10))
                .Match(_ => _,
                       0)
                .Should().Be(10);

        [Test]
        public void TryMatch_WhenException_Fail()
            => Try(() => GetValue(0))
                .Match(_ => _,
                       100)
                .Should().Be(100);

        [Test]
        public void TryOnFail_WhenNoException_Value()
            => Try(() => GetValue(10))
                .OnFail(0)
                .Should().Be(10);

        [Test]
        public void TryOnFail_WhenException_Fallback()
            => Try(() => GetValue(0))
                .OnFail(100)
                .Should().Be(100);

        [Test]
        public void TryOnFail_Func_WhenNoException_Value()
            => Try(() => GetValue(10))
                .OnFail(() => 0)
                .Should().Be(10);

        [Test]
        public void TryOnFail_Func_WhenException_Fallback()
            => Try(() => GetValue(0))
                .OnFail(() => 100)
                .Should().Be(100);

        [Test]
        public void TryOnFail_FuncWithEx_WhenNoException_Value()
            => Try(() => GetValue(10))
                .OnFail(_ => 0)
                .Should().Be(10);

        [Test]
        public void TryOnFail_FuncWithEx_WhenException_Fallback()
            => Try(() => GetValue(0))
                .OnFail(_ => _.Message.Length)
                .Should().Be(28);

        [Test]
        public void TryToEither_WhenNoException_Right()
            => Try(() => GetValue(10))
                .ToEither()
                .IsRight
                .Should().BeTrue();

        [Test]
        public void TryToEither_WhenException_Left()
            => Try(() => GetValue(0))
                .ToEither()
                .IsLeft
                .Should().BeTrue();

        [Test]
        public void TryMap_WhenNoException_Call()
            => Try(() => GetValue(10))
                .Map(_ => GetValue(_ * 10))
                .OnFail(0)
                .Should().Be(1);

        [Test]
        public void TryMap_WhenException_DontcallMap()
            => Try(() => GetValue(0))
                .Map(_ => GetValue(_ * 10))
                .OnFail(0)
                .Should().Be(0);

        [Test]
        public void TryBind_WhenNoException_Call()
            => Try(() => GetValue(10))
                .Bind(_ => Try(() => GetValue(5)))
                .OnFail(0)
                .Should().Be(20);
        [Test]
        public void TryBind_WhenFirstException_DontCall()
            => Try(() => GetValue(0))
                .Bind(_ => { Assert.Fail(); return Try(() => GetValue(5)); })
                .OnFail(0)
                .Should().Be(0);

        [Test]
        public void TryBind_WhenSecondException_DontCall()
            => Try(() => GetValue(10))
                .Bind(_ => Try(() => GetValue(0)))
                .OnFail(-1)
                .Should().Be(-1);

        [Test]
        public void TryDo_WhenNoException_Call()
            => Try(() => GetValue(10))
                .Do(_ => { } )
                .OnFail(0)
                .Should().Be(10);

        [Test]
        public void TryDo_WhenException_DontCall()
            => Try(() => GetValue(0))
                .Do(_ => { } )
                .OnFail(0)
                .Should().Be(0);

        [Test]
        public void Memo_WhenNoExcpetion_MemoizeTheTryCall()
        {
            var counter = 0;
            var @try = Try(() => counter++);
            var memo = @try.Memo();
            memo();
            memo();
            memo();
            counter.Should().Be(1);
        }

        [Test]
        public void Memo_WhenExcpetion_DontMemoizeTheTryCall()
        {
            var counter = 0;
            var @try = Try(() => counter++ == 0 ? throw new Exception() : counter );
            var memo = @try.Memo();
            memo();
            memo();
            memo();
            counter.Should().Be(2);
        }
    }
}
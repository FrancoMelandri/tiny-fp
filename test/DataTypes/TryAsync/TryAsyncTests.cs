using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest
{
    [TestFixture]
    public class TryAsyncTests
    {
        static Task<int> GetValueAsync(int value)
            => Task.FromResult(100 / value);

        [Test]
        public void TryAsyncMatch_WhenNoException_Success()
            => TryAsync(() => GetValueAsync(10))
                .Match(_ => { _.Should().Be(10); return Unit.Default; },
                       _ => { Assert.Fail(); return Unit.Default; })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
                .Match(_ => { Assert.Fail(); return Unit.Default; },
                       _ => { Assert.Pass(); return Unit.Default; })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncSuccTask_WhenNoException_Success()
            => TryAsync(() => GetValueAsync(10))
                .Match(_ => { _.Should().Be(10); return Task.FromResult(Unit.Default); },
                       _ => { Assert.Fail(); return Unit.Default; })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncSuccTask_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
                .Match(_ => { Assert.Fail(); return Task.FromResult(Unit.Default); },
                       _ => { Assert.Pass(); return Unit.Default; })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncFailTask_WhenNoException_Success()
            => TryAsync(() => GetValueAsync(10))
                .Match(_ => { _.Should().Be(10); return Unit.Default; },
                       _ => { Assert.Fail(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncFailTask_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
                .Match(_ => { Assert.Fail(); return Unit.Default; },
                       _ => { Assert.Pass(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncTask_WhenNoException_Success()
            => TryAsync(() => GetValueAsync(10))
                .Match(_ => { _.Should().Be(10); return Task.FromResult(Unit.Default); },
                       _ => { Assert.Fail(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncTask_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
                .Match(_ => { Assert.Fail(); return Task.FromResult(Unit.Default); },
                       _ => { Assert.Pass(); return Task.FromResult(Unit.Default); })
            .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncSucc_WhenNoException_Success()
            => TryAsync(() => GetValueAsync(10))
                .Match(_ => { _.Should().Be(10); return Unit.Default; },
                       Unit.Default)
                .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncSucc_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
                .Match(_ => { Assert.Fail(); return Unit.Default; },
                       Unit.Default)
            .GetAwaiter().GetResult()
            .Should().Be(Unit.Default);

        [Test]
        public void TryAsyncMatch_FuncTaskSucc_WhenNoException_Success()
            => TryAsync(() => GetValueAsync(10))
                .Match(_ => { _.Should().Be(10); return Task.FromResult(Unit.Default); },
                       Unit.Default)
                .GetAwaiter().GetResult();

        [Test]
        public void TryAsyncMatch_FuncTaskSucc_WhenException_Fail()
        => TryAsync(() => GetValueAsync(0))
                .Match(_ => { Assert.Fail(); return Task.FromResult(Unit.Default); },
                       Unit.Default)
            .GetAwaiter().GetResult()
            .Should().Be(Unit.Default);
    }
}

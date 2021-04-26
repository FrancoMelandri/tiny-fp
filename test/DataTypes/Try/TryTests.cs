using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using static TinyFp.Prelude;

namespace TinyFpTest
{
    [TestFixture]
    public class TryTests
    {
        int GetValue(int value)
            => 100 / value;

        [Test]
        public void Try_WhenNoException_Success()
            => Try(() => GetValue(10))
                .Match(_ => { _.Should().Be(10); return Unit.Default; },
                       _ => { Assert.Fail(); return Unit.Default; });

        [Test]
        public void Try_WhenException_Fail()
            => Try(() => GetValue(0))
                .Match(_ => { Assert.Fail(); return Unit.Default; },
                       _ => { Assert.Pass(); return Unit.Default; });        
    }
}

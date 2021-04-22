using System.Threading.Tasks;
using TinyFp;
using FluentAssertions;
using NUnit.Framework;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class UnionTests
    {
        [Test]
        public void FromT1_CreateT1Object()
        {
            var t1 = Union<int, string>.FromT1(42);

            t1.IsT1.Should().BeTrue();
            t1.IsT2.Should().BeFalse();
        }

        [Test]
        public void FromT2_CreateT1Object()
        {
            var t2 = Union<int, string>.FromT2("t2");

            t2.IsT1.Should().BeFalse();
            t2.IsT2.Should().BeTrue();
        }

        [Test]
        public void Match_WhenT1_ToOutput()
            => Union<int, string>.FromT1(42)
                    .Match(_ => true, _ => false)
                    .Should().BeTrue();

        [Test]
        public void Match_WhenT2_ToOutput()
            => Union<int, string>.FromT2("union")
                    .Match(_ => false, _ => true)
                    .Should().BeTrue();                

        [Test]
        public void MatchAsync_WhenT1_ToOutput()
            => Union<int, string>.FromT1(42)
                    .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false))
                    .Result
                    .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenT2_ToOutput()
            => Union<int, string>.FromT2("union")
                    .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true))
                    .Result
                    .Should().BeTrue();                
    }
}

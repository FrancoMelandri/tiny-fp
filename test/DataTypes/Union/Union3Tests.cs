using System.Threading.Tasks;
using TinyFp;
using FluentAssertions;
using NUnit.Framework;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class Union3Tests
    {
        [Test]
        public void FromT1_CreateT1Object()
        {
            var t1 = Union<int, string, bool>.FromT1(42);

            t1.IsT1.Should().BeTrue();
            t1.IsT2.Should().BeFalse();
            t1.IsT3.Should().BeFalse();
        }

        [Test]
        public void FromT2_CreateT1Object()
        {
            var t2 = Union<int, string, bool>.FromT2("t2");

            t2.IsT1.Should().BeFalse();
            t2.IsT2.Should().BeTrue();
            t2.IsT3.Should().BeFalse();
        }

        [Test]
        public void FromT3_CreateT3Object()
        {
            var t2 = Union<int, string, bool>.FromT3(true);

            t2.IsT1.Should().BeFalse();
            t2.IsT2.Should().BeFalse();
            t2.IsT3.Should().BeTrue();
        }

        [Test]
        public void Match_WhenT1_ToOutput()
            => Union<int, string, bool>.FromT1(42)
                    .Match(_ => true, _ => false, _ => false)
                    .Should().BeTrue();

        [Test]
        public void Match_WhenT2_ToOutput()
            => Union<int, string, bool>.FromT2("union")
                    .Match(_ => false, _ => true, _ => false)
                    .Should().BeTrue();

        [Test]
        public void Match_WhenT3_ToOutput()
            => Union<int, string, bool>.FromT3(true)
                    .Match(_ => false, _ => false, _ => true)
                    .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenT1_ToOutput()
            => Union<int, string, bool>.FromT1(42)
                    .MatchAsync(_ => Task.FromResult(true), _ => Task.FromResult(false), _ => Task.FromResult(false))
                    .Result
                    .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenT2_ToOutput()
            => Union<int, string, bool>.FromT2("union")
                    .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(true), _ => Task.FromResult(false))
                    .Result
                    .Should().BeTrue();

        [Test]
        public void MatchAsync_WhenT3_ToOutput()
            => Union<int, string, bool>.FromT3(true)
                    .MatchAsync(_ => Task.FromResult(false), _ => Task.FromResult(false), _ => Task.FromResult(true))
                    .Result
                    .Should().BeTrue();

        [Test]
        public void WhenT1_ImplicitCast()
        {
            Union<int, string, bool> result = 1;

            result.IsT1.Should().BeTrue();
            result.IsT2.Should().BeFalse();
            result.IsT3.Should().BeFalse();
        }

        [Test]
        public void WhenT2_ImplicitCast()
        {
            Union<int, string, bool> result = "union";

            result.IsT1.Should().BeFalse();
            result.IsT2.Should().BeTrue();
            result.IsT3.Should().BeFalse();
        }

        [Test]
        public void WhenT3_ImplicitCast()
        {
            Union<int, string, bool> result = true;

            result.IsT1.Should().BeFalse();
            result.IsT2.Should().BeFalse();
            result.IsT3.Should().BeTrue();
        }
    }
}

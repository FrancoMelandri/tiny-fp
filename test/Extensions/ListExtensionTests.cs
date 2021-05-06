using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using static TinyFp.Extensions.Functional;

namespace TinyFpTest.Extensions
{
    [TestFixture]
    public class ListExtensionTests
    {
        [Test]
        public void Filter_FilterUsingPredicate()
            => new [] { 1, 2, 3, 4, 5 }
                .Filter(_ => _ >= 4)
                .Should().HaveCount(2);

        [TestCase(0, 10)]
        [TestCase(10, 20)]
        public void Fold_ApplyFolder_WithInitialState(int initial, int expected)
            => new[] { 1, 2, 3, 4 }
                .Fold(initial, (s, i) => s + i)
                .Should().Be(expected);

        [Test]
        public void Reduce_ApplyReducer()
            => new[] { 1, 2, 3, 4 }
                .Reduce((s, i) => s + i)
                .Should().Be(10);

        [Test]
        public void Map_ApplyMap()
            => new [] { 1, 2, 3, 4 }
                .Map(_ => _ * 2)
                .Should().BeEquivalentTo(new[] { 2, 4, 6, 8 });

        [Test]
        public void ForEach_ApplyForEach()
        {
            var counter = 0;
            _ = new[] { 1, 2, 3, 4 }
                .ForEach(_ => counter += _);
            counter.Should().Be(10);
        }
    }       
}

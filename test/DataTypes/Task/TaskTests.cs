using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using System.Threading.Tasks;

namespace TinyFpTest.DataTypes
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void AsTask_ReturnTask()
            => new object()
                .AsTask()
                .Should().BeOfType(typeof(Task<object>));        
    }
}

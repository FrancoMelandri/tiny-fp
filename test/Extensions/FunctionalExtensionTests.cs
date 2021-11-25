using TinyFp.Extensions;
using NUnit.Framework;
using FluentAssertions;

namespace TinyFpTest.Extensions
{
    [TestFixture]
    public class FunctionalExtensionTests
    {
        [TestCase(true, "teezed")]
        [TestCase(false, "any")]
        public void TeeWhen_WithTrueOrFalseCondition_TransformOrNot(bool whenResult, string expected)
            => "any".TeeWhen(_ => "teezed", () => whenResult)
                .Should().Be(expected);

        [TestCase(true, "teezed")]
        [TestCase(false, "any")]
        public void TeeWhen_WithTrueOrFalseConditionOnInput_TransformOrNot(bool whenResult, string expected)
            => "any".TeeWhen(_ => "teezed", _ => whenResult)
                .Should().Be(expected);

        [Test]
        public void Tee_Transform()
        {
            var result = "any".Tee(_ => "teezed");

            result.Should().Be("teezed");
        }

        [Test]
        public void Map_MapToOutput()
            => "42".Map(Convert.ToInt32)
                .Should().Be(42);

        internal class TestClass
        {
            internal string Status;
        }

        [Test]
        public void Tee_WithAction_ReturnSameObject()
        {
            var input = new TestClass
            {
                Status = "initial"
            };

            var result = input.Tee(_ => _.Status = "final");

            result.Should().Be(input);
            result.Status.Should().Be("final");
        }

        [Test]
        public void Do_CallAction()
        {
            var input = new TestClass
            {
                Status = "initial"
            };

            input.Do(_ => _.Status = "final");

            input.Status.Should().Be("final");
        }
    }
}

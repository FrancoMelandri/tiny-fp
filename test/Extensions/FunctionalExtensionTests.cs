using TinyFp.Extensions;
using NUnit.Framework;
using FluentAssertions;
using System;

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

        [Test]
        public void ToOption_WithMapAndWhenNone_WhenNoValue_AndWhenNoneFalse_None()
        {
            var value = (string)null;
            var option = value.ToOption(_ => Convert.ToInt32(_), _ => false);

            option.IsNone.Should().BeTrue();
        }

        [Test]
        public void ToOption_WithMapAndWhenNone_WhenNoValue_AndWhenNoneTrue_None()
        {
            var value = (string)null;
            var option = value.ToOption(_ => Convert.ToInt32(_), _ => true);

            option.IsNone.Should().BeTrue();
        }

        [Test]
        public void ToOption_WithMapAndWhenNone_WhenValue_AndWhenNoneTrue_None()
        {
            var value = "42";
            var option = value.ToOption(_ => Convert.ToInt32(_), _ => true);

            option.IsNone.Should().BeTrue();
        }

        [Test]
        public void ToOption_WithMapAndWhenNone_WhenValue_AndWhenNoneFalse_SomeConverted()
        {
            var value = "42";
            var option = value.ToOption(_ => Convert.ToInt32(_), _ => false);

            option.IsSome.Should().BeTrue();
            option.OnSome(_ => _.Should().Be(42));
        }

        [Test]
        public void ToOption_WithMapAndWhenNone_WhenValue_AndWhenNoneFalse_Some()
        {
            var value = "42";
            var option = value.ToOption(_ => Convert.ToInt32(_), _ => false);

            option.IsSome.Should().BeTrue();
            option.OnSome(_ => _.Should().Be(42));
        }

        [Test]
        public void ToOption_WithWhenNone_WhenNoValue_AndWhenNoneTrue_None()
        {
            var value = (string)null;
            var option = value.ToOption( _ => true);

            option.IsNone.Should().BeTrue();
        }

        [Test]
        public void ToOption_WithWhenNone_WhenNoValue_AndWhenNoneFalse_None()
        {
            var value = (string)null;
            var option = value.ToOption( _ => false);

            option.IsNone.Should().BeTrue();
        }

        [Test]
        public void ToOption_WithWhenNone_WhenValue_AndWhenNoneTrue_None()
        {
            var value = "42";
            var option = value.ToOption(_ => true);

            option.IsNone.Should().BeTrue();
        }

        [Test]
        public void ToOption_WithWhenNone_WhenValue_AndWhenNoneFalse_Some()
        {
            var value = "42";
            var option = value.ToOption(_ => false);

            option.IsSome.Should().BeTrue();
        }

        [Test]
        public void ToOption_WhenNoValue_None()
        {
            var value = (string)null;
            var option = value.ToOption();

            option.IsNone.Should().BeTrue();
        }

        [Test]
        public void ToOption_WhenValue_Some()
        {
            var value = "42";
            var option = value.ToOption(_ => false);

            option.IsSome.Should().BeTrue();
        }
    }
}

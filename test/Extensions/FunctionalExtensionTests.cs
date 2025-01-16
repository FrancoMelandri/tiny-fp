using NUnit.Framework;
using TinyFp.Extensions;
using Shouldly;

namespace TinyFpTest.Extensions;

[TestFixture]
public class FunctionalExtensionTests
{
    [TestCase(true, "teezed")]
    [TestCase(false, "any")]
    public void TeeWhen_WithTrueOrFalseCondition_TransformOrNot(bool whenResult, string expected)
        => "any".TeeWhen(_ => "teezed", () => whenResult)
            .ShouldBe(expected);

    [TestCase(true, "teezed")]
    [TestCase(false, "any")]
    public void TeeWhen_Action_WithTrueOrFalseCondition_TransformOrNot(bool whenResult, string expected)
        =>  new StringHolder { Value = "any"}
            .TeeWhen(_ => _.Value = "teezed", () => whenResult)
            .Value
            .ShouldBe(expected);

    [TestCase(true, "teezed")]
    [TestCase(false, "any")]
    public void TeeWhen_WithTrueOrFalseConditionOnInput_TransformOrNot(bool whenResult, string expected)
        => "any".TeeWhen(_ => "teezed", _ => whenResult)
            .ShouldBe(expected);

    [TestCase(true, "teezed")]
    [TestCase(false, "any")]
    public void TeeWhen_Action_WithTrueOrFalseConditionOnInput_TransformOrNot(bool whenResult, string expected)
        => new StringHolder { Value = "any" }.TeeWhen(_ =>
            {
                _.Value = "teezed";
            }, _ => whenResult)
            .Value
            .ShouldBe(expected);

    [Test]
    public void Tee_Transform()
    {
        var result = "any".Tee(_ => "teezed");

        result.ShouldBe("teezed");
    }

    [Test]
    public void Map_MapToOutput()
        => "42".Map(Convert.ToInt32)
            .ShouldBe(42);

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

        result.ShouldBe(input);
        result.Status.ShouldBe("final");
    }

    [Test]
    public void Do_CallAction()
    {
        var input = new TestClass
        {
            Status = "initial"
        };

        input.Do(_ => _.Status = "final");

        input.Status.ShouldBe("final");
    }
}
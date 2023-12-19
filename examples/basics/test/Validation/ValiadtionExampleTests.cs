using NUnit.Framework;

namespace TinyFpTest.Examples.Basics.Validation;

[TestFixture]
public class ValiadtionExampleTests
{
    private ValidationExample _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new ValidationExample();
    }

    [Test]
    public void WhenAllDataIsValid_ReturEmptyString()
    {
        var result = ValidationExample.Validate("aa@aa.com", "aaaa", "1234");
        Assert.That(result == "");
    }

    [Test]
    public void WhenEmailIsNotValid_ReturErrorString()
    {
        var result = ValidationExample.Validate("aa.com", "aaaa", "1234");
        Assert.That(result == "invalid mail");
    }

    [Test]
    public void WhenPasswordIsNotValid_ReturErrorString()
    {
        var result = ValidationExample.Validate("aa@aa.com", "aaa", "1234");
        Assert.That(result == "invalid password");
    }

    [Test]
    public void WhenCodeIsNotValid_ReturErrorString()
    {
        var result = ValidationExample.Validate("aa@aa.com", "aaaa", "abcd");
        Assert.That(result == "invalid code");
    }
}
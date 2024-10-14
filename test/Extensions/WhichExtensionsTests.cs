using FluentAssertions;
using NUnit.Framework;
using TinyFp;
using static TinyFp.Extensions.Functional;

namespace TinyFpTest.Extensions;

class WhichExtensionsTests
{
    [Test]
    public void Which_WhenFalse_onFalse()
    {
        var retVal = Which(() => false,
                           () => 1,
                           () => 10);

        retVal.Should().Be(10);
    }

    [Test]
    public void Which_WhenTrue_onTrue()
    {
        var retVal = Which(() => true,
                           () => 1,
                           () => 10);

        retVal.Should().Be(1);
    }

    [Test]
    public void Which_Input_WhenTrue_onTrue()
    {
        const int value = 1;

        var retVal = value.Which(_ => _ == 1,
                                 _ => 1,
                                 _ => 10);

        retVal.Should().Be(1);
    }

    [Test]
    public async Task Which_WhenTrueTask_AndTaskCallbacks_OnTrue()
    {
        var retVal = await Which(() => true,
                           () => 1.AsTask(),
                           () => 10.AsTask());

        retVal.Should().Be(1);
    }

    [Test]
    public async Task Which_Input_WhenTrue_AndTaskCallbacks_nTrue()
    {
        const int value = 1;

        var retVal = await value.Which(_ => _ == 1,
                                 _ => 1.AsTask(),
                                 _ => 10.AsTask());

        retVal.Should().Be(1);
    }
}

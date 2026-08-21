using RestaurantSimulation.Domain.ValueObject;

namespace RestaurantSimulation.Domain.Tests.ValueObject;

public class MoneyTests
{
    [Fact]
    public void Add_ReturnsNewMoneyWithSum()
    {
        var a = new Money(10, Currency.EUR);
        var b = new Money(20, Currency.EUR);

        var result = a.Add(b);
        Assert.Equal(new Money(30, Currency.EUR), result);
    }

    [Fact]
    public void Subtract_ReturnNewMoneyWithDifference()
    {
        var a = new Money(10, Currency.EUR);
        var b = new Money(20, Currency.EUR);

        var result = b.Subtract(a);
        Assert.Equal(new Money(10, Currency.EUR), result);
    }

    [Fact]
    public void Constructor_NegativeAmount_ReturnThrow()
    {
        Assert.Throws<ArgumentException>(() => new Money(-10, Currency.EUR));
    }
}
using RestaurantSimulation.Domain.ValueObject;

namespace RestaurantSimulation.Domain.Tests.ValueObject;

public class MoneyTests
{
    [Fact]
    public void Add_ReturnsNewMoneyWithSum()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(20, Currency.EUR);

        var result = a.Add(b);
        Assert.Equal(Money.Create(30, Currency.EUR), result);
    }
    
    [Fact]
    public void AddTwoDifferentCurrency_ReturnsThrow()
    {
        var a = Money.Create(10, Currency.PLN);
        var b = Money.Create(20, Currency.EUR);
        
        Assert.Throws<ArgumentException>(() => a.Add(b));
    }
    
    [Fact]
    public void Subtract_ReturnsNewMoneyWithDifference()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(20, Currency.EUR);

        var result = b.Subtract(a);
        Assert.Equal(Money.Create(10, Currency.EUR), result);
    }
    
    [Fact]
    public void SubtractGreaterAmount_ThrowsArgumentOutOfRangeException()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(20, Currency.EUR);

        Assert.Throws<ArgumentOutOfRangeException>(() => a.Subtract(b));
    }
    
    [Fact]
    public void SubtractTwoDifferentCurrency_ReturnsThrow()
    {
        var a = Money.Create(10, Currency.PLN);
        var b = Money.Create(20, Currency.EUR);
        
        Assert.Throws<ArgumentException>(()=> a.Subtract(b));
    }

    [Fact]
    public void CreateWithNegativeAmount_ReturnsThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Money.Create(-10, Currency.EUR));
    }
    
    [Fact]
    public void DivideByZero__ReturnsThrow()
    {
        Assert.Throws<DivideByZeroException>(() => Money.Create(10, Currency.EUR).Divide(0));
    }

    [Fact]
    public void MultiplyByCorrectFactor_ReturnsCorrectResult()
    {
        var a = Money.Create(10, Currency.EUR);
        decimal factor = new decimal(1.5);
        var result = a.Multiply(factor);
        Assert.Equal(Money.Create(15, Currency.EUR), result);
    }
    
    [Fact]
    public void MultiplyByNotCorrectFactor_ReturnsException()
    {
        var a = Money.Create(10, Currency.EUR);
        decimal factor = new decimal(-1.5);
        Assert.Throws<ArgumentOutOfRangeException>(() => a.Multiply(factor));
    }

    [Fact]
    public void EqualTwoSameObjects_ReturnsTrue()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(10, Currency.EUR);
        
        Assert.True(a.Equals(b));
    }
    
    [Fact]
    public void EqualTwoDifferentObjects_ReturnsFalse()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(20, Currency.EUR);
        
        Assert.False(a.Equals(b));
    }
    [Fact]
    public void TwoSameObjects_ReturnsSameHashCode()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(10, Currency.EUR);
        
        Assert.True(a.GetHashCode() == b.GetHashCode());
    }
    
    [Fact]
    public void OperatorEqualTwoSameObjects_ReturnsTrue()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(10, Currency.EUR);
        
        Assert.True(a==b);
    }
    
    [Fact]
    public void OperatorEqualTwoDifferentObjects_ReturnsFalse()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(20, Currency.EUR);
        
        Assert.False(a==b);
    }
    
    [Fact]
    public void OperatorNotEqualTwoDifferentObjects_ReturnsTrue()
    {
        var a = Money.Create(10, Currency.EUR);
        var b = Money.Create(20, Currency.EUR);
        
        Assert.True(a!=b);
    }
}
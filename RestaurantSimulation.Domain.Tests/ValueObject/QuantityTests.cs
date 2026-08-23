using RestaurantSimulation.Domain.ValueObject;

namespace RestaurantSimulation.Domain.Tests.ValueObject;

public class QuantityTests
{
    [Fact]
    public void CreateCorrectQuantity_ReturnsQuantity()
    {
        var quantity = Quantity.Create(1, Unit.Portion);
        Assert.NotNull(quantity);
    }
    
    [Fact]
    public void CreateNegativeQuantity_Throw()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Quantity.Create(-1, Unit.Portion));
    }
    
    [Fact]
    public void CreateQuantityObjectsWithFractionalPortionUnit_Throws()
    {
        Assert.Throws<ArgumentException>(() => Quantity.Create(1.5m, Unit.Portion));
    }
    
    [Fact]
    public void CreateFractionalLitre_DoesNotThrow()
    {
        var quantity = Quantity.Create(1.5m, Unit.Litre);
        Assert.Equal(1.5m, quantity.Value);
    }
    
    [Fact]
    public void CreateQuantityObjectsWithZeroValue_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(()=>Quantity.Create(0, Unit.Portion));
    }
    
    [Fact]
    public void EqualTwoSameQuantityObjects_ReturnsTrue()
    {
        var a = Quantity.Create(1, Unit.Portion);
        var b = Quantity.Create(1, Unit.Portion);
        
        Assert.Equal(a,b);
        Assert.True(a.Equals(b));
    }
    
    [Fact]
    public void DifferentUnits_ReturnsFalseThroughObjectEquals()
    {
        var a = Quantity.Create(1, Unit.Portion);
        var b = Quantity.Create(1, Unit.Gram);
        
        Assert.NotEqual(a,b);
        Assert.False(a.Equals(b));
    }
    
    [Fact]
    public void EqualTwoDifferentValueQuantityObjects_ReturnFalse()
    {
        var a = Quantity.Create(1, Unit.Portion);
        var b = Quantity.Create(2, Unit.Portion);
        
        Assert.NotEqual(a,b);
    }
}
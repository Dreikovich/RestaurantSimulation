using RestaurantSimulation.Domain.Aggregates;

namespace RestaurantSimulation.Domain.Tests.Aggregates;

public class RestaurantTests
{
    [Fact]
    public void Create_WithValidCapacity_SetsCapacityAndNoTables()
    {
        var restaurant = Restaurant.Create(10);
        Assert.Empty(restaurant.Tables);
        Assert.Equal(10, restaurant.MaxSeatingCapacity);
    }
    
    [Fact]
    public void AddTable_WithinLimits_AddsTable()
    {
        var restaurant = Restaurant.Create(10);
        restaurant.AddTable(5);
        Assert.Single(restaurant.Tables);
        Assert.Equal(5, restaurant.Tables.Single().Capacity);
    }

    [Fact]
    public void Create_WithInvalidCapacity_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(()=>Restaurant.Create(-10));
    }
    
    [Fact]
    public void AddTable_ExactlyAtLimit_AddsTable()
    {
        var restaurant = Restaurant.Create(10);
        restaurant.AddTable(10);
        Assert.Single(restaurant.Tables);
        Assert.Equal(10, restaurant.MaxSeatingCapacity);
    }
    
    [Fact]
    public void AddTable_WithInvalidCapacity_Throws()
    {
        var restaurant = Restaurant.Create(10);
        Assert.Throws<ArgumentOutOfRangeException>(()=>restaurant.AddTable(11));
        Assert.Empty(restaurant.Tables);
    }
    
    [Fact]
    public void AddTable_WithNegativeCapacity_Throws()
    {
        var restaurant = Restaurant.Create(10);
        Assert.Throws<ArgumentOutOfRangeException>(()=>restaurant.AddTable(-10));
        Assert.Empty(restaurant.Tables);
    }
    
    [Fact]
    public void AddSeveralTable_WithValidCapacity_AddsTables()
    {
        var restaurant = Restaurant.Create(10);
        restaurant.AddTable(2);
        restaurant.AddTable(4);
        restaurant.AddTable(4);
        Assert.Equal(10, restaurant.Tables.Sum(t=>t.Capacity));
    }
    
    [Fact]
    public void AddSeveralTable_WithLastExceedCapacity_AddsOnlyNotExceededTables()
    {
        var restaurant = Restaurant.Create(10);
        restaurant.AddTable(2);
        restaurant.AddTable(4);
        Assert.Throws<ArgumentOutOfRangeException>(()=>restaurant.AddTable(10));
        Assert.Equal(6, restaurant.Tables.Sum(t=>t.Capacity));
    }

    [Fact]
    public void EqualRestaurant_AreEqual()
    {
        var id = RestaurantId.New(); 
        var first = new Restaurant(id, 10);
        var second = new Restaurant(id, 10);
        Assert.Equal(first, second);
    }
}
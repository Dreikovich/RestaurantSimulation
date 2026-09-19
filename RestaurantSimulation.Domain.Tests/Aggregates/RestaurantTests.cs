using RestaurantSimulation.Domain.Aggregates;
using RestaurantSimulation.Domain.Common;
using RestaurantSimulation.Domain.Time;

namespace RestaurantSimulation.Domain.Tests.Aggregates;

public class RestaurantTests
{
    [Fact]
    public void Create_WithValidCapacity_SetsCapacityAndNoTables()
    {
        var restaurant = Restaurant.Create(10, new SystemTimeProvider());
        Assert.Empty(restaurant.Tables);
        Assert.Equal(10, restaurant.MaxSeatingCapacity);
    }
    
    [Fact]
    public void AddTable_WithinLimits_AddsTable()
    {
        var restaurant = Restaurant.Create(10, new SystemTimeProvider());
        restaurant.AddTable(5);
        Assert.Single(restaurant.Tables);
        Assert.Equal(5, restaurant.Tables.Single().Capacity);
    }

    [Fact]
    public void Create_WithInvalidCapacity_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(()=>Restaurant.Create(-10, new SystemTimeProvider()));
    }
    
    [Fact]
    public void AddTable_ExactlyAtLimit_AddsTable()
    {
        var restaurant = Restaurant.Create(10, new SystemTimeProvider());
        restaurant.AddTable(10);
        Assert.Single(restaurant.Tables);
        Assert.Equal(10, restaurant.MaxSeatingCapacity);
    }
    
    [Fact]
    public void AddTable_ExceedMaxLimitCapacity_ReturnFailureResult()
    {
        var restaurant = Restaurant.Create(10, new SystemTimeProvider());
        var result = restaurant.AddTable(11);
        Assert.False(result.IsSuccess);
        Assert.False(string.IsNullOrEmpty(result.Error?.Message));
        Assert.IsType<ExceedMaxSittingCapacityError>(result.Error);
        Assert.Empty(restaurant.Tables);
    }
    
    [Fact]
    public void AddTable_WithNegativeCapacity_ReturnFailureResult()
    {
        var restaurant = Restaurant.Create(10, new SystemTimeProvider());
        var result = restaurant.AddTable(-10);
        Assert.False(result.IsSuccess);
        Assert.False(string.IsNullOrEmpty(result.Error?.Message));
        Assert.IsType<InvalidCapacityError>(result.Error);
        Assert.Empty(restaurant.Tables);
    }
    
    [Fact]
    public void AddSeveralTable_WithValidCapacity_AddsTables()
    {
        var restaurant = Restaurant.Create(10, new SystemTimeProvider());
        restaurant.AddTable(2);
        restaurant.AddTable(4);
        restaurant.AddTable(4);
        Assert.Equal(10, restaurant.Tables.Sum(t=>t.Capacity));
    }
    
    [Fact]
    public void AddSeveralTable_WithLastExceedCapacity_AddsOnlyNotExceededTables()
    {
        var restaurant = Restaurant.Create(10, new SystemTimeProvider());
        restaurant.AddTable(2);
        restaurant.AddTable(4);
        var result = restaurant.AddTable(10);
        Assert.False(result.IsSuccess);
        Assert.False(string.IsNullOrEmpty(result.Error?.Message));
        Assert.IsType<ExceedMaxSittingCapacityError>(result.Error);
        Assert.Equal(6, restaurant.Tables.Sum(t=>t.Capacity));
    }

    [Fact]
    public void EqualRestaurant_AreEqual()
    {
        var id = RestaurantId.New(); 
        var first = new Restaurant(id, 10, new SystemTimeProvider());
        var second = new Restaurant(id, 10, new SystemTimeProvider());
        Assert.Equal(first, second);
    }
}
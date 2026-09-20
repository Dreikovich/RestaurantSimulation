using RestaurantSimulation.Domain.Entities;
using RestaurantSimulation.Domain.Exceptions;

namespace RestaurantSimulation.Domain.Tests.Entities;

public class TableTests
{
    [Fact]
    public void Create_SeatCapacityAndIsFree()
    {
        var table = Table.Create(new TableId(Guid.NewGuid()), 4);
        Assert.Equal(4, table.Capacity);
        Assert.False(table.IsOccupied);
    }
    
    [Fact]
    public void Create_NegativeCapacity_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(()=>Table.Create(new TableId(Guid.NewGuid()), -4));
    }

    [Fact]
    public void Occupy_FreeTable_BecomesOccupied()
    {
        var customerGroup = CustomerGroup.Create(4).Value;
        var table = Table.Create(new TableId(Guid.NewGuid()), 4);
        table.Occupy(customerGroup!);
        Assert.True(table.IsOccupied);
    } 
    
    [Fact]
    public void Occupy_OccupiedTable_Throws()
    {
        var customerGroup = CustomerGroup.Create(4).Value;
        var table = Table.Create(new TableId(Guid.NewGuid()), 4);
        table.Occupy(customerGroup!);
        Assert.Throws<TableAlreadyOccupiedException>(()=>table.Occupy(customerGroup!));
    }
    
    [Fact]
    public void Free_OccupiedTable_BecomesFree()
    {
        var customerGroup = CustomerGroup.Create(4).Value;
        var table = Table.Create(new TableId(Guid.NewGuid()), 4);
        table.Occupy(customerGroup!);
        table.Free();
        Assert.False(table.IsOccupied);
    }

    [Fact]
    public void EqualTableIds_AreEqual()
    {
        var guid = Guid.NewGuid();
        var first = new TableId(guid);
        var second = new TableId(guid);
        Assert.Equal(first, second);
    }
    
}
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
        var table = Table.Create(new TableId(Guid.NewGuid()), 4);
        table.Occupy();
        Assert.True(table.IsOccupied);
    } 
    
    [Fact]
    public void Occupy_OccupiedTable_Throws()
    {
        var table = Table.Create(new TableId(Guid.NewGuid()), 4);
        table.Occupy();
        Assert.Throws<TableAlreadyOccupiedException>(table.Occupy);
    }
    
    [Fact]
    public void Free_OccupiedTable_BecomesFree()
    {
        var table = Table.Create(new TableId(Guid.NewGuid()), 4);
        table.Occupy();
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
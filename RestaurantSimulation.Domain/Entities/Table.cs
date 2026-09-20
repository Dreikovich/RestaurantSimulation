using RestaurantSimulation.Domain.Exceptions;

namespace RestaurantSimulation.Domain.Entities;

public class Table : Entity<TableId>
{
    public int Capacity { get; }
    public bool IsOccupied { get; private set; }
    public CustomerGroup? SeatedGroup { get; private set; }

    private Table(TableId id, int capacity) : base(id)
    {
        Capacity = capacity;
        IsOccupied = false;
    }

    internal static Table Create(TableId id, int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Table capacity cannot be negative or zero");
        }
        return new Table(id, capacity);
    }

    public void Occupy(CustomerGroup customerGroup)
    {
        if (IsOccupied)
        {
            throw new TableAlreadyOccupiedException();
        }

        SeatedGroup = customerGroup;
        IsOccupied = true;
    }

    public void Free()
    {
        SeatedGroup = null;
        IsOccupied = false;
    }

    public bool CanSeat(CustomerGroup customerGroup)
    {
        if (IsOccupied)
        {
            return false;
        }

        return Capacity >= customerGroup.Size;
    }
}

public record struct TableId(Guid Id)
{
    internal static TableId New()
    {
        return new TableId(Guid.NewGuid());
    }
}
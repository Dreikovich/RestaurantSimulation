using RestaurantSimulation.Domain.Exceptions;

namespace RestaurantSimulation.Domain.Entities;

public class Table : Entity<TableId>
{
    public int Capacity { get; }
    public bool IsOccupied { get; private set; }

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

    public void Occupy()
    {
        if (IsOccupied)
        {
            throw new TableAlreadyOccupiedException();
        }

        IsOccupied = true;
    }

    public void Free()
    {
        IsOccupied = false;
    }
}

public record struct TableId(Guid Id)
{
    internal static TableId New()
    {
        return new TableId(Guid.NewGuid());
    }
}
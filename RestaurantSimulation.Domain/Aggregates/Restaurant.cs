using RestaurantSimulation.Domain.Entities;

namespace RestaurantSimulation.Domain.Aggregates;

public class Restaurant : AggregateRoot<RestaurantId>
{
    public int MaxSeatingCapacity { get; }
    private readonly List<Table> _tables;
    private int _totalSeats;
    public IReadOnlyCollection<Table> Tables => _tables;
    
    internal Restaurant(RestaurantId id, int maxSeatingCapacity) : base(id)
    {
        _tables = new();
        MaxSeatingCapacity = maxSeatingCapacity;
    }

    public static Restaurant Create(int maxSeatingCapacity)
    {
        if (maxSeatingCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxSeatingCapacity), "Out of capacity limit");
        }
        
        return new Restaurant(RestaurantId.New(), maxSeatingCapacity);
    }

    public void AddTable(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "capacity cannot be negative or zero");
        }
       
        if (_totalSeats + capacity > MaxSeatingCapacity)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Exceed max seating capacity");
        }
        var table = Table.Create(TableId.New(), capacity);
        _tables.Add(table);
        _totalSeats += capacity;
    }
}

public record struct RestaurantId(Guid Id)
{
    public static RestaurantId New()
    {
        return new RestaurantId(Guid.NewGuid());
    }
}

using RestaurantSimulation.Domain.Common;
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
            throw new ArgumentOutOfRangeException(nameof(maxSeatingCapacity), "Negative or zero value doesnt allow");
        }
        
        return new Restaurant(RestaurantId.New(), maxSeatingCapacity);
    }

    public Result AddTable(int capacity)
    {
        if (capacity <= 0)
        {
            return Result.Failure(new InvalidCapacityError(capacity));
        }
       
        if (_totalSeats + capacity > MaxSeatingCapacity)
        {
            return Result.Failure(new ExceedMaxSittingCapacity(_totalSeats, capacity));
        }
        var table = Table.Create(TableId.New(), capacity);
        _tables.Add(table);
        _totalSeats += capacity;
        return Result.Success();
    }
}

public record struct RestaurantId(Guid Id)
{
    public static RestaurantId New()
    {
        return new RestaurantId(Guid.NewGuid());
    }
}

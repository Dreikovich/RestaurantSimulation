using RestaurantSimulation.Domain.Common;
using RestaurantSimulation.Domain.Entities;

namespace RestaurantSimulation.Domain.Aggregates;

public class Restaurant : AggregateRoot<RestaurantId>
{
    public int MaxSeatingCapacity { get; }
    private readonly List<Table> _tables;
    private readonly List<CustomerGroup> _waitingGuests = new();
    private readonly ITimeProvider _clock;
    private int _totalSeats;
    public IReadOnlyCollection<Table> Tables => _tables;
    private readonly TimeSpan _waitTime = TimeSpan.FromMinutes(10);
    
    internal Restaurant(RestaurantId id, int maxSeatingCapacity, ITimeProvider clock) : base(id)
    {
        _tables = new();
        MaxSeatingCapacity = maxSeatingCapacity;
        _clock = clock;

    }

    public static Restaurant Create(int maxSeatingCapacity, ITimeProvider clock)
    {
        if (maxSeatingCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxSeatingCapacity), "Negative or zero value doesnt allow");
        }
        
        return new Restaurant(RestaurantId.New(), maxSeatingCapacity, clock);
    }

    public Result AddTable(int capacity)
    {
        if (capacity <= 0)
        {
            return Result.Failure(new InvalidCapacityError(capacity));
        }
       
        if (_totalSeats + capacity > MaxSeatingCapacity)
        {
            return Result.Failure(new ExceedMaxSittingCapacityError(_totalSeats, capacity));
        }
        var table = Table.Create(TableId.New(), capacity);
        _tables.Add(table);
        _totalSeats += capacity;
        return Result.Success();
    }

    public Result FreeTable(TableId tableId)
    {
        var table = _tables.FirstOrDefault(t => t.Id == tableId);
        if (table is null)
        {
            return Result.Failure(new NotFoundTableError(tableId));
        }
        
        if (!table.IsOccupied)
        { 
            return Result.Failure(new TableFreeError());
        }

        if ( _waitingGuests.Any())
        {
            var bestGroup = FindBestFitCustomerGroupFromGuestList(table);
            if (bestGroup is not null)
            {
                table.Occupy();
                _waitingGuests.Remove(bestGroup);
                
            }
        }

        return Result.Success();
    }

    private CustomerGroup? FindBestFitCustomerGroupFromGuestList(Table table)
    {
        CustomerGroup? priorityGroup = null;
        CustomerGroup? bestGroup = null;
        
        foreach (var customerGroup in _waitingGuests)
        {
            if (customerGroup.Size > table.Capacity) continue;
            if (bestGroup == null || customerGroup.Size > bestGroup.Size)
            {
                bestGroup = customerGroup;
            }

            if (_clock.UtcNow - customerGroup.WaitingSince < _waitTime) continue;
            if (priorityGroup == null || priorityGroup.WaitingSince < customerGroup.WaitingSince)
            {
                priorityGroup = customerGroup;
            }
        }
        return priorityGroup ?? bestGroup;
    }

    public Result<SeatingOutcome> TrySeat(CustomerGroup customerGroup)
    {
        foreach (var table in _tables)
        {
            if (!table.CanSeat(customerGroup))
            {
                continue;
            }
            
            table.Occupy();
            return Result<SeatingOutcome>.Success(SeatingOutcome.Seated);
        }

        customerGroup.StartWaiting(_clock.UtcNow);
        _waitingGuests.Add(customerGroup);
        return Result<SeatingOutcome>.Success(SeatingOutcome.Waiting);
    }
}

public record struct RestaurantId(Guid Id)
{
    public static RestaurantId New()
    {
        return new RestaurantId(Guid.NewGuid());
    }
}

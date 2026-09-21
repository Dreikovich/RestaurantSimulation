using RestaurantSimulation.Domain.Common;

namespace RestaurantSimulation.Domain.Entities;

//Todo tests
public class CustomerGroup : Entity<CustomerGroupId>
{
    public int Size { get;}
    public DateTime? WaitingSince { get; private set; }
    
    private readonly List<Guest> _guests = new();
    public IReadOnlyCollection<Guest> Guests => _guests;

    private CustomerGroup(CustomerGroupId id, int size, DateTime? time) : base(id)
    {
        Size = size;
        WaitingSince = time;
    }

    public static Result<CustomerGroup> Create(int size)
    {
        if (size <= 0)
        {
            return Result<CustomerGroup>.Failure(new InvalidSizeError(size));
        }

        var customerGroup = new CustomerGroup(CustomerGroupId.New(), size, null);

        for (int i = 0; i < size; i++)
        {
            var guest = Guest.Create(GuestState.Waiting);
            customerGroup._guests.Add(guest.Value!);
        }
        
        return Result<CustomerGroup>.Success(customerGroup);
    }

    public void StartWaiting(DateTime time)
    {
        if (WaitingSince is null)
        {
            WaitingSince = time;
        }
    }

    public void SeatAll()
    {
        foreach (var guest in _guests)
        {
            guest.Seat();
        }
    }

    public void LeaveAll()
    {
        foreach (var guest in _guests)
        {
            guest.Leave();
        }
    }
}

public record struct CustomerGroupId(Guid Id)
{
    public static CustomerGroupId New()
    {
        return new CustomerGroupId(Guid.NewGuid());
    }
}
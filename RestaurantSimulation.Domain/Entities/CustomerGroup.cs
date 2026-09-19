using RestaurantSimulation.Domain.Common;

namespace RestaurantSimulation.Domain.Entities;

//Todo tests
public class CustomerGroup : Entity<CustomerGroupId>
{
    public int Size { get;}
    public DateTime? WaitingSince { get; private set; }

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
        return Result<CustomerGroup>.Success(customerGroup);

    }

    public void StartWaiting(DateTime time)
    {
        if (WaitingSince is null)
        {
            WaitingSince = time;
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
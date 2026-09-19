using RestaurantSimulation.Domain.Common;

namespace RestaurantSimulation.Domain.Entities;

//Todo tests
public class CustomerGroup : Entity<CustomerGroupId>
{
    public int Size { get;}

    private CustomerGroup(CustomerGroupId id, int size) : base(id)
    {
        Size = size;
    }

    public static Result<CustomerGroup> Create(int size)
    {
        if (size <= 0)
        {
            return Result<CustomerGroup>.Failure(new InvalidSizeError(size));
        }

        var customerGroup = new CustomerGroup(CustomerGroupId.New(), size);
        return Result<CustomerGroup>.Success(customerGroup);

    }
    
}

public record struct CustomerGroupId(Guid Id)
{
    public static CustomerGroupId New()
    {
        return new CustomerGroupId(Guid.NewGuid());
    }
}
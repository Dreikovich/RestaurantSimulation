namespace RestaurantSimulation.Domain.Common;

public interface ITimeProvider
{
    public DateTime UtcNow { get; } 
}
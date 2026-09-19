using RestaurantSimulation.Domain.Common;

namespace RestaurantSimulation.Domain.Time;

public class SystemTimeProvider : ITimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
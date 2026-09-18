namespace RestaurantSimulation.Domain.Common;

public sealed record InvalidCapacityError(int capacity)
    : Error($"capacity cannot be negative or zero, provided {capacity}");

public sealed record ExceedMaxSittingCapacity(int totalSeats, int providedCapacity) 
    : Error($"Exceed max seating capacity, total seats now - {totalSeats}, provided capacity - {providedCapacity}");

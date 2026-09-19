using RestaurantSimulation.Domain.Entities;

namespace RestaurantSimulation.Domain.Common;

public sealed record InvalidCapacityError(int capacity)
    : Error($"capacity cannot be negative or zero, provided {capacity}");

public sealed record ExceedMaxSittingCapacityError(int totalSeats, int providedCapacity) 
    : Error($"Exceed max seating capacity, total seats now - {totalSeats}, provided capacity - {providedCapacity}");

public sealed record InvalidSizeError(int size)
    : Error($"size cannot be negative or zero, provided {size}");

public sealed record TableSeatError(int groupSize)
    : Error($"There is no table to match your group size");

public sealed record TableFreeError()
    : Error($"Table is not occupied, cannot free”.");

public sealed record NotFoundTableError(TableId id)
    : Error($"Not found table with id - {id}");

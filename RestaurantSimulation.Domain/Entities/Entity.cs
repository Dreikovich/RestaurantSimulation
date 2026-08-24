namespace RestaurantSimulation.Domain.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; protected set; }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> other && EqualityComparer<TId>.Default.Equals(other.Id, Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
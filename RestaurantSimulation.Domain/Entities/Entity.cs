namespace RestaurantSimulation.Domain.Entities;

public abstract class Entity<TId> where TId : notnull
{
    public TId Id { get; }

    protected Entity(TId id)
    {
        Id = id;
    }
    
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> other && EqualityComparer<TId>.Default.Equals(other.Id, Id);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }
}
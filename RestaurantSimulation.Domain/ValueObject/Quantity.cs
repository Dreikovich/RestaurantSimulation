namespace RestaurantSimulation.Domain.ValueObject;

public sealed class Quantity : IEquatable<Quantity>
{
    public decimal Value { get; }
    public Unit Unit { get; }


    private Quantity(decimal value,  Unit unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Quantity Create(decimal value, Unit unit)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), value, "value cannot be negative or zero" );
        }

        if (unit is Unit.Portion && value % 1 != 0)
        {
            throw new ArgumentException("Portions must be a whole number", nameof(value));
        }
        
        return new Quantity(value, unit);
    }

    public bool Equals(Quantity? other)
    {
        if (other == null)
        {
            return false;
        }
        
        return Value == other.Value && Unit == other.Unit;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Quantity);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, Unit);
    }

    public override string ToString()
    {
        return $"{Value} {Unit}";
    }

    public static bool operator ==(Quantity? a, Quantity? b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(Quantity? a, Quantity? b)
    {
        return !(a == b);
    }
}
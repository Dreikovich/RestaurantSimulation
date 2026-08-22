namespace RestaurantSimulation.Domain.ValueObject;

public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; }
    public Currency Currency { get; }
    
    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount),"Amount cannot be negative");
        }
        return new Money(FromRounded(amount), currency);
    }

    public Money Multiply(decimal factor)
    {
        if (factor < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), "Factor cannot be negative");
        }
        return Create(FromRounded(Amount * factor), Currency);
    }
    
    public Money Divide(decimal factor)
    {
        return factor switch
        {
            0 => throw new DivideByZeroException("Cannot divide by zero"),
            < 0 => throw new ArgumentOutOfRangeException(nameof(factor), "Factor cannot be negative"),
            _ =>  Create(FromRounded(Amount / factor), Currency)
        };
    }

    public Money Add(Money other)
    {
        if (!EnsureSameCurrency(other.Currency))
        {
            throw new ArgumentException("Currency should be the same",nameof(other.Currency));
        }

        var result = Amount + other.Amount;
        return Create(FromRounded(result), Currency);
    }
    
    public Money Subtract(Money other)
    {
        if (!EnsureSameCurrency(other.Currency))
        {
            throw new ArgumentException("Currency should be the same",nameof(other.Currency));
        }

        var result = Amount - other.Amount;
        return Create(FromRounded(result), Currency);
    }

    public bool Equals(Money? money)
    {
        if (money is null)
        {
            return false;
        }
        if (!EnsureSameCurrency(money.Currency))
        {
            return false;
        }
        return money.Amount == Amount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public override string ToString()
    {
        return $"Amount:{Amount:0.00} {Currency}";
    }

    public static bool operator ==(Money? a, Money? b)
    {
        return Equals(a, b);
    }

    public static bool operator !=(Money? a, Money? b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Money);
    }

    private static decimal FromRounded(decimal amount)
    {
        return Math.Round(amount, 2, MidpointRounding.ToEven);
    }

    private bool EnsureSameCurrency(Currency currency)
    {
        return currency == Currency;
    }
}
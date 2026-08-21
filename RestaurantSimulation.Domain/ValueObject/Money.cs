namespace RestaurantSimulation.Domain.ValueObject;

public sealed class Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    public Money(decimal amount, Currency currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative", nameof(amount));
        }

        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money other)
    {
        if (other.Currency != Currency)
        {
            throw new ArgumentException("Cannot add money in different currencies",
                nameof(other));
        }
        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (other.Currency != Currency)
        {
            throw new ArgumentException("Cannot subtract money in different currencies",
                nameof(other));
        }
        
        var result = Amount - other.Amount;
        if (result < 0)
        {
            throw new ArgumentException("Result cannot be negative", nameof(other));
        }

        return new Money(result, Currency);
    }

    public Money Multiply(decimal factor)
    {
        return new Money(Amount * factor, Currency);
    }

    public Money Divide(decimal factor)
    {
        return new Money(Amount / factor, Currency);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Money other) return false;

        return Amount == other.Amount && Currency == other.Currency;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public static bool operator ==(Money? a, Money? b)
    {
        return a?.Equals(b) ?? b is null;
    }

    public static bool operator !=(Money? a, Money? b)
    {
        return !(a == b);
    }
}

public enum Currency
{
    USD,
    PLN, 
    EUR
}
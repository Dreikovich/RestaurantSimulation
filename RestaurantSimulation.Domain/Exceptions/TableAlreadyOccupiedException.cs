namespace RestaurantSimulation.Domain.Exceptions;

public class TableAlreadyOccupiedException : Exception
{
    public TableAlreadyOccupiedException() : base("Table is already occupied")
    {
    }
}
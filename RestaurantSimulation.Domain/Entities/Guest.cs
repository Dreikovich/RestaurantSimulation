using RestaurantSimulation.Domain.Common;

namespace RestaurantSimulation.Domain.Entities;

public class Guest : Entity<GuestId>
{
    public GuestState State { get; private set; } 
    
    private Guest(GuestState state, GuestId id) : base(id)
    {
        State = state;
    }

    public static Result<Guest> Create(GuestState state)
    {
        var guest = new Guest(state, GuestId.New());
        return  Result<Guest>.Success(guest);
    }

    public void Seat()
    {
        if (State != GuestState.Waiting) return;
        State = GuestState.Seated;
    }

    public void Leave()
    {
        if (State == GuestState.Churned || State == GuestState.Departed)
        {
            return;
        }
        
        if (State == GuestState.Seated)
        {
            State = GuestState.Departed;
            return;
        }

        State = GuestState.Churned;
    }
}

public record struct GuestId(Guid Id)
{
    public static GuestId New()
    {
        return new GuestId(Guid.NewGuid());
    }
}

public enum GuestState
{
    Waiting,
    Seated,
    Departed,
    Churned
}
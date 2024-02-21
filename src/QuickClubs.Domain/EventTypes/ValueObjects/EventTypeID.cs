namespace QuickClubs.Domain.EventTypes.ValueObjects;

public sealed record EventTypeID(Guid Value)
{
    public static EventTypeID New() => new(Guid.NewGuid());
}

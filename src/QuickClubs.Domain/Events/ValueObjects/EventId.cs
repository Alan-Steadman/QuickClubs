namespace QuickClubs.Domain.Events.ValueObjects;
public sealed record EventId(Guid Value)
{
    public static EventId New() =>
        new(Guid.NewGuid());
}

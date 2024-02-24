namespace QuickClubs.Domain.EventEntry.ValueObjects;

public sealed record EventEntryId(Guid Value)
{
    public static EventEntryId New() =>
        new(Guid.NewGuid());
}

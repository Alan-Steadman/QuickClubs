namespace QuickClubs.Domain.EventEntries.ValueObjects;

public sealed record EventEntryId(Guid Value)
{
    public static EventEntryId New() =>
        new(Guid.NewGuid());
}

namespace QuickClubs.Domain.Events.ValueObjects;
public sealed record EntryRestrictions(
    int? MinEntries,
    int? MaxEntries,
    DateTime? EntriesOpen,
    DateTime? EntriesClose);

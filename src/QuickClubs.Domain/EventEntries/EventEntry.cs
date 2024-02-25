using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.EventEntries.ValueObjects;
using QuickClubs.Domain.Events.ValueObjects;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Domain.EventEntries;
public class EventEntry : AggregateRoot<EventEntryId>
{
    public EventId EventId { get; private set; }
    public UserId UserId { get; private set; }
    public DateTime EntryDate { get; private set; }
    public bool Paid { get; private set; }

    private EventEntry(
        EventEntryId id,
        EventId eventId,
        UserId userId,
        DateTime entryDate,
        bool paid) : base(id)
    {
        EventId = eventId;
        UserId = userId;
        EntryDate = entryDate;
        Paid = paid;
    }

    public static EventEntry Create(
        EventId eventId,
        UserId userId,
        DateTime entryDate,
        bool paid)
    {
        var entry = new EventEntry(
            EventEntryId.New(),
            eventId,
            userId,
            entryDate,
            false);
        
        return entry;
    }
}

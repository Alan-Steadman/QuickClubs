using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.EventEntries.ValueObjects;
using QuickClubs.Domain.Events.ValueObjects;
using QuickClubs.Domain.EventTypes.ValueObjects;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Domain.Events;
public class Event : AggregateRoot<EventId>
{
    public ClubId ClubId { get; private set; }
    public EventTypeID EventTypeID { get; private set; }
    public LocationId LocationId { get; private set; }
    public EventName Name { get; private set; }
    public EventStart Start { get; private set; }
    public EventEnd End { get; private set; }
    // TODO: ReadOnlyList of All Event Times (include built in times like Start/End, EntriesOpen/EntriesClose, and include all custom event times)
    // TODO: Editable list of Custom Event Times (eg scrutineering starts 9am, lunch starts at 12pm)
    // TODO: Invited clubs
    // TODO: Classes(or categories>)
    // TODO: Event Officials
    public Money EntryFee { get; private set; }
    public bool IsPublished { get; private set; }
    public EntryRestrictions EntryRestrictions { get; private set; }
    public IReadOnlyList<EventEntryId> EventEntries => _eventEntries.AsReadOnly();
    // TODO: Document Attachments


    private readonly List<EventEntryId> _eventEntries = new();
}


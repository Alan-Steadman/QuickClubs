using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Events.ValueObjects;
using QuickClubs.Domain.EventTypes.ValueObjects;

namespace QuickClubs.Domain.Events;
public class Event : AggregateRoot<EventId>
{
    public ClubId ClubId { get; private set; }
    public EventTypeID EventTypeID { get; private set; }
    // TODO: EventLocation
    public EventName Name { get; private set; }
    public EventStart Start { get; private set; }
    public EventEnd End { get; private set;}
    // TODO: Invited clubs
    // TODO: Classes(or categories>)
    // TODO: Event Officials
    public Money EntryFee { get; private set; }
    public bool IsPublished { get; private set; }
    public EntryRestrictions EntryRestrictions { get; private set; }
    // TODO: Entries collection
    // TODO: Document Attachments
}



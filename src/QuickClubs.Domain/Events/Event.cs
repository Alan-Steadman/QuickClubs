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

    // TODO: Invited clubs (do invited clubs belong in EntryRestrictions value object?
    // TODO: Classes(or categories>)
    // TODO: Event Officials
    // TODO: Document Attachments
    public Money EntryFee { get; private set; }
    public bool IsPublished { get; private set; }
    public EntryRestrictions EntryRestrictions { get; private set; }

    public IReadOnlyList<EventEntryId> EventEntries => _eventEntries.AsReadOnly();
    public IReadOnlyList<EventTime> CustomEventTimes => _customEventTimes.AsReadOnly();
    public IReadOnlyList<EventTime> AllEventTimes
    {
        get
        {
            List<EventTime> eventTimes = _customEventTimes;
            eventTimes.Add(new EventTime(Start.Value, "Start", null));
            eventTimes.Add(new EventTime(Start.Value, "End", null));
            return _customEventTimes.AsReadOnly();
        }
    }

    private readonly List<EventEntryId> _eventEntries = new();
    private readonly List<EventTime> _customEventTimes = new();

    private Event(
        EventId id,
        ClubId clubId,
        EventTypeID eventTypeID,
        LocationId locationId,
        EventName name,
        EventStart start,
        EventEnd end,
        Money entryFee,
        bool isPublished,
        EntryRestrictions entryRestrictions) : base(id)
    {
        ClubId = clubId;
        EventTypeID = eventTypeID;
        LocationId = locationId;
        Name = name;
        Start = start;
        End = end;
        EntryFee = entryFee;
        IsPublished = isPublished;
        EntryRestrictions = entryRestrictions;
    }

    public static Event Create(
        ClubId clubId,
        EventTypeID eventTypeID,
        LocationId locationId,
        EventName name,
        EventStart start,
        EventEnd end,
        Money entryFee,
        bool isPublished,
        EntryRestrictions entryRestrictions)
    {
        return new (
            EventId.New(),
            clubId,
            eventTypeID,
            locationId,
            name,
            start,
            end,
            entryFee,
            isPublished,
            entryRestrictions);
    }

#pragma warning disable CS8618
    private Event()
    {
    }
#pragma warning restore CS8618
}

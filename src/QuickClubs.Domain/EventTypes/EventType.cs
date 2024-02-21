using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.EventTypes.ValueObjects;

namespace QuickClubs.Domain.EventTypes;
public sealed class EventType : AggregateRoot<EventTypeID>
{
    EventTypeName Name { get; set; }

    private EventType (
        EventTypeID id,
        EventTypeName name) : base(id)
    {
        Name = name;
    }

    public static EventType Create(EventTypeName name)
    {
        var eventType = new EventType(
            EventTypeID.New(),
            name);

        return eventType;
    }
}

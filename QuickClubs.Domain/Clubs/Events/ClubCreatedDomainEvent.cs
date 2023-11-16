using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;

namespace QuickClubs.Domain.Clubs.Events;

public sealed record ClubCreatedDomainEvent(ClubId ClubId) : IDomainEvent
{
}

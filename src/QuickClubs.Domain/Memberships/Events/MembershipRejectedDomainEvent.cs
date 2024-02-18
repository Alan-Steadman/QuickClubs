using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Memberships.ValueObjects;

namespace QuickClubs.Domain.Memberships.Events;
public sealed record MembershipRejectedDomainEvent(MembershipId membershipId) : IDomainEvent;

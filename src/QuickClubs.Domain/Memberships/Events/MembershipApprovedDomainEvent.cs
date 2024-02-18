using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Memberships.ValueObjects;

namespace QuickClubs.Domain.Memberships.Events;

public sealed record MembershipApprovedDomainEvent(MembershipId membershipId) : IDomainEvent;

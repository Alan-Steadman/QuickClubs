using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;

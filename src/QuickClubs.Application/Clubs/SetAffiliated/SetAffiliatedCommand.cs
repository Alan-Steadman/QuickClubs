using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.Application.Clubs.SetAffiliated;

public sealed record SetAffiliatedCommand(
    Guid ClubId,
    string CurrencyCode,
    bool MembershipNeedsApproval) : ICommand;
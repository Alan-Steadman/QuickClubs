namespace QuickClubs.Contracts.Clubs;

public record SetAffiliatedRequest(
    string CurrencyCode,
    bool MembershipNeedsApproval);

namespace QuickClubs.Contracts.Clubs;

public sealed record ClubResponse(
    Guid Id,
    string FullName,
    string Acronym,
    string Website,
    bool IsAffiliate);
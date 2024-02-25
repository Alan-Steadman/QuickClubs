namespace QuickClubs.Contracts.Clubs;

public sealed record ClubResponse(
    Guid Id,
    string ClubType,
    string FullName,
    string Acronym,
    string Website,
    bool IsAffiliate);
namespace QuickClubs.Application.Clubs.Common;

public sealed record ClubResult(
    Guid Id,
    string ClubType,
    string FullName,
    string Acronym,
    string Website,
    bool IsAffiliate);
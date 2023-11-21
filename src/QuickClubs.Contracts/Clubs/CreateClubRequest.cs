namespace QuickClubs.Contracts.Clubs;

public sealed record CreateClubRequest(
    string FullName,
    string Acronym,
    string Website);
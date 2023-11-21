using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.Application.Clubs.CreateClub;

public sealed record CreateClubCommand(
    string FullName,
    string Acronym,
    string Website) : ICommand<Guid>;
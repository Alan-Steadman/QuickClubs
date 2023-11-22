using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Clubs.Common;

namespace QuickClubs.Application.Clubs.GetAllClubs;

public sealed record GetAllClubsQuery() : IQuery<IEnumerable<ClubResult>>;

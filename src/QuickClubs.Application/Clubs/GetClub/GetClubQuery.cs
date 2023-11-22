using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Clubs.Common;

namespace QuickClubs.Application.Clubs.GetClub;

public sealed record GetClubQuery(Guid ClubId) : IQuery<ClubResult>;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Memberships.Common;

namespace QuickClubs.Application.Memberships.GetAllClubMembers;

public sealed record GetAllClubMembersQuery(
    Guid ClubId,
    DateTime? MemberAtDate) : IQuery<IEnumerable<MemberResult>>;
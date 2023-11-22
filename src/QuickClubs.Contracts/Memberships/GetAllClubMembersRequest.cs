namespace QuickClubs.Contracts.Memberships;

public sealed record GetAllClubMembersRequest(
    Guid ClubId,
    DateTime? MemberAtDate);
namespace QuickClubs.Contracts.Memberships;
public sealed record ApprovalResponse(
    bool IsApproved,
    string ApprovalStatus,
    Guid? ApprovedByUserId,
    string ApprovedByName,
    DateTime? ApprovedDate,
    string? ApprovalReason);
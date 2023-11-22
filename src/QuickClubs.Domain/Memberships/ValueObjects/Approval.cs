using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Domain.Memberships.ValueObjects;

public sealed record Approval(
    bool IsApproved,
    ApprovalStatus ApprovalStatus,
    UserId? ApprovedBy,
    DateTime? ApprovedDate,
    string? Reason);

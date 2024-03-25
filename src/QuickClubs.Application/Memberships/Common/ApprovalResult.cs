namespace QuickClubs.Application.Memberships.Common;
public sealed class ApprovalResult
{
    public bool IsApproved { get; init; }
    public string ApprovalStatus { get; init; }
    public Guid? ApprovedByUserId { get; init; }
    public string? ApprovedByName { get; init; }
    public DateTime? ApprovedDate { get; init; }
    public string? ApprovalReason { get; init; }
}
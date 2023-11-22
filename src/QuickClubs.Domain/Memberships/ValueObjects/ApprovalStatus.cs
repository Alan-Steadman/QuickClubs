namespace QuickClubs.Domain.Memberships.ValueObjects;

public sealed record ApprovalStatus
{
    public static ApprovalStatus NotSet = new ApprovalStatus("NotSet", false);
    public static ApprovalStatus NotRequired = new ApprovalStatus("NotRequired", true);
    public static ApprovalStatus Approved = new ApprovalStatus("Approved", true);
    public static ApprovalStatus Rejected = new ApprovalStatus("Rejected", false);

    public string Name { get; init; }
    public bool IsApproved { get; init; }

    private ApprovalStatus(string name, bool isApproved)
    {
        Name = name;
        IsApproved = isApproved;
    }
    public static ApprovalStatus FromString(string value)
    {
        return All.FirstOrDefault(a => a.Name == value) ??
            throw new ApplicationException("The approval status is invalid");
    }

    public override string ToString()
    {
        return Name;
    }

    public static readonly IReadOnlyCollection<ApprovalStatus> All = new[]
    {
        NotSet,
        NotRequired,
        Approved,
        Rejected
    };
}
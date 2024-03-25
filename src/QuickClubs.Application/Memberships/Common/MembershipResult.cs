namespace QuickClubs.Application.Memberships.Common;
public sealed class MembershipResult
{ 
    public Guid Id { get; init; }
    public Guid ClubId { get; init; }
    public Guid MembershipOptionId { get; init; }
    public Guid MembershipLevelId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string MembershipNumber { get; init; }
    public string MembershipName { get; init; }
    public decimal PriceAmount { get; init; }
    public string PriceCurrency { get; init; }
    public string PriceFormatted { get; init; }
    public bool Paid { get; init; }
    public ApprovalResult Approval { get; set; }
    public List<MembershipMemberResult> Members { get; set; } = [];
}
public sealed class MembershipMemberResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
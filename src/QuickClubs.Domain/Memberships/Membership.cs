using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.MembershipOptions.ValueObjects;
using QuickClubs.Domain.Memberships.Events;
using QuickClubs.Domain.Memberships.ValueObjects;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Domain.Memberships;
public sealed class Membership : AggregateRoot<MembershipId>
{
    private readonly List<UserId> _members = new();

    public ClubId ClubId { get; private set; } = null!;
    public IReadOnlyList<UserId> Members => _members.AsReadOnly();
    public MembershipOptionId MembershipOptionId { get; private set; } = null!;
    public MembershipLevelId MembershipLevelId { get; private set; } = null!;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public MembershipNumber MembershipNumber { get; private set; } = null!;
    public MembershipName MembershipName { get; private set; } = null!;
    public Money Price { get; private set; } = null!;
    public bool Paid { get; private set; }
    public Approval Approval { get; private set; } = null!;

    public bool IsActive(DateTime utcNow) => Approval.IsApproved && Paid && (StartDate < utcNow && EndDate > utcNow);

    private Membership(
        MembershipId id,
        ClubId clubId,
        List<UserId> members,
        MembershipOptionId membershipOptionId,
        MembershipLevelId membershipLevelId,
        DateTime startDate,
        DateTime endDate,
        MembershipNumber membershipNumber,
        MembershipName membershipName,
        Money price,
        bool paid,
        Approval approval)
        : base(id)
    {
        ClubId = clubId;
        _members = members;
        MembershipOptionId = membershipOptionId;
        MembershipLevelId = membershipLevelId;
        StartDate = startDate;
        EndDate = endDate;
        MembershipNumber = membershipNumber;
        MembershipName = membershipName;
        Price = price;
        Paid = paid;
        Approval = approval;
    }

    public static Membership Create(
        ClubId clubId,
        List<UserId> members,
        MembershipOptionId membershipOptionId,
        MembershipLevelId membershipLevelId,
        DateTime startDate,
        DateTime endDate,
        MembershipNumber membershipNumber,
        MembershipName membershipName,
        Money price,
        bool membershipNeedsApproval)
    {
        var approvalStatus = membershipNeedsApproval ? ApprovalStatus.NotSet : ApprovalStatus.NotRequired;

        var approval = new Approval(
            IsApproved: approvalStatus.IsApproved,
            approvalStatus,
            ApprovedBy: null,
            ApprovedDate: null,
            Reason: null);

        var membership = new Membership(
            MembershipId.New(),
            clubId,
            members,
            membershipOptionId,
            membershipLevelId,
            startDate,
            endDate,
            membershipNumber,
            membershipName,
            price,
            paid: false,
            approval);

        if (approvalStatus.IsApproved)
            membership.RaiseDomainEvent(new MembershipApprovedDomainEvent(membership.Id));

        return membership;
    }

    public void SetPaid()
    {
        Paid = true;
    }

    public void SetApproved(UserId approvedBy, DateTime utcNow, string? reason)
    {
        var approvalStatus = ApprovalStatus.Approved;

        var approval = new Approval(
            IsApproved: approvalStatus.IsApproved,
            approvalStatus,
            ApprovedBy: approvedBy,
            ApprovedDate: utcNow,
            Reason: reason);

        Approval = approval;

        RaiseDomainEvent(new MembershipApprovedDomainEvent(Id));
    }

    public void SetRejected(UserId rejectedBy, DateTime UtcNow, string reason)
    {
        var approvalStatus = ApprovalStatus.Rejected;

        var approval = new Approval(
            IsApproved: approvalStatus.IsApproved,
            approvalStatus,
            ApprovedBy: rejectedBy,
            ApprovedDate: UtcNow,
            Reason: reason);

        Approval = approval;

        RaiseDomainEvent(new MembershipRejectedDomainEvent(Id));
    }

#pragma warning disable CS8618
    private Membership()
    {
    }
#pragma warning restore CS8618
}

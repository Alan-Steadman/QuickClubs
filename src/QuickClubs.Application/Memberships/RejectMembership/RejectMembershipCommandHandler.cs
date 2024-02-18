using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Memberships.Errors;
using QuickClubs.Domain.Memberships.Repository;
using QuickClubs.Domain.Memberships.ValueObjects;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Memberships.RejectMembership;

public sealed class RejectMembershipCommandHandler : ICommandHandler<RejectMembershipCommand>
{
    private readonly IMembershipRepository _membershipRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RejectMembershipCommandHandler(
        IMembershipRepository membershipRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(RejectMembershipCommand request, CancellationToken cancellationToken)
    {
        var membership = await _membershipRepository.GetByIdAsync(new MembershipId(request.MembershipId), cancellationToken);
        if (membership is null)
            return Result.Failure(MembershipErrors.NotFound(request.MembershipId));

        // TODO: Decide if any checks are necessary to make sure the membership application is in a state that can be rejected (eg should only "NotSet" be rejectable?  Should it be possible to reject a membership after it has been approved?  Should it be possible to reject a membership that is automatically approved when it is created?
        // For now let's assume only "NotSet" be rejectable:
        if (membership.Approval.ApprovalStatus != ApprovalStatus.NotSet)
            return Result.Failure(MembershipErrors.NotInARejectableState);

        var rejectedBy = await _userRepository.GetByIdAsync(new UserId(request.RejectedByUserId), cancellationToken);
        if (rejectedBy is null)
            return Result.Failure(UserErrors.NotFound(request.RejectedByUserId));
        // TODO: Check RejectedByUserId has authorization to reject memberships

        membership.SetRejected(
            rejectedBy.Id,
            _dateTimeProvider.UtcNow,
            reason: request.Reason.Trim());

        _membershipRepository.Update(membership);

        return Result.Success();
    }
}

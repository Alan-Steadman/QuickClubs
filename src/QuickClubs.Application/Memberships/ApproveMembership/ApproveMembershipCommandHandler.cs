using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Memberships.Errors;
using QuickClubs.Domain.Memberships.Repository;
using QuickClubs.Domain.Memberships.ValueObjects;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Memberships.ApproveMembership;

public sealed class ApproveMembershipCommandHandler : ICommandHandler<ApproveMembershipCommand>
{
    private readonly IMembershipRepository _membershipRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ApproveMembershipCommandHandler(
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

    public async Task<Result> Handle(ApproveMembershipCommand request, CancellationToken cancellationToken)
    {
        var membership = await _membershipRepository.GetByIdAsync(new MembershipId(request.MembershipId), cancellationToken);
        if (membership is null)
            return Result.Failure(MembershipErrors.NotFound(request.MembershipId));

        if (membership.Approval.IsApproved)
            return Result.Failure(MembershipErrors.AlreadyApproved);

        var approvedBy = await _userRepository.GetByIdAsync(new UserId(request.ApprovedByUserId), cancellationToken);
        if (approvedBy is null)
            return Result.Failure(UserErrors.NotFound(request.ApprovedByUserId));

        membership.SetApproved(
            approvedBy: approvedBy.Id,
            utcNow: _dateTimeProvider.UtcNow,
            reason: request.Reason?.Trim() == "" ? null : request.Reason?.Trim());

        _membershipRepository.Update(membership);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

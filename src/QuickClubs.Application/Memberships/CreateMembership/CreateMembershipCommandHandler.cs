using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Memberships.Common;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.MembershipOptions.Errors;
using QuickClubs.Domain.MembershipOptions.Repository;
using QuickClubs.Domain.MembershipOptions.ValueObjects;
using QuickClubs.Domain.Memberships;
using QuickClubs.Domain.Memberships.Repository;
using QuickClubs.Domain.Memberships.Services;
using QuickClubs.Domain.Memberships.ValueObjects;
using QuickClubs.Domain.Users.Errors;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Memberships.CreateMembership;

public sealed class CreateMembershipCommandHandler : ICommandHandler<CreateMembershipCommand, MembershipResult>
{
    private readonly IClubRepository _clubRepository;
    private readonly IMembershipOptionRepository _membershipOptionRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly EndDateService _endDateService;

    public CreateMembershipCommandHandler(
        IClubRepository clubRepository,
        IMembershipOptionRepository membershipOptionRepository,
        IMembershipRepository membershipRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        EndDateService endDateService)
    {
        _clubRepository = clubRepository;
        _membershipOptionRepository = membershipOptionRepository;
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _endDateService = endDateService;
    }

    public async Task<Result<MembershipResult>> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
    {
        var membershipOption = await _membershipOptionRepository.GetByIdAsync(new MembershipOptionId(request.MembershipOptionId), cancellationToken);
        if (membershipOption is null)
            return Result.Failure<MembershipResult>(MembershipOptionErrors.NotFound(request.MembershipOptionId));

        var membershipLevel = membershipOption.Levels.FirstOrDefault(l => l.Id == new MembershipLevelId(request.MembershipLevelId));
        if (membershipLevel is null)
            return Result.Failure<MembershipResult>(MembershipOptionErrors.MembershipLevelNotFound(request.MembershipLevelId));

        var club = await _clubRepository.GetByIdAsync(membershipOption.ClubId, cancellationToken);
        if (club is null)
            return Result.Failure<MembershipResult>(ClubErrors.NotFound(membershipOption.ClubId.Value));

        var user = await _userRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);
        if (user is null)
            return Result.Failure<MembershipResult>(UserErrors.NotFound(request.UserId));

        var members = new List<UserId> { user.Id };

        foreach (var additionalMemberUserId in request.AdditionalMembers)
        {
            var additionalUser = await _userRepository.GetByIdAsync(new UserId(additionalMemberUserId), cancellationToken);
            if (additionalUser is null)
                return Result.Failure<MembershipResult>(UserErrors.NotFound(additionalMemberUserId));

            members.Add(additionalUser.Id);
        }

        // TODO: Check members count vs membership option
        // TODO: Check ages of users vs membership option - return error if not set
        // TODO: Check users have an address
        // TODO: Check all users share an address (just have the same postcode?)
        // TODO: Check users don't already have membership to this club for the same period

        var membership = Membership.Create(
            clubId: club.Id,
            members: members,
            membershipOption.Id,
            membershipLevel.Id,
            startDate: _dateTimeProvider.UtcNow,
            endDate: _endDateService.CalculateEndDate(membershipOption, _dateTimeProvider.UtcNow),
            membershipNumber: new MembershipNumber("1"), // TODO: create a service that generates membership numbers according to Club's "MembershipNumberPolicy" mentioned in ClubEngine.Docs.DomainModels.md
            membershipName: new MembershipName($"{membershipOption.Name.Value} {membershipLevel.Name.Value}"),
            price: membershipLevel.Price,
            membershipNeedsApproval: club.GetSettings().MembershipNeedsApproval
            );

        _membershipRepository.Add(membership);
        await _unitOfWork.SaveChangesAsync();

        var result = new MembershipResult 
        {
            Id = membership.Id.Value,
            ClubId = membership.ClubId.Value,
            MembershipOptionId = membership.MembershipOptionId.Value,
            MembershipLevelId = membership.MembershipLevelId.Value,
            StartDate = membership.StartDate,
            EndDate = membership.EndDate,
            MembershipNumber = membership.MembershipNumber.Value,
            MembershipName = membership.MembershipName.Value,
            PriceAmount = membership.Price.Amount,
            PriceCurrency = membership.Price.Currency.Code,
            PriceFormatted = membership.Price.Currency.Symbol + membership.Price.Amount,
            Paid = membership.Paid,
            Approval = new ApprovalResult
            {
                IsApproved = membership.Approval.IsApproved,
                ApprovalStatus = membership.Approval.ApprovalStatus.Name,
                ApprovedByUserId = membership.Approval.ApprovedBy?.Value,
                ApprovedDate = membership.Approval.ApprovedDate,
                ApprovalReason = membership.Approval.Reason
            },
            Members = membership.Members.Select(m => new MembershipMemberResult
            {
                UserId = m.Value
            }).ToList()
        }; 

        return Result.Success<MembershipResult>(result);
    }
}

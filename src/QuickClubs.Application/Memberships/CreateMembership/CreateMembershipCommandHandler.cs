using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.MembershipOptions.Errors;
using QuickClubs.Domain.MembershipOptions.Repository;
using QuickClubs.Domain.Memberships;
using QuickClubs.Domain.Memberships.Repository;
using QuickClubs.Domain.Memberships.Services;
using QuickClubs.Domain.Memberships.ValueObjects;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Application.Memberships.CreateMembership;

internal sealed class CreateMembershipCommandHandler : ICommandHandler<CreateMembershipCommand, Guid>
{
    private readonly IClubRepository _clubRepository;
    private readonly IMembershipOptionRepository _membershipOptionRepository;
    private readonly IMembershipRepository _membershipRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly EndDateService _endDateService;

    public CreateMembershipCommandHandler(
        IClubRepository clubRepository,
        IMembershipOptionRepository membershipOptionRepository,
        IMembershipRepository membershipRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        EndDateService endDateService)
    {
        _clubRepository = clubRepository;
        _membershipOptionRepository = membershipOptionRepository;
        _membershipRepository = membershipRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _endDateService = endDateService;
    }

    public async Task<Result<Guid>> Handle(CreateMembershipCommand request, CancellationToken cancellationToken)
    {
        var membershipOption = await _membershipOptionRepository.GetByIdAsync(request.MembershipOptionId, cancellationToken);
        if (membershipOption is null)
        {
            return Result.Failure<Guid>(MembershipOptionErrors.NotFound);
        }

        var membershipLevel = membershipOption.Levels.FirstOrDefault(l => l.Id == request.MembershipLevelId);
        if (membershipLevel is null)
        {
            return Result.Failure<Guid>(MembershipOptionErrors.MembershipLevelNotFound);
        }

        var club = await _clubRepository.GetByIdAsync(membershipOption.ClubId);
        if (club is null)
        {
            return Result.Failure<Guid>(ClubErrors.NotFound);
        }

        var members = new List<UserId>
        {
            request.UserId
        };
        members.AddRange(request.AdditionalMembers);

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
            membershipNumber: new MembershipNumber("1"), // TODO: create a service that generates membership numbers according to Club
            membershipName: new MembershipName($"{membershipOption.Name} {membershipLevel.Name}"),
            price: membershipLevel.Price,
            membershipNeedsApproval: club.GetSettings().MembershipNeedsApproval
            );

        _membershipRepository.Add(membership);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success<Guid>(membership.Id.Value);
    }
}

using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.MembershipOptions.Entities;
using QuickClubs.Domain.MembershipOptions.Repository;
using QuickClubs.Domain.MembershipOptions.ValueObjects;

namespace QuickClubs.Application.MembershipOptions.CreateMembershipOption;

public sealed class CreateMembershipOptionCommandHandler : ICommandHandler<CreateMembershipOptionCommand, Guid>
{
    private readonly IMembershipOptionRepository _membershipOptionRepository;
    private readonly IClubRepository _clubRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMembershipOptionCommandHandler(
        IMembershipOptionRepository membershipOptionRepository,
        IClubRepository clubRepository,
        IUnitOfWork unitOfWork)
    {
        _membershipOptionRepository = membershipOptionRepository;
        _clubRepository = clubRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateMembershipOptionCommand request, CancellationToken cancellationToken)
    {
        var club = await _clubRepository.GetByIdAsync(new ClubId(request.ClubId), cancellationToken);

        if (club is null)
        {
            return Result.Failure<Guid>(ClubErrors.NotFound);
        }

        if (!club.IsAffiliate)
        {
            return Result.Failure<Guid>(ClubErrors.NotAffiliated);
        }

        var membershipOption = Domain.MembershipOptions.MembershipOption.Create(
            new ClubId(request.ClubId),
            new MembershipOptionName(request.Name),
            MembershipPeriod.FromString(request.Period),
            request.HasCutoff ? new Cutoff(request.CutoffMonth, request.CutoffDay) : null,
            request.Levels.ConvertAll(level => MembershipLevel.Create(
                Name: new MembershipLevelName(level.Name),
                Description: new MembershipLevelDescription(level.Description),
                MaxMembers: level.MaxMembers,
                MinAge: level.MinAge,
                MaxAge: level.MaxAge,
                Price: new Money(level.PriceAmount, club.GetSettings().Currency)
            )));

        _membershipOptionRepository.Add(membershipOption);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success(membershipOption.Id.Value);
    }
}

using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;

namespace QuickClubs.Application.Clubs.SetAffiliated;

public sealed class SetAffiliatedCommandHandler : ICommandHandler<SetAffiliatedCommand>
{
    private readonly IClubRepository _clubRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SetAffiliatedCommandHandler(IClubRepository clubRepository, IUnitOfWork unitOfWork)
    {
        _clubRepository = clubRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SetAffiliatedCommand request, CancellationToken cancellationToken)
    {
        var club = await _clubRepository.GetByIdAsync(new ClubId(request.ClubId), cancellationToken);

        if (club is null)
        {
            return Result.Failure(ClubErrors.NotFound(request.ClubId));
        }

        var result = club.SetAffiliated(
            Currency.FromCode(request.CurrencyCode),
            request.MembershipNeedsApproval);

        if (result.IsFailure)
        {
            return result;
        }

        _clubRepository.Update(club);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

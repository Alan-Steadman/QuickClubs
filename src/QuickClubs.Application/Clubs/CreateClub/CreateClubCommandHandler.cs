using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.ClubTypes;
using QuickClubs.Domain.ClubTypes.Errors;

namespace QuickClubs.Application.Clubs.CreateClub;

public sealed class CreateClubCommandHandler : ICommandHandler<CreateClubCommand, Guid>
{
    private readonly IClubRepository _clubRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClubCommandHandler(IClubRepository clubRepository, IUnitOfWork unitOfWork)
    {
        _clubRepository = clubRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateClubCommand request, CancellationToken cancellationToken)
    {
        var clubType = ClubType.FromCode(request.ClubTypeCode);

        if (clubType is null)
            return Result.Failure<Guid>(ClubTypeErrors.NotFound(request.ClubTypeCode));

        if (!await _clubRepository.IsFullNameUniqueAsync(null, request.FullName, cancellationToken))
            return Result.Failure<Guid>(ClubErrors.DuplicateFullName(request.FullName));

        if (!await _clubRepository.IsAcronymUniqueAsync(null, request.Acronym, cancellationToken))
            return Result.Failure<Guid>(ClubErrors.DuplicateAcronym(request.Acronym));

        if (!await _clubRepository.IsWebsiteUniqueAsync(null, request.Website, cancellationToken))
            return Result.Failure<Guid>(ClubErrors.DuplicateWebsite(request.Website));

        var club = Club.Create(
            clubType,
            new ClubName(request.FullName, request.Acronym),
            new ClubWebsite(request.Website));

        _clubRepository.Add(club);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return club.Id.Value;
    }
}

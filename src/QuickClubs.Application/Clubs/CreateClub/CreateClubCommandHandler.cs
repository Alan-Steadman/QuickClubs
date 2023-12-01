using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Clubs.ValueObjects;

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
        if (!await _clubRepository.IsFullNameUniqueAsync(null, request.FullName, cancellationToken))
        {
            return Result.Failure<Guid>(ClubErrors.DuplicateFullName(request.FullName));
        }

        if (!await _clubRepository.IsAcronymUniqueAsync(null, request.Acronym, cancellationToken))
        {
            return Result.Failure<Guid>(ClubErrors.DuplicateAcronym);
        }

        if (!await _clubRepository.IsWebsiteUniqueAsync(null, request.Website, cancellationToken))
        {
            return Result.Failure<Guid>(ClubErrors.DuplicateWebsite);
        }

        var club = Club.Create(
            new ClubName(request.FullName, request.Acronym),
            new ClubWebsite(request.Website));

        _clubRepository.Add(club);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return club.Id.Value;
    }
}

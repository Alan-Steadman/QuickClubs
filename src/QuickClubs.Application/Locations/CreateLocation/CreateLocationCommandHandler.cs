using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Locations;
using QuickClubs.Domain.Locations.Errors;
using QuickClubs.Domain.Locations.Repository;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Application.Locations.CreateLocation;
public sealed class CreateLocationCommandHandler : ICommandHandler<CreateLocationCommand, Guid>
{
    private readonly IClubRepository _clubRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLocationCommandHandler(
        IClubRepository clubRepository,
        ILocationRepository locationRepository,
        IUnitOfWork unitOfWork)
    {
        _clubRepository = clubRepository;
        _locationRepository = locationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var clubId = new ClubId(request.ClubId);

        if (await _clubRepository.GetByIdAsync(clubId) is null)
            return Result.Failure<Guid>(ClubErrors.NotFound(request.ClubId));

        if (await _locationRepository.IsNameUniqueAsync(null, clubId, request.Name, cancellationToken))
            return Result.Failure<Guid>(LocationErrors.DuplicateName(request.Name));

        var location = Location.Create(
            clubId,
            new LocationName(request.Name),
            new Position(
                request.AddressPostcode is null ? null 
                    : new Address(
                        request.AddressBuilding!,
                        request.AddressStreet!,
                        request.AddressLocality!,
                        request.AddressTown!,
                        request.AddressCounty!,
                        request.AddressPostcode),
                request.WhatThreeWords is null ? null : new WhatThreeWords(request.WhatThreeWords),
                request.OsGridRef is null ? null : new OsGridRef(request.OsGridRef)
                    ),
            request.Directions is null ? null : new Directions(request.Directions));

        _locationRepository.Add(location);

        await _unitOfWork.SaveChangesAsync();

        return location.Id.Value;
    }
}

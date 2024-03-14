using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Locations.Common;

namespace QuickClubs.Application.Locations.GetAllLocations;

public sealed record GetAllLocationsQuery(Guid ClubId) : IQuery<IEnumerable<LocationResult>>;
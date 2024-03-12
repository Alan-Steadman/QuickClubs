using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Locations.Common;

namespace QuickClubs.Application.Locations.GetLocation;
public sealed record GetLocationQuery(Guid LocationId) : IQuery<LocationResult>;

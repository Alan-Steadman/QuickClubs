using Asp.Versioning;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Locations.Common;
using QuickClubs.Application.Locations.CreateLocation;
using QuickClubs.Application.Locations.GetAllLocations;
using QuickClubs.Application.Locations.GetLocation;
using QuickClubs.Contracts.Locations;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Presentation.Controllers;

[Route("api/v{v:apiVersion}/clubs/{clubId:guid}/locations/")]
public sealed class LocationsController : ApiController
{
    private readonly IMapper _mapper;

    public LocationsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new location
    /// </summary>
    /// <param name="clubId">The club in which to create the new location</param>
    /// <param name="request">The CreateLocationRequest</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A LocationResponse of the newly create location</returns>
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<ActionResult<LocationResponse>> CreateLocation(
        Guid clubId,
        CreateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateLocationCommand>((clubId, request));

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            CreatedAtAction(
                nameof(GetLocation),
                new { clubId = result.Value.ClubId, id = result.Value.Id },
                MapResult(result.Value))
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Retrieves a location matching the supplied id
    /// </summary>
    /// <param name="id">The id of the location to retrieve</param>
    /// <returns>A LocationResponse</returns>
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [MapToApiVersion(1)]
    public async Task<ActionResult<LocationResponse>> GetLocation(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetLocationQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ?
            base.Ok(MapResult(result))
            : base.NotFound();
    }

    /// <summary>
    /// Retrieves all locations
    /// </summary>
    /// <returns>An IEnumerable of type LocationResponse</returns>
    [AllowAnonymous]
    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<ActionResult<IEnumerable<LocationResponse>>> GetAllLocations(Guid clubId, CancellationToken cancellationToken)
    {
        var query = new GetAllLocationsQuery(clubId);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ?
            Ok(_mapper.Map<IEnumerable<LocationResponse>>(result.Value))
            : NotFound();
    }

    private LocationResponse MapResult(Result<LocationResult> result)
    {
        return _mapper.Map<LocationResponse>(result.Value);
    }
}

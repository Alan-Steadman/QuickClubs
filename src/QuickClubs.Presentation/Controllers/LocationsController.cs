using Asp.Versioning;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Locations.CreateLocation;
using QuickClubs.Application.Locations.GetLocation;
using QuickClubs.Contracts.Locations;

namespace QuickClubs.Presentation.Controllers;

[Route("api/v{v:apiVersion}/clubs/{clubId:guid}/locations/")]
public sealed class LocationsController : ApiController
{
    private readonly IMapper _mapper;

    public LocationsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<ActionResult<Guid>> CreateLocation(
        Guid clubId,
        CreateLocationRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateLocationCommand>((clubId, request));

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ?
            CreatedAtAction(nameof(GetLocation), new { clubId = result.Value.ClubId, id = result.Value.Id }, result.Value)
            : BadRequest(result.Error);
    }

    //public async Task<ActionResult<LocationResponse>> GetLocation(Guid id)

    /// <summary>
    /// Retrieves a location matching the supplied id
    /// </summary>
    /// <param name="id">The id of the location to retrieve</param>
    /// <returns>A LocationResponse</returns>
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ActionName("GetLocation")]
    [MapToApiVersion(1)]
    public async Task<ActionResult> GetLocation(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetLocationQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ?
            Ok(_mapper.Map<LocationResponse>(result.Value))
            : NotFound();
    }
}

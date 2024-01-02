using Asp.Versioning;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Clubs.CreateClub;
using QuickClubs.Application.Clubs.GetAllClubs;
using QuickClubs.Application.Clubs.GetClub;
using QuickClubs.Application.Clubs.SetAffiliated;
using QuickClubs.Contracts.Clubs;

namespace QuickClubs.Presentation.Controllers;
public sealed class ClubsController : ApiController
{
    private readonly IMapper _mapper;

    public ClubsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<IActionResult> CreateClub(
        CreateClubRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateClubCommand>(request);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? 
            CreatedAtAction(nameof(GetClub), new { id = result.Value }, result.Value)
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Retrieves a club with the supplied id
    /// </summary>
    /// <param name="id">The id of the club to retrieve</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("{id}")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetClub(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClubQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ?
            Ok(_mapper.Map<ClubResponse>(result.Value))
            : NotFound();
    }

    [AllowAnonymous]
    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetAllClubs(CancellationToken cancellationToken)
    {
        var query = new GetAllClubsQuery();

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ?
            Ok(result.Value.Select(c => _mapper.Map<ClubResponse>(c)))
            : NotFound();
    }

    [HttpPut("{id}/set-affiliated")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> SetAffiliated(Guid id, SetAffiliatedRequest request, CancellationToken cancellationToken)
    {
        var command = new SetAffiliatedCommand(id, request.CurrencyCode, request.MembershipNeedsApproval);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}

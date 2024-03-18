using Asp.Versioning;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.MembershipOptions.Common;
using QuickClubs.Application.MembershipOptions.CreateMembershipOption;
using QuickClubs.Application.MembershipOptions.GetMembershipOption;
using QuickClubs.Contracts.MembershipOptions;
using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Presentation.Controllers;

[Route("api/v{v:apiVersion}/clubs/{clubId:guid}/membership-options/")]
public sealed class MembershipOptionsController : ApiController
{
    private readonly IMapper _mapper;

    public MembershipOptionsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new MembershipOption
    /// </summary>
    /// <param name="clubId">The club in which to create the new location</param>
    /// <param name="request">A CreateMembershipOptionRequest</param>
    /// <returns>The id of the newly created MembershipOption</returns>
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<ActionResult<MembershipOptionResponse>> CreateMembershipOption(Guid clubId, CreateMembershipOptionRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateMembershipOptionCommand>((clubId, request));

        var result = await Sender.Send(command);

        return result.IsSuccess ?
            CreatedAtAction(
                nameof(GetMembershipOption),
                new { clubId = result.Value.ClubId, id = result.Value.Id },
                MapResult(result.Value))
            : BadRequest(result.Error);
    }

    /// <summary>
    /// Retrieves a membership option matching the supplied id
    /// </summary>
    /// <param name="id">The id of the membership option to retrieve</param>
    /// <returns>A MembershipOptionResponse</returns>
    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [MapToApiVersion(1)]
    public async Task<ActionResult<MembershipOptionResponse>> GetMembershipOption(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMembershipOptionQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(MapResult(result.Value)) : base.NotFound(result.Error);
    }

    private MembershipOptionResponse MapResult(Result<MembershipOptionResult> result)
    {
        return _mapper.Map<MembershipOptionResponse>(result.Value);
    }
}

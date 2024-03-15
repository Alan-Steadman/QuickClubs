using Asp.Versioning;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.MembershipOptions.CreateMembershipOption;
using QuickClubs.Contracts.MembershipOptions;

namespace QuickClubs.Presentation.Controllers;

[Route("api/v{v:apiVersion}/clubs/{clubId:guid}/membership-options/")]
public sealed class MembershipOptionsController : ApiController
{
    /// <summary>
    /// Creates a new MembershipOption
    /// </summary>
    /// <param name="clubId">The club in which to create the new location</param>
    /// <param name="request">A CreateMembershipOptionRequest</param>
    /// <returns>The id of the newly created MembershipOption</returns>
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<IActionResult> CreateMembershipOption(Guid clubId, CreateMembershipOptionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMembershipOptionCommand(
            clubId,
            request.Name,
            request.Period,
            request.HasCutoff,
            request.CutoffMonth,
            request.CutoffDay,
            request.Levels.ConvertAll(l => new Application.MembershipOptions.CreateMembershipOption.CreateMembershipLevel(
                l.Name,
                l.Description,
                l.MaxMembers,
                l.MinAge,
                l.MaxAge,
                l.PriceAmount)));

        var result = await Sender.Send(command);

        // TODO: return created at
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}

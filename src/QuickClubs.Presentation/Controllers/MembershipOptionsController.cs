using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.MembershipOptions.CreateMembershipOption;
using QuickClubs.Contracts.MembershipOptions;

namespace QuickClubs.Presentation.Controllers;

[Route("api/v{v:apiVersion}/membership-options")]
public sealed class MembershipOptionsController : ApiController
{
    /// <summary>
    /// Creates a new MembershipOption
    /// </summary>
    /// <param name="request">A CreateMembershipOptionRequest</param>
    /// <returns>The id of the newly created MembershipOption</returns>
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<IActionResult> CreateMembershipOption(CreateMembershipOptionRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMembershipOptionCommand(
            request.ClubId,
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

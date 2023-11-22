using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Authentication.Register;
using QuickClubs.Application.Clubs.CreateClub;
using QuickClubs.Application.Clubs.SetAffiliated;
using QuickClubs.Contracts.Authentication;
using QuickClubs.Contracts.Clubs;

namespace QuickClubs.Presentation.Controllers;
public sealed class ClubsController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateClub(
        CreateClubRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateClubCommand(
            request.FullName,
            request.Acronym,
            request.Website);

        var result = await Sender.Send(command, cancellationToken);

        // TODO: return created at
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpPut("{id}/set-affiliated")]
    public async Task<IActionResult> SetAffiliated(Guid id, SetAffiliatedRequest request, CancellationToken cancellationToken)
    {
        var command = new SetAffiliatedCommand(id, request.CurrencyCode, request.MembershipNeedsApproval);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}

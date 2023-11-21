using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Authentication.Register;
using QuickClubs.Application.Clubs.CreateClub;
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

}

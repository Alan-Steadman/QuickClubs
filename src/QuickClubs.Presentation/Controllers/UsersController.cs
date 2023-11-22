using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Users.SetProfile;
using QuickClubs.Contracts.Users;

namespace QuickClubs.Presentation.Controllers;

public sealed class UsersController : ApiController
{
    [HttpPut("{id}/set-profile")]
    public async Task<IActionResult> SetProfile(Guid id, SetProfileRequest request, CancellationToken cancellationToken)
    {
        var command = new SetProfileCommand(
            id,
            request.DateOfBirth,
            request.Building,
            request.Street,
            request.Locality,
            request.Town,
            request.County,
            request.Postcode);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}

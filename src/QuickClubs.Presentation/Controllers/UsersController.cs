using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Users.SetProfile;
using QuickClubs.Contracts.Users;

namespace QuickClubs.Presentation.Controllers;

public sealed class UsersController : ApiController
{
    /// <summary>
    /// Sets a user's profile
    /// </summary>
    /// <param name="id">The user id</param>
    /// <param name="request">A SetProfileRequest</param>
    /// <returns>No content</returns>
    [HttpPut("{id:guid}/set-profile")]
    [MapToApiVersion(1)]
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

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }
}

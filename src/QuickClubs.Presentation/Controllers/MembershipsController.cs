using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Memberships.CreateMembership;
using QuickClubs.Contracts.Memberships;
using QuickClubs.Domain.MembershipOptions.ValueObjects;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Presentation.Controllers;

public class MembershipsController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateMembership(CreateMembershipRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMembershipCommand(
            new UserId(request.UserId),
            request.AdditionalMembers.ConvertAll(u => new UserId(u)),
            new MembershipOptionId(request.MembershipOptionId),
            new MembershipLevelId(request.MembershipLevelId));

        var result = await Sender.Send(command);

        // TODO: Return 201 CreatedAt:
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}

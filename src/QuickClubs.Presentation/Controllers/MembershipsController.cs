using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Memberships.ApproveMembership;
using QuickClubs.Application.Memberships.CreateMembership;
using QuickClubs.Application.Memberships.GetAllClubMembers;
using QuickClubs.Contracts.Memberships;
using System.Security.Claims;

namespace QuickClubs.Presentation.Controllers;

public class MembershipsController : ApiController
{
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<IActionResult> CreateMembership(CreateMembershipRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMembershipCommand(
            request.UserId,
            request.AdditionalMembers,
            request.MembershipOptionId,
            request.MembershipLevelId);

        var result = await Sender.Send(command);

        // TODO: Return 201 CreatedAt:
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetAllClubMembers(GetAllClubMembersRequest request, CancellationToken cancellationToken)
    {
        var query = new GetAllClubMembersQuery(request.ClubId, request.MemberAtDate);

        Console.WriteLine("User Id: " + User.FindFirst(ClaimTypes.NameIdentifier));

        Console.WriteLine("Id: " + User.FindFirst("sub"));
        Console.WriteLine("Email: " + User.FindFirst("email"));
        Console.WriteLine("Jti: " + User.FindFirst("jti"));

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPost("{id}/approve")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> ApproveMembership(Guid Id, ApproveMembershipRequest request, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return Unauthorized();

        var userIdString = userIdClaim.Value;
        if (userIdString is null)
            return Unauthorized();

        if (!Guid.TryParse(userIdString, out Guid userId))
            return Unauthorized();

        var command = new ApproveMembershipCommand(
            Id,
            userId,
            request.Reason);

        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }

}

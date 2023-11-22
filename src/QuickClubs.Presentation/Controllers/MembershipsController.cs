using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Clubs.GetAllClubs;
using QuickClubs.Application.Memberships.CreateMembership;
using QuickClubs.Application.Memberships.GetAllClubMembers;
using QuickClubs.Contracts.Memberships;
using QuickClubs.Domain.MembershipOptions.ValueObjects;
using QuickClubs.Domain.Users.ValueObjects;
using System.Security.Claims;

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

    [HttpGet]
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

}

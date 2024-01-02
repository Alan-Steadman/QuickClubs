using Asp.Versioning;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Memberships.ApproveMembership;
using QuickClubs.Application.Memberships.CreateMembership;
using QuickClubs.Application.Memberships.GetAllClubMembers;
using QuickClubs.Contracts.Memberships;
using System.Security.Claims;

namespace QuickClubs.Presentation.Controllers;

public class MembershipsController : ApiController
{
    private readonly IMapper _mapper;

    public MembershipsController(IMapper mapper)
    {
        _mapper = mapper;
    }
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<IActionResult> CreateMembership(CreateMembershipRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateMembershipCommand>(request);

        var result = await Sender.Send(command);

        // TODO: Return 201 CreatedAt:
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetAllClubMembers(GetAllClubMembersRequest request, CancellationToken cancellationToken)
    {
        var query = new GetAllClubMembersQuery(request.ClubId, request.MemberAtDate);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value.Select(m => _mapper.Map<MembershipResponse>(m))) : NotFound();
    }

    [HttpPost("{id}/approve")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> ApproveMembership(Guid Id, ApproveMembershipRequest request, CancellationToken cancellationToken)
    {

        //Console.WriteLine("User Id: " + User.FindFirst(ClaimTypes.NameIdentifier));

        //Console.WriteLine("Id: " + User.FindFirst("sub"));
        //Console.WriteLine("Email: " + User.FindFirst("email"));
        //Console.WriteLine("Jti: " + User.FindFirst("jti"));

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

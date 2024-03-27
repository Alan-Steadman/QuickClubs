using Asp.Versioning;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Memberships.ApproveMembership;
using QuickClubs.Application.Memberships.Common;
using QuickClubs.Application.Memberships.CreateMembership;
using QuickClubs.Application.Memberships.GetAllClubMembers;
using QuickClubs.Application.Memberships.GetMembership;
using QuickClubs.Application.Memberships.RejectMembership;
using QuickClubs.Contracts.Memberships;
using QuickClubs.Domain.Abstractions;
using System.Security.Claims;

namespace QuickClubs.Presentation.Controllers;

public class MembershipsController : ApiController
{
    private readonly IMapper _mapper;

    public MembershipsController(IMapper mapper)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Create a new membership for a user at a club
    /// </summary>
    /// <param name="request">A CreateMembershipRequest</param>
    /// <returns>A MembershipResponse object of the newly created membership</returns>
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<ActionResult<MembershipResponse>> CreateMembership(CreateMembershipRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateMembershipCommand>(request);

        var result = await Sender.Send(command);

        return result.IsSuccess ?
            CreatedAtAction(
                nameof(GetMembership),
                new { clubId = result.Value.ClubId, id = result.Value.Id },
                MapResult(result)) 
            : BadRequest(result.Error);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id">The membership id</param>
    /// <returns>A MembershipResponse</returns>
    [HttpGet("{id:guid}")]
    [MapToApiVersion(1)]
    public async Task<ActionResult<MembershipResponse>> GetMembership(Guid Id, CancellationToken cancellationToken)
    {
        var query = new GetMembershipQuery(Id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? base.Ok(MapResult(result)) : base.NotFound(result.Error);
    }

    /// <summary>
    /// Returns a list of all members of a club, at a given point in time.
    /// </summary>
    /// <param name="request">A GetAllClubMembersRequest</param>
    /// <returns>An IEnumerable of type MembershipResponse</returns>
    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<ActionResult<IEnumerable<MemberResponse>>> GetAllClubMembers(GetAllClubMembersRequest request, CancellationToken cancellationToken)
    {
        var query = new GetAllClubMembersQuery(request.ClubId, request.MemberAtDate);
                
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value.Select(m => _mapper.Map<MemberResponse>(m))) : NotFound(result.Error);
    }

    /// <summary>
    /// Approve a user's club membership application
    /// </summary>
    /// <param name="Id">The membership id</param>
    /// <param name="request">An ApproveMembershipRequest</param>
    /// <returns>No content</returns>
    [HttpPost("{id:guid}/approve")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> ApproveMembership(Guid Id, ApproveMembershipRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromUser();
        if (userId is null)
            return Unauthorized();

        var command = new ApproveMembershipCommand(
            Id,
            (Guid)userId,
            request.Reason);

        var result = await Sender.Send(command);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    /// <summary>
    /// Approve a user's club membership application
    /// </summary>
    /// <param name="Id">The membership id</param>
    /// <param name="request">A RejectMembershipRequest</param>
    /// <returns>No content</returns>
    [HttpPost("{id:guid}/reject")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> RejectMembership(Guid Id, RejectMembershipRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromUser();
        if (userId is null)
            return Unauthorized();

        var command = new RejectMembershipCommand(
            Id,
            (Guid)userId,
            request.Reason);

        var result = await Sender.Send(command);

        return result.IsSuccess ? NoContent() : BadRequest(result.Error);
    }

    /// <returns>A valid Guid UserId or null if the claim is invalid</returns>
    private Guid? GetUserIdFromUser()
    {
        //Console.WriteLine("User Id: " + User.FindFirst(ClaimTypes.NameIdentifier));
        //Console.WriteLine("Id: " + User.FindFirst("sub"));
        //Console.WriteLine("Email: " + User.FindFirst("email"));
        //Console.WriteLine("Jti: " + User.FindFirst("jti"));

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null)
            return null;

        var userIdString = userIdClaim.Value;
        if (userIdString is null)
            return null;

        if (!Guid.TryParse(userIdString, out Guid userId))
            return null;

        return userId;
    }

    private MembershipResponse MapResult(Result<MembershipResult> result)
    {
        return _mapper.Map<MembershipResponse>(result.Value);
    }
}

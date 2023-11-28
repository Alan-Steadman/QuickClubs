﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Clubs.CreateClub;
using QuickClubs.Application.Clubs.GetAllClubs;
using QuickClubs.Application.Clubs.GetClub;
using QuickClubs.Application.Clubs.SetAffiliated;
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

        return result.IsSuccess ? 
            CreatedAtAction(nameof(GetClub), new { id = result.Value }, result.Value)
            : BadRequest(result.Error);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClub(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetClubQuery(id);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllClubs(CancellationToken cancellationToken)
    {
        var query = new GetAllClubsQuery();

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }

    [HttpPut("{id}/set-affiliated")]
    public async Task<IActionResult> SetAffiliated(Guid id, SetAffiliatedRequest request, CancellationToken cancellationToken)
    {
        var command = new SetAffiliatedCommand(id, request.CurrencyCode, request.MembershipNeedsApproval);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : BadRequest(result.Error);
    }
}

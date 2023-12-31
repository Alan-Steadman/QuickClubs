using Asp.Versioning;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickClubs.Application.Authentication.Login;
using QuickClubs.Application.Authentication.Register;
using QuickClubs.Contracts.Authentication;

namespace QuickClubs.Presentation.Controllers;

public sealed class AuthController : ApiController
{
    private readonly IMapper _mapper;

    public AuthController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> Register(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> Login(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var command = new LoginCommand(
            request.Email,
            request.Password);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(_mapper.Map<AuthenticationResponse>(result.Value)) : BadRequest(result.Error);
    }

}

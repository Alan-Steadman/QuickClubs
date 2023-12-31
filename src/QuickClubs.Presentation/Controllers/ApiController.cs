using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace QuickClubs.Presentation.Controllers;

[Authorize]
[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender _sender = null!;

    /// <summary>
    /// Gets the sender.
    /// </summary>
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>()!;
}

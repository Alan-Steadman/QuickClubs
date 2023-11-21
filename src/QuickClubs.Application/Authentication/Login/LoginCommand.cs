using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Authentication.Common;

namespace QuickClubs.Application.Authentication.Login;

public sealed record LoginCommand(
    string Email,
    string Password) : ICommand<AuthenticationResult>;
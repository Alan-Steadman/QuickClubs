using QuickClubs.Application.Abstractions.Mediator;
using QuickClubs.Application.Authentication.Common;

namespace QuickClubs.Application.Authentication.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<AuthenticationResult>;
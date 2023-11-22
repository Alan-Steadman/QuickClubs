using QuickClubs.Application.Abstractions.Mediator;

namespace QuickClubs.Application.Users.SetProfile;
public sealed record SetProfileCommand(
    Guid UserId,
    DateOnly DateOfBirth,
    string Building,
    string Street,
    string Locality,
    string Town,
    string County,
    string Postcode)
    : ICommand;
namespace QuickClubs.Contracts.Users;

public sealed record SetProfileRequest(
    DateOnly DateOfBirth,
    string Building,
    string Street,
    string Locality,
    string Town,
    string County,
    string Postcode);
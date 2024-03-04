using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.Locations.Errors;
public static class LocationErrors
{
    public static Error NotFound(Guid locationId) => new("Location.NotFound", $"No location was found with the id '{locationId}'");

    public static Error DuplicateName(string name) => new("Location.DuplicateFullName", $"A location already exists with the name '{name}' at this club");
}

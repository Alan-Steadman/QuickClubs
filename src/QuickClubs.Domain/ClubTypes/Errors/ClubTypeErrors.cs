using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.ClubTypes.Errors;
public static class ClubTypeErrors
{
    public static Error NotFound(string code) => new("ClubType.NotFound", $"No club type was found with the code '{code}'");
}

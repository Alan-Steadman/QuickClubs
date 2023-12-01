using QuickClubs.Domain.Abstractions;

namespace QuickClubs.Domain.Clubs.Errors;
public static class ClubErrors
{
    public static Error NotFound(Guid clubId) => new("Club.NotFound", $"No club was found with the id '{clubId}'");

    public static Error DuplicateFullName(string fullName) => new("Club.DuplicateFullName", $"A club already exists with the full name '{fullName}'");
    public static Error DuplicateAcronym => new("Club.DuplicateAcronym", "A club already exists with that acronym");
    public static Error DuplicateWebsite => new("Club.DuplicateWebsite", "A club already exists with that website");

    public static Error AlreadyAffiliated => new("Club.AlreadyAffiliated", "This club is already affiliated");
    public static Error NotAffiliated => new("Club.NotAffiliated", "This club is not affiliated");

    public static Error CurrencyNotSet => new("Club.CurrencyNotSet", "This club does not have a currency set up");
}

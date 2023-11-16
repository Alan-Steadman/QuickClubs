namespace QuickClubs.Domain.Common;

public record Address(
    string Building,
    string Street,
    string Locality,
    string Town,
    string County,
    string Postcode)
{
    public const int BuildingMaxLength = 50;
    public const int StreetMaxLength = 50;
    public const int LocalityMaxLength = 50;
    public const int TownMaxLength = 50;
    public const int CountyMaxLength = 20;
    public const int PostcodeMaxLength = 8;
}
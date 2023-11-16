using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Entities;
using QuickClubs.Domain.Clubs.Errors;
using QuickClubs.Domain.Clubs.Events;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;

namespace QuickClubs.Domain.Clubs;
public sealed class Club : AggregateRoot<ClubId>
{
    private Club(
        ClubId id,
        ClubName name,
        ClubWebsite website,
        bool isAffiliate,
        ClubSettings? settings
        ) : base(id)
    {
        Name = name;
        Website = website;
        IsAffiliate = isAffiliate;
        Settings = settings;
    }

    public ClubName Name { get; private set; }
    public ClubWebsite Website { get; private set; }
    public bool IsAffiliate { get; private set; }
    public ClubSettings? Settings { get; private set; } // For safe access to non-null Settings, use GetSettings()

    public ClubSettings GetSettings()
    {
        if (!IsAffiliate)
        {
            throw new ApplicationException("Attempt to get club settings when IsAffiliate is false");
        }
        if (Settings is null)
        {
            throw new ApplicationException("Club Settings is null when IsAffiliate is true");
        }

        return Settings;
    }

    public static Club Create(
        ClubName name,
        ClubWebsite website)
    {
        var club = new Club(
            id: ClubId.New(),
            name: name,
            website: website,
            isAffiliate: false,
            settings: null);

        club.RaiseDomainEvent(new ClubCreatedDomainEvent(club.Id));

        return club;
    }

    /// <summary>
    /// Set a club to IsAfilliated = true and sets all necessary required club settings
    /// </summary>
    public Result SetAffiliated(
        Currency currency,
        bool membershipNeedsApproval)
    {
        if (IsAffiliate == true)
        {
            return Result.Failure(ClubErrors.AlreadyAffiliated);
        }

        var settings = ClubSettings.Create(
            currency: currency,
            membershipNeedsApproval: membershipNeedsApproval);

        IsAffiliate = true;
        Settings = settings;

        return Result.Success();
    }

#pragma warning disable CS8618
    private Club()
    {
    }
#pragma warning restore CS8618
}

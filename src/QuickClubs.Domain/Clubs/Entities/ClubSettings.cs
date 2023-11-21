using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;

namespace QuickClubs.Domain.Clubs.Entities;

public class ClubSettings : Entity<ClubSettingsId>
{
    public Currency Currency { get; private set; }

    public bool MembershipNeedsApproval { get; private set; }

    private ClubSettings(
        ClubSettingsId id,
        Currency currency,
        bool membershipNeedsApproval)
        : base(id)
    {
        Currency = currency;
        MembershipNeedsApproval = membershipNeedsApproval;
    }

    public static ClubSettings Create(
        Currency currency,
        bool membershipNeedsApproval)
    {
        return new ClubSettings(
            ClubSettingsId.New(),
            currency,
            membershipNeedsApproval);
    }

#pragma warning disable CS8618
    private ClubSettings()
    {
    }
#pragma warning restore CS8618
}

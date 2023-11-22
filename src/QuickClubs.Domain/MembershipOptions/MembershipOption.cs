using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.MembershipOptions.Entities;
using QuickClubs.Domain.MembershipOptions.ValueObjects;

namespace QuickClubs.Domain.MembershipOptions;

public sealed class MembershipOption : AggregateRoot<MembershipOptionId>
{
    private readonly List<MembershipLevel> _levels = new();

    public ClubId ClubId { get; private set; } = null!;
    public MembershipOptionName Name { get; private set; } = null!;
    public MembershipPeriod Period { get; private set; } = null!;
    public Cutoff? Cutoff { get; private set; }
    public IReadOnlyList<MembershipLevel> Levels => _levels.AsReadOnly();

    private MembershipOption(
        MembershipOptionId membershipOptionId,
        ClubId clubId,
        MembershipOptionName name,
        MembershipPeriod period,
        Cutoff? cutoff,
        List<MembershipLevel> levels)
        : base(membershipOptionId)
    {
        ClubId = clubId;
        Name = name;
        Period = period;
        Cutoff = cutoff;
        _levels = levels;
    }

    public static MembershipOption Create(
        ClubId ClubId,
        MembershipOptionName Name,
        MembershipPeriod Period,
        Cutoff? Cutoff,
        List<MembershipLevel> Levels)
    {
        return new MembershipOption(
            MembershipOptionId.New(),
            ClubId,
            Name,
            Period,
            Cutoff,
            Levels);
    }

#pragma warning disable CS8618
    private MembershipOption()
    {
    }
#pragma warning restore CS8618
}

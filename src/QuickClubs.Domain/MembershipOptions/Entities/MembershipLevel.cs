using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.MembershipOptions.ValueObjects;

namespace QuickClubs.Domain.MembershipOptions.Entities;

public sealed class MembershipLevel : Entity<MembershipLevelId>
{
    public MembershipLevelName Name { get; private set; } = null!;
    public MembershipLevelDescription Description { get; private set; } = null!;
    public int MaxMembers { get; private set; }
    public int? MinAge { get; private set; }
    public int? MaxAge { get; private set; }
    public Money Price { get; private set; } = null!;

    private MembershipLevel(
        MembershipLevelId membershipLevelId,
        MembershipLevelName name,
        MembershipLevelDescription description,
        int maxMembers,
        int? minAge,
        int? maxAge,
        Money price)
        : base(membershipLevelId)
    {
        Name = name;
        Description = description;
        MaxMembers = maxMembers;
        MinAge = minAge;
        MaxAge = maxAge;
        Price = price;
    }

    public static MembershipLevel Create(
        MembershipLevelName Name,
        MembershipLevelDescription Description,
        int MaxMembers,
        int? MinAge,
        int? MaxAge,
        Money Price)
    {
        return new MembershipLevel(
            MembershipLevelId.New(),
            Name,
            Description,
            MaxMembers,
            MinAge,
            MaxAge,
            Price);
    }

#pragma warning disable CS8618
    private MembershipLevel()
    {
    }
#pragma warning restore CS8618
}

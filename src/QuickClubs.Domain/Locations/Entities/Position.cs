using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Domain.Locations.Entities;
public sealed class Position : Entity<PositionId>
{
    public Address? Address { get; private set; }
    public WhatThreeWords? WhatThreeWords { get; private set; }
    public OsGridRef? OsGridRef { get; private set; }

    private Position(
        PositionId id,
        Address? address,
        WhatThreeWords? whatThreeWords,
        OsGridRef? osGridRef)
        : base(id)
    {
        Address = address;
        WhatThreeWords = whatThreeWords;
        OsGridRef = osGridRef;
    }

    public static Position Create(
        Address? address,
        WhatThreeWords? whatThreeWords,
        OsGridRef? osGridRef)
    {
        return new Position(
            PositionId.New(),
            address,
            whatThreeWords,
            osGridRef);
    }

#pragma warning disable CS8618
    public Position()
    {
    }
#pragma warning restore CS8618
}


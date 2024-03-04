using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Domain.Locations;
public sealed class Location : AggregateRoot<LocationId>
{
    public ClubId ClubId { get; private set; }
    public LocationName Name { get; private set; }
    public Position Position { get; private set; }
    public Directions Directions { get; private set; }

    private Location(
        LocationId id,
        ClubId clubId,
        LocationName name,
        Position position,
        Directions directions) : base(id)
    {
        ClubId = clubId;
        Name = name;
        Position = position;
        Directions = directions;
    }

    public static Location Create(
        ClubId clubId,
        LocationName name,
        Position position,
        Directions directions)
    {
        var location = new Location(
            LocationId.New(),
            clubId,
            name,
            position,
            directions);

        return location;
    }

    public Result UpdatePosition(Position position)
    {
        Position = position;
        return Result.Success();
    }

    public Result UpdateName(LocationName name)
    {
        Name = name;
        return Result.Success();
    }

    public Result UpdateDirections(Directions directions)
    {
        Directions = directions;
        return Result.Success();
    }

}

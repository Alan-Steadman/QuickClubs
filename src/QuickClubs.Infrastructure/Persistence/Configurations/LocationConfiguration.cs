using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Locations;
using QuickClubs.Domain.Locations.ValueObjects;

namespace QuickClubs.Infrastructure.Persistence.Configurations;

internal sealed class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Location");

        builder.HasKey(location => location.Id);

        builder.Property(location => location.Id)
            .HasColumnName("Id")
            .HasConversion(locationId => locationId.Value, value => new LocationId(value));

        builder.Property(location => location.ClubId)
            .HasConversion(clubId => clubId.Value, value => new ClubId(value));

        builder.Property(location => location.Name)
            .HasConversion(name => name.Value, value => new LocationName(value))
            .HasMaxLength(LocationName.MaxLength);

        builder.OwnsOne(location => location.Position, positionBuilder =>
        {
            positionBuilder.Property(position => position.Id)
                .HasColumnName("PositionId")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => new PositionId(value));

            positionBuilder.OwnsOne(position => position.Address, addressBuilder =>
            {
                addressBuilder.Property(address => address.Building)
                    .HasColumnName("Building")
                    .HasMaxLength(Address.BuildingMaxLength);
                addressBuilder.Property(address => address.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(Address.StreetMaxLength);
                addressBuilder.Property(address => address.Locality)
                    .HasColumnName("Locality")
                    .HasMaxLength(Address.LocalityMaxLength);
                addressBuilder.Property(address => address.Town)
                    .HasColumnName("Town")
                    .HasMaxLength(Address.TownMaxLength);
                addressBuilder.Property(address => address.County)
                    .HasColumnName("County")
                    .HasMaxLength(Address.CountyMaxLength);
                addressBuilder.Property(address => address.Postcode)
                    .HasColumnName("Postcode")
                    .HasMaxLength(Address.PostcodeMaxLength);
            });

            positionBuilder.Property(position => position.WhatThreeWords)
                .HasColumnName("WhatThreeWords")
                .HasConversion(whatThreeWords => whatThreeWords == null ? null : whatThreeWords.Value, value => value == null ? null : new WhatThreeWords(value))
                .HasMaxLength(WhatThreeWords.MaxLength);

            positionBuilder.Property(position => position.OsGridRef)
                .HasColumnName("OsGridRef")
                .HasConversion(gridRef => gridRef == null ? null : gridRef.Value, value => value == null ? null : new OsGridRef(value))
                .HasMaxLength(OsGridRef.MaxLength);
        });

        builder.Property(location => location.Directions)
            .HasColumnName("Directions")
            .HasConversion(directions => directions == null ? null : directions.Value, value => value == null ? null : new Directions(value))
            .HasMaxLength(Directions.MaxLength);

        builder.Property<byte[]>("Version").IsRowVersion();

        builder.HasIndex(location => new { location.ClubId, location.Id })
            .IsUnique(true);

        builder.HasIndex(location => new { location.ClubId, location.Name })
            .IsUnique(true);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.ClubTypes;
using QuickClubs.Domain.Common;

namespace QuickClubs.Infrastructure.Persistence.Configurations;

internal sealed class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.ToTable("Club");

        builder.HasKey(club => club.Id);

        builder.Property(club => club.Id)
            .HasConversion(clubId => clubId.Value, value => new ClubId(value));

        builder.Property(club => club.ClubType)
            .HasConversion(clubType => clubType.Code, value => ClubType.FromCode(value))
            .HasMaxLength(ClubType.CodeMaxLength);

        builder.OwnsOne(club => club.Name, nameBuilder =>
        {
            nameBuilder.Property(name => name.FullName)
                .HasColumnName("FullName")
                .HasMaxLength(ClubName.FullNameMaxLength);

            nameBuilder.Property(name => name.Acronym)
                .HasColumnName("Acronym")
                .HasMaxLength(ClubName.AcronymMaxLength);

            nameBuilder.HasIndex(name => name.FullName).IsUnique();
            nameBuilder.HasIndex(name => name.Acronym).IsUnique();
        });

        builder.OwnsOne(club => club.Website, websiteBuilder =>
        {
            websiteBuilder.Property(website => website.Url)
                .HasColumnName("Website")
                .HasMaxLength(ClubWebsite.MaxLength);

            websiteBuilder.HasIndex(website => website.Url).IsUnique();
        });

        builder.OwnsOne(club => club.Settings, settingsBuilder =>
        {
            settingsBuilder.ToTable("ClubSettings");

            settingsBuilder.Property(s => s.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => new ClubSettingsId(value));

            settingsBuilder.WithOwner().HasForeignKey("ClubId");

            settingsBuilder.HasKey("Id", "ClubId");

            settingsBuilder.Property(s => s.Currency)
                .HasConversion(c => c.Code, value => Currency.FromCode(value))
                .HasMaxLength(Currency.CodeMaxLength);
        });

        builder.Property<byte[]>("Version").IsRowVersion();
    }
}

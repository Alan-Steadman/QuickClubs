using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickClubs.Domain.Clubs;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;

namespace QuickClubs.Infrastructure.Persistence.Configurations;

internal sealed class ClubConfiguration : IEntityTypeConfiguration<Club>
{
    public void Configure(EntityTypeBuilder<Club> builder)
    {
        builder.ToTable("club");

        builder.HasKey(club => club.Id);

        builder.Property(club => club.Id)
            .HasConversion(clubId => clubId.Value, value => new ClubId(value));

        builder.OwnsOne(club => club.Name, nameBuilder =>
        {
            nameBuilder.Property(name => name.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(ClubName.FullNameMaxLength);

            nameBuilder.Property(name => name.Acronym)
                .HasColumnName("acronym")
                .HasMaxLength(ClubName.AcronymMaxLength);

            nameBuilder.HasIndex(name => name.FullName).IsUnique();
            nameBuilder.HasIndex(name => name.Acronym).IsUnique();
        });

        builder.OwnsOne(club => club.Website, websiteBuilder =>
        {
            websiteBuilder.Property(website => website.Url)
                .HasColumnName("website")
                .HasMaxLength(ClubWebsite.MaxLength);

            websiteBuilder.HasIndex(website => website.Url).IsUnique();
        });

        builder.Property<uint>("Version").IsRowVersion();

        builder.OwnsOne(club => club.Settings, settingsBuilder =>
        {
            settingsBuilder.ToTable("club_settings");

            settingsBuilder.Property(s => s.Id)
                .HasColumnName("id")
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => new ClubSettingsId(value));

            settingsBuilder.WithOwner().HasForeignKey("club_id");

            settingsBuilder.HasKey("Id", "club_id");

            settingsBuilder.Property(s => s.Currency)
                .HasConversion(c => c.Code, value => Currency.FromCode(value))
                .HasMaxLength(Currency.CodeMaxLength);
        });
    }
}

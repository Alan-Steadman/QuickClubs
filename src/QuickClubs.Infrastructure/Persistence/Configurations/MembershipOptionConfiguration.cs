using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.MembershipOptions;
using QuickClubs.Domain.MembershipOptions.ValueObjects;

namespace QuickClubs.Infrastructure.Persistence.Configurations;
internal sealed class MembershipOptionConfiguration : IEntityTypeConfiguration<MembershipOption>
{
    public void Configure(EntityTypeBuilder<MembershipOption> builder)
    {
        builder.ToTable("MembershipOption");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(id => id.Value, value => new MembershipOptionId(value));

        builder.Property(o => o.ClubId)
            .HasConversion(id => id.Value, value => new ClubId(value));

        builder.Property(o => o.Name)
            .HasConversion(name => name.Value, value => new MembershipOptionName(value))
            .IsRequired()
            .HasMaxLength(MembershipOptionName.MaxLength);

        builder.Property(o => o.Period)
            .HasConversion(p => p.ToString(), value => MembershipPeriod.FromString(value))
            .IsRequired()
            .HasMaxLength(MembershipOptionName.MaxLength);

        builder.OwnsOne(o => o.Cutoff, cutoffBuilder =>
        {
            cutoffBuilder.Property(c => c.Day).HasColumnName("CutoffDay");
            cutoffBuilder.Property(c => c.Month).HasColumnName("CutoffMonth");
        });

        builder.OwnsMany(o => o.Levels, levelsBuilder =>
        {
            levelsBuilder.ToTable("MembershipLevels");

            levelsBuilder.Property(l => l.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new MembershipLevelId(value));

            //levelsBuilder.WithOwner().HasForeignKey("membership_option_id");

            levelsBuilder.HasKey("Id", "MembershipOptionId"); // tell it the names of the two fields that make up our composite primary key.  There is a way to do this by setting HasKey(s => new[] {s.id, xxx}), but the xxx isn’t available as it is a shadow property of another table, so instead we’ll use an overload that accepts strings of the database field names.

            levelsBuilder.Property(l => l.Name)
                .HasConversion(n => n.Value, value => new MembershipLevelName(value))
                .IsRequired()
                .HasMaxLength(MembershipLevelName.MaxLength);

            levelsBuilder.Property(l => l.Description)
                .HasConversion(d => d.Value, value => new MembershipLevelDescription(value))
                .IsRequired()
                .HasMaxLength(MembershipLevelDescription.MaxLength);

            levelsBuilder.OwnsOne(l => l.Price, priceBuilder =>
            {
                priceBuilder.Property(p => p.Currency)
                    .HasColumnName("PriceCurrency")
                    .HasConversion(c => c.Code, value => Currency.FromCode(value))
                    .HasMaxLength(Currency.CodeMaxLength);

                priceBuilder.Property(p => p.Amount)
                    .HasColumnName("PriceAmount")
                    .HasColumnType(Money.AmountColumnType);
            });

        });

        /* The MembershipOption.Levels public property is a ReadOnlyList<T>,
        meaning that EF will only have read-only access to them by default.
        We need to tell EF to populate the private List<T> backing field 
        and not the public IReadonlyList. */
        builder.Metadata.FindNavigation(nameof(MembershipOption.Levels))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

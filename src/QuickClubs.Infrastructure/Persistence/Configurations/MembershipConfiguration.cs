using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickClubs.Domain.Clubs.ValueObjects;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.MembershipOptions.ValueObjects;
using QuickClubs.Domain.Memberships;
using QuickClubs.Domain.Memberships.ValueObjects;
using QuickClubs.Domain.Users.ValueObjects;

namespace QuickClubs.Infrastructure.Persistence.Configurations;

internal sealed class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.ToTable("Membership");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(id => id.Value, value => new MembershipId(value));

        builder.Property(m => m.ClubId)
            .HasConversion(id => id.Value, value => new ClubId(value));

        builder.OwnsMany(m => m.Members, membersBuilder =>
        {
            membersBuilder.ToTable("Member");

            membersBuilder.HasKey("Id");

            membersBuilder.Property(m => m.Value)
                .HasColumnName("UserId");
        });

        builder.Metadata.FindNavigation(nameof(Membership.Members))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(m => m.MembershipOptionId)
            .HasConversion(id => id.Value, value => new MembershipOptionId(value));

        builder.Property(m => m.MembershipLevelId)
            .HasConversion(id => id.Value, value => new MembershipLevelId(value));

        builder.Property(m => m.MembershipNumber)
            .HasConversion(mn => mn.Value, value => new MembershipNumber(value))
            .HasMaxLength(MembershipNumber.MaxLength);

        builder.Property(m => m.MembershipName)
            .HasConversion(mn => mn.Value, value => new MembershipName(value))
            .HasMaxLength(MembershipName.MaxLength);

        builder.OwnsOne(m => m.Price, priceBuilder =>
        {
            priceBuilder.Property(p => p.Amount)
                .HasColumnType(Money.AmountColumnType);

            priceBuilder.Property(p => p.Currency)
                .HasConversion(c => c.Code, value => Currency.FromCode(value));
        });

        builder.OwnsOne(m => m.Approval, approvalBuilder =>
        {
            approvalBuilder.Property(a => a.ApprovalStatus)
                .HasConversion(a => a.ToString(), value => ApprovalStatus.FromString(value))
                .HasMaxLength(ApprovalStatus.MaxLength);

            approvalBuilder.Property(a => a.ApprovedBy)
                .HasConversion(
                    uid => uid.Value,
                    value => new UserId(value));

            approvalBuilder.Property(a => a.Reason)
                .HasMaxLength(Approval.ReasonMaxLength);
        });
    }
}

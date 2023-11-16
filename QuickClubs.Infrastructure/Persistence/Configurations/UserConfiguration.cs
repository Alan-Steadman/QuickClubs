using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuickClubs.Domain.Users.Entities;
using QuickClubs.Domain.Users.ValueObjects;
using QuickClubs.Domain.Users;

namespace QuickClubs.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .ValueGeneratedNever()
            .HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(user => user.FirstName)
            .HasMaxLength(FirstName.MaxLength)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));

        builder.Property(user => user.LastName)
            .HasMaxLength(LastName.MaxLength)
            .HasConversion(lastName => lastName.Value, value => new LastName(value));

        builder.Property(user => user.Email)
            .HasMaxLength(UserEmail.MaxLength)
            .HasConversion(email => email.Value, value => new UserEmail(value));

        builder.HasOne(user => user.Profile)
            .WithOne(up => up.User)
            .HasForeignKey<UserProfile>(up => up.UserId)
            .IsRequired(false);
        //builder.OwnsOne(user => user.Profile, profileBuilder =>
        //{
        //    profileBuilder.ToTable("user_profile");

        //    profileBuilder.Property(p => p.Id)
        //        .HasColumnName("id")
        //        .ValueGeneratedNever()
        //        .HasConversion(id => id.Value, value => new UserProfileId(value));

        //    profileBuilder.WithOwner().HasForeignKey();

        //    profileBuilder.HasKey("Id", "user_id");

        //    profileBuilder.OwnsOne(p => p.Address, addressBuilder =>
        //    {
        //        addressBuilder.Property(a => a.Building)
        //            .HasMaxLength(Address.BuildingMaxLength);
        //        addressBuilder.Property(a => a.Street)
        //            .HasMaxLength(Address.StreetMaxLength);
        //        addressBuilder.Property(a => a.Locality)
        //            .HasMaxLength(Address.LocalityMaxLength);
        //        addressBuilder.Property(a => a.Town)
        //            .HasMaxLength(Address.TownMaxLength);
        //        addressBuilder.Property(a => a.County)
        //            .HasMaxLength(Address.CountyMaxLength);
        //        addressBuilder.Property(a => a.Postcode)
        //            .HasMaxLength(Address.PostcodeMaxLength);
        //    });
        //});

        builder.Property<uint>("Version").IsRowVersion();

        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.IdentityId).IsUnique();
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuickClubs.Domain.Common;
using QuickClubs.Domain.Users.ValueObjects;
using QuickClubs.Domain.Users;

namespace QuickClubs.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

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

        builder.Property(user => user.PasswordHash)
            .HasMaxLength(PasswordHash.MaxLength)
            .HasConversion(passwordHash => passwordHash.Value, value => new PasswordHash(value));

        builder.OwnsOne(user => user.Profile, profileBuilder => {
            profileBuilder.ToTable("UserProfile");

            profileBuilder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(id => id.Value, value => new UserProfileId(value));

                profileBuilder.HasKey("Id", "UserId");

            profileBuilder.OwnsOne(p => p.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Building)
                    .HasMaxLength(Address.BuildingMaxLength);
                addressBuilder.Property(a => a.Street)
                    .HasMaxLength(Address.StreetMaxLength);
                addressBuilder.Property(a => a.Locality)
                    .HasMaxLength(Address.LocalityMaxLength);
                addressBuilder.Property(a => a.Town)
                    .HasMaxLength(Address.TownMaxLength);
                addressBuilder.Property(a => a.County)
                    .HasMaxLength(Address.CountyMaxLength);
                addressBuilder.Property(a => a.Postcode)
                    .HasMaxLength(Address.PostcodeMaxLength);
            });
        });

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property<byte[]>("Version").IsRowVersion();
    }
}

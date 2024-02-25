﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuickClubs.Infrastructure.Persistence;

#nullable disable

namespace QuickClubs.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240225125919_Added-Club.ClubType")]
    partial class AddedClubClubType
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QuickClubs.Domain.Clubs.Club", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClubType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAffiliate")
                        .HasColumnType("bit");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Club", (string)null);
                });

            modelBuilder.Entity("QuickClubs.Domain.MembershipOptions.MembershipOption", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClubId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("MembershipOption", (string)null);
                });

            modelBuilder.Entity("QuickClubs.Domain.Memberships.Membership", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClubId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MembershipLevelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MembershipName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("MembershipNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("MembershipOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Membership", (string)null);
                });

            modelBuilder.Entity("QuickClubs.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<DateTime>("Registered")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("QuickClubs.Domain.Clubs.Club", b =>
                {
                    b.OwnsOne("QuickClubs.Domain.Clubs.Entities.ClubSettings", "Settings", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<Guid>("ClubId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("nvarchar(3)");

                            b1.Property<bool>("MembershipNeedsApproval")
                                .HasColumnType("bit");

                            b1.HasKey("Id", "ClubId");

                            b1.HasIndex("ClubId")
                                .IsUnique();

                            b1.ToTable("ClubSettings", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("ClubId");
                        });

                    b.OwnsOne("QuickClubs.Domain.Clubs.ValueObjects.ClubName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ClubId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Acronym")
                                .IsRequired()
                                .HasMaxLength(8)
                                .HasColumnType("nvarchar(8)")
                                .HasColumnName("Acronym");

                            b1.Property<string>("FullName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("FullName");

                            b1.HasKey("ClubId");

                            b1.HasIndex("Acronym")
                                .IsUnique();

                            b1.HasIndex("FullName")
                                .IsUnique();

                            b1.ToTable("Club");

                            b1.WithOwner()
                                .HasForeignKey("ClubId");
                        });

                    b.OwnsOne("QuickClubs.Domain.Clubs.ValueObjects.ClubWebsite", "Website", b1 =>
                        {
                            b1.Property<Guid>("ClubId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasMaxLength(80)
                                .HasColumnType("nvarchar(80)")
                                .HasColumnName("Website");

                            b1.HasKey("ClubId");

                            b1.HasIndex("Url")
                                .IsUnique();

                            b1.ToTable("Club");

                            b1.WithOwner()
                                .HasForeignKey("ClubId");
                        });

                    b.Navigation("Name")
                        .IsRequired();

                    b.Navigation("Settings");

                    b.Navigation("Website")
                        .IsRequired();
                });

            modelBuilder.Entity("QuickClubs.Domain.MembershipOptions.MembershipOption", b =>
                {
                    b.OwnsMany("QuickClubs.Domain.MembershipOptions.Entities.MembershipLevel", "Levels", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("Id");

                            b1.Property<Guid>("MembershipOptionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("nvarchar(120)");

                            b1.Property<int?>("MaxAge")
                                .HasColumnType("int");

                            b1.Property<int>("MaxMembers")
                                .HasColumnType("int");

                            b1.Property<int?>("MinAge")
                                .HasColumnType("int");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(12)
                                .HasColumnType("nvarchar(12)");

                            b1.HasKey("Id", "MembershipOptionId");

                            b1.HasIndex("MembershipOptionId");

                            b1.ToTable("MembershipLevel", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MembershipOptionId");

                            b1.OwnsOne("QuickClubs.Domain.Common.Money", "Price", b2 =>
                                {
                                    b2.Property<Guid>("MembershipLevelId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("MembershipLevelMembershipOptionId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<decimal>("Amount")
                                        .HasColumnType("decimal(6,2)")
                                        .HasColumnName("PriceAmount");

                                    b2.Property<string>("Currency")
                                        .IsRequired()
                                        .HasMaxLength(3)
                                        .HasColumnType("nvarchar(3)")
                                        .HasColumnName("PriceCurrency");

                                    b2.HasKey("MembershipLevelId", "MembershipLevelMembershipOptionId");

                                    b2.ToTable("MembershipLevel");

                                    b2.WithOwner()
                                        .HasForeignKey("MembershipLevelId", "MembershipLevelMembershipOptionId");
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.OwnsOne("QuickClubs.Domain.MembershipOptions.ValueObjects.Cutoff", "Cutoff", b1 =>
                        {
                            b1.Property<Guid>("MembershipOptionId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Day")
                                .HasColumnType("int")
                                .HasColumnName("CutoffDay");

                            b1.Property<int>("Month")
                                .HasColumnType("int")
                                .HasColumnName("CutoffMonth");

                            b1.HasKey("MembershipOptionId");

                            b1.ToTable("MembershipOption");

                            b1.WithOwner()
                                .HasForeignKey("MembershipOptionId");
                        });

                    b.Navigation("Cutoff");

                    b.Navigation("Levels");
                });

            modelBuilder.Entity("QuickClubs.Domain.Memberships.Membership", b =>
                {
                    b.OwnsOne("QuickClubs.Domain.Common.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("MembershipId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(6,2)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MembershipId");

                            b1.ToTable("Membership");

                            b1.WithOwner()
                                .HasForeignKey("MembershipId");
                        });

                    b.OwnsOne("QuickClubs.Domain.Memberships.ValueObjects.Approval", "Approval", b1 =>
                        {
                            b1.Property<Guid>("MembershipId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("ApprovalStatus")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)");

                            b1.Property<Guid?>("ApprovedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime?>("ApprovedDate")
                                .HasColumnType("datetime2");

                            b1.Property<bool>("IsApproved")
                                .HasColumnType("bit");

                            b1.Property<string>("Reason")
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.HasKey("MembershipId");

                            b1.ToTable("Membership");

                            b1.WithOwner()
                                .HasForeignKey("MembershipId");
                        });

                    b.OwnsMany("QuickClubs.Domain.Users.ValueObjects.UserId", "Members", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<Guid>("MembershipId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uniqueidentifier")
                                .HasColumnName("UserId");

                            b1.HasKey("Id");

                            b1.HasIndex("MembershipId");

                            b1.ToTable("Member", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MembershipId");
                        });

                    b.Navigation("Approval")
                        .IsRequired();

                    b.Navigation("Members");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("QuickClubs.Domain.Users.User", b =>
                {
                    b.OwnsOne("QuickClubs.Domain.Users.Entities.UserProfile", "Profile", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateOnly>("DateOfBirth")
                                .HasColumnType("date");

                            b1.HasKey("Id", "UserId");

                            b1.HasIndex("UserId")
                                .IsUnique();

                            b1.ToTable("UserProfile", (string)null);

                            b1.WithOwner("User")
                                .HasForeignKey("UserId");

                            b1.OwnsOne("QuickClubs.Domain.Common.Address", "Address", b2 =>
                                {
                                    b2.Property<Guid>("UserProfileId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("UserProfileUserId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Building")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)");

                                    b2.Property<string>("County")
                                        .IsRequired()
                                        .HasMaxLength(20)
                                        .HasColumnType("nvarchar(20)");

                                    b2.Property<string>("Locality")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)");

                                    b2.Property<string>("Postcode")
                                        .IsRequired()
                                        .HasMaxLength(8)
                                        .HasColumnType("nvarchar(8)");

                                    b2.Property<string>("Street")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)");

                                    b2.Property<string>("Town")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)");

                                    b2.HasKey("UserProfileId", "UserProfileUserId");

                                    b2.ToTable("UserProfile");

                                    b2.WithOwner()
                                        .HasForeignKey("UserProfileId", "UserProfileUserId");
                                });

                            b1.Navigation("Address")
                                .IsRequired();

                            b1.Navigation("User");
                        });

                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}

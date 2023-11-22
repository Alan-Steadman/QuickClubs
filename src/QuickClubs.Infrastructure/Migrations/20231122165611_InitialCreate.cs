using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickClubs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IsAffiliate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MembershipName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Price_Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    Approval_IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Approval_ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approval_ApprovedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Approval_ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Approval_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Period = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CutoffMonth = table.Column<int>(type: "int", nullable: true),
                    CutoffDay = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Registered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClubSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClubId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MembershipNeedsApproval = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubSettings", x => new { x.Id, x.ClubId });
                    table.ForeignKey(
                        name: "FK_ClubSettings_Club_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Member_Membership_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Membership",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    MaxMembers = table.Column<int>(type: "int", nullable: false),
                    MinAge = table.Column<int>(type: "int", nullable: true),
                    MaxAge = table.Column<int>(type: "int", nullable: true),
                    PriceAmount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipLevel", x => new { x.Id, x.MembershipOptionId });
                    table.ForeignKey(
                        name: "FK_MembershipLevel_MembershipOption_MembershipOptionId",
                        column: x => x.MembershipOptionId,
                        principalTable: "MembershipOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Address_Building = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Locality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_Town = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_County = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address_Postcode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Club_Acronym",
                table: "Club",
                column: "Acronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Club_FullName",
                table: "Club",
                column: "FullName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Club_Website",
                table: "Club",
                column: "Website",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClubSettings_ClubId",
                table: "ClubSettings",
                column: "ClubId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_MembershipId",
                table: "Member",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipLevel_MembershipOptionId",
                table: "MembershipLevel",
                column: "MembershipOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfile",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubSettings");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "MembershipLevel");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "MembershipOption");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

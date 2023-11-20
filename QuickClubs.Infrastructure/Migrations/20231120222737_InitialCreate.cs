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
                name: "club",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    acronym = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    website = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IsAffiliate = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<long>(type: "bigint", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<long>(type: "bigint", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "club_settings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    club_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MembershipNeedsApproval = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_club_settings", x => new { x.id, x.club_id });
                    table.ForeignKey(
                        name: "FK_club_settings_club_club_id",
                        column: x => x.club_id,
                        principalTable: "club",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_UserProfile", x => new { x.id, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_club_acronym",
                table: "club",
                column: "acronym",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_club_full_name",
                table: "club",
                column: "full_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_club_website",
                table: "club",
                column: "website",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_club_settings_club_id",
                table: "club_settings",
                column: "club_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IdentityId",
                table: "User",
                column: "IdentityId",
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
                name: "club_settings");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "club");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

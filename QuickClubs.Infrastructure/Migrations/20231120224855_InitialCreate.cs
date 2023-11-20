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
                    IsAffiliate = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<long>(type: "bigint", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.Id);
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
                name: "ClubSettings");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickClubs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRowVersionToAllOtherEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "MembershipOption",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Membership",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Club",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "MembershipOption");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Membership");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Club");
        }
    }
}

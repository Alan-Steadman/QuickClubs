using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickClubs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRowVersionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "User",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "User");
        }
    }
}

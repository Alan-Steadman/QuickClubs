using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickClubs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedMembershipFieldNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price_Currency",
                table: "Membership",
                newName: "PriceCurrency");

            migrationBuilder.RenameColumn(
                name: "Price_Amount",
                table: "Membership",
                newName: "PriceAmount");

            migrationBuilder.RenameColumn(
                name: "Approval_Reason",
                table: "Membership",
                newName: "ApprovalReason");

            migrationBuilder.RenameColumn(
                name: "Approval_ApprovedDate",
                table: "Membership",
                newName: "ApprovedDate");

            migrationBuilder.RenameColumn(
                name: "Approval_ApprovedBy",
                table: "Membership",
                newName: "ApprovedBy");

            migrationBuilder.RenameColumn(
                name: "Approval_ApprovalStatus",
                table: "Membership",
                newName: "ApprovalStatus");

            migrationBuilder.AlterColumn<string>(
                name: "PriceCurrency",
                table: "Membership",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceCurrency",
                table: "Membership",
                newName: "Price_Currency");

            migrationBuilder.RenameColumn(
                name: "PriceAmount",
                table: "Membership",
                newName: "Price_Amount");

            migrationBuilder.RenameColumn(
                name: "ApprovedDate",
                table: "Membership",
                newName: "Approval_ApprovedDate");

            migrationBuilder.RenameColumn(
                name: "ApprovedBy",
                table: "Membership",
                newName: "Approval_ApprovedBy");

            migrationBuilder.RenameColumn(
                name: "ApprovalStatus",
                table: "Membership",
                newName: "Approval_ApprovalStatus");

            migrationBuilder.RenameColumn(
                name: "ApprovalReason",
                table: "Membership",
                newName: "Approval_Reason");

            migrationBuilder.AlterColumn<string>(
                name: "Price_Currency",
                table: "Membership",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);
        }
    }
}

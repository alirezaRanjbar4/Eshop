using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReceiptItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueAdded",
                schema: "Model",
                table: "ReceiptServiceItem",
                newName: "ValueAddedPercent");

            migrationBuilder.RenameColumn(
                name: "ValueAdded",
                schema: "Model",
                table: "ReceiptProductItem",
                newName: "ValueAddedPercent");

            migrationBuilder.AlterColumn<long>(
                name: "Count",
                schema: "Model",
                table: "ReceiptServiceItem",
                type: "BigInt",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Float");

            migrationBuilder.AlterColumn<long>(
                name: "Count",
                schema: "Model",
                table: "ReceiptProductItem",
                type: "BigInt",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueAddedPercent",
                schema: "Model",
                table: "ReceiptServiceItem",
                newName: "ValueAdded");

            migrationBuilder.RenameColumn(
                name: "ValueAddedPercent",
                schema: "Model",
                table: "ReceiptProductItem",
                newName: "ValueAdded");

            migrationBuilder.AlterColumn<double>(
                name: "Count",
                schema: "Model",
                table: "ReceiptServiceItem",
                type: "Float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BigInt");

            migrationBuilder.AlterColumn<double>(
                name: "Count",
                schema: "Model",
                table: "ReceiptProductItem",
                type: "Float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BigInt");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class receiptChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiptNumber",
                schema: "Model",
                table: "Receipt",
                type: "Int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptSerial",
                schema: "Model",
                table: "Receipt",
                type: "Nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                schema: "Model",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "ReceiptSerial",
                schema: "Model",
                table: "Receipt");
        }
    }
}

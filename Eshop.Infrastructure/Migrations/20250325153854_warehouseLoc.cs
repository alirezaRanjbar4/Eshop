using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class warehouseLoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Count",
                schema: "Model",
                table: "ProductWarehouseLocation",
                type: "Float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "Int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Count",
                schema: "Model",
                table: "ProductWarehouseLocation",
                type: "Int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "Float");
        }
    }
}

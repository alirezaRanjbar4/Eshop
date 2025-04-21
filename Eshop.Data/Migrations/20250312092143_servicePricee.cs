using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class servicePricee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                schema: "Model",
                table: "ServicePrice",
                type: "UniqueIdentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                schema: "Model",
                table: "ServicePrice",
                type: "Datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                schema: "Model",
                table: "ProductPrice",
                type: "Datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Datetime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                schema: "Model",
                table: "ServicePrice",
                type: "UniqueIdentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                schema: "Model",
                table: "ServicePrice",
                type: "Datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "Datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpiryDate",
                schema: "Model",
                table: "ProductPrice",
                type: "Datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "Datetime",
                oldNullable: true);
        }
    }
}

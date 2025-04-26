using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class refresh2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar");

            migrationBuilder.AlterColumn<string>(
                name: "RevokedByIp",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar");

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByIp",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Token",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "RevokedByIp",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ReplacedByToken",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByIp",
                schema: "Model",
                table: "RefreshToken",
                type: "Nvarchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}

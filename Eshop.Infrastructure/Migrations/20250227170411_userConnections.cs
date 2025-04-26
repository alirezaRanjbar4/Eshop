using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class userConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                schema: "Identity",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoreType",
                schema: "Model",
                table: "Store",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StoreType",
                schema: "Model",
                table: "Store");
        }
    }
}

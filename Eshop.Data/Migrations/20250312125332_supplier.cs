using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class supplier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_User_UserId",
                schema: "Model",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UserId",
                schema: "Model",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Model",
                table: "Customer");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "Identity",
                table: "User",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "Model",
                table: "Customer",
                type: "Nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "Model",
                table: "Customer",
                type: "Nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "Nvarchar(1000)", maxLength: 1000, nullable: false),
                    Phone = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_CustomerId",
                schema: "Identity",
                table: "User",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CreateById",
                schema: "Model",
                table: "Supplier",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_ModifiedById",
                schema: "Model",
                table: "Supplier",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_StoreId",
                schema: "Model",
                table: "Supplier",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Customer_CustomerId",
                schema: "Identity",
                table: "User",
                column: "CustomerId",
                principalSchema: "Model",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Customer_CustomerId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "Model");

            migrationBuilder.DropIndex(
                name: "IX_User_CustomerId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "Model",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "Model",
                table: "Customer",
                type: "Nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Model",
                table: "Customer",
                type: "UniqueIdentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                schema: "Model",
                table: "Customer",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_User_UserId",
                schema: "Model",
                table: "Customer",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

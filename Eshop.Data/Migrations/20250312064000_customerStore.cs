using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class customerStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerStore",
                schema: "Model");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "Model",
                table: "Customer");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                schema: "Model",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StoreId",
                schema: "Model",
                table: "Customer",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Store_StoreId",
                schema: "Model",
                table: "Customer",
                column: "StoreId",
                principalSchema: "Model",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Store_StoreId",
                schema: "Model",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_StoreId",
                schema: "Model",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "StoreId",
                schema: "Model",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "Model",
                table: "Customer",
                type: "Nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CustomerStore",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerStore_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Model",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerStore_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerStore_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_CreateById",
                schema: "Model",
                table: "CustomerStore",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_CustomerId",
                schema: "Model",
                table: "CustomerStore",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_ModifiedById",
                schema: "Model",
                table: "CustomerStore",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_StoreId",
                schema: "Model",
                table: "CustomerStore",
                column: "StoreId");
        }
    }
}

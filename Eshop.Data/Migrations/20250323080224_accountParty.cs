using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class accountParty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "Model",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCardItem_Customer_CustomerId",
                schema: "Model",
                table: "ShoppingCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Customer_CustomerId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "Model");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "Identity",
                table: "User",
                newName: "AccountPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_User_CustomerId",
                schema: "Identity",
                table: "User",
                newName: "IX_User_AccountPartyId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "Model",
                table: "ShoppingCardItem",
                newName: "AccountPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCardItem_CustomerId",
                schema: "Model",
                table: "ShoppingCardItem",
                newName: "IX_ShoppingCardItem_AccountPartyId");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "Model",
                table: "Order",
                newName: "AccountPartyId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustomerId",
                schema: "Model",
                table: "Order",
                newName: "IX_Order_AccountPartyId");

            migrationBuilder.CreateTable(
                name: "AccountParty",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "Nvarchar(1000)", maxLength: 1000, nullable: false),
                    Phone = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<byte>(type: "Tinyint", nullable: false),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountParty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountParty_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountParty_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountParty_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountParty_CreateById",
                schema: "Model",
                table: "AccountParty",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountParty_ModifiedById",
                schema: "Model",
                table: "AccountParty",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountParty_StoreId",
                schema: "Model",
                table: "AccountParty",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AccountParty_AccountPartyId",
                schema: "Model",
                table: "Order",
                column: "AccountPartyId",
                principalSchema: "Model",
                principalTable: "AccountParty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCardItem_AccountParty_AccountPartyId",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "AccountPartyId",
                principalSchema: "Model",
                principalTable: "AccountParty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_AccountParty_AccountPartyId",
                schema: "Identity",
                table: "User",
                column: "AccountPartyId",
                principalSchema: "Model",
                principalTable: "AccountParty",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AccountParty_AccountPartyId",
                schema: "Model",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCardItem_AccountParty_AccountPartyId",
                schema: "Model",
                table: "ShoppingCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_User_AccountParty_AccountPartyId",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropTable(
                name: "AccountParty",
                schema: "Model");

            migrationBuilder.RenameColumn(
                name: "AccountPartyId",
                schema: "Identity",
                table: "User",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_User_AccountPartyId",
                schema: "Identity",
                table: "User",
                newName: "IX_User_CustomerId");

            migrationBuilder.RenameColumn(
                name: "AccountPartyId",
                schema: "Model",
                table: "ShoppingCardItem",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCardItem_AccountPartyId",
                schema: "Model",
                table: "ShoppingCardItem",
                newName: "IX_ShoppingCardItem_CustomerId");

            migrationBuilder.RenameColumn(
                name: "AccountPartyId",
                schema: "Model",
                table: "Order",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AccountPartyId",
                schema: "Model",
                table: "Order",
                newName: "IX_Order_CustomerId");

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "Nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "Nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false)
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
                name: "IX_Customer_CreateById",
                schema: "Model",
                table: "Customer",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ModifiedById",
                schema: "Model",
                table: "Customer",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_StoreId",
                schema: "Model",
                table: "Customer",
                column: "StoreId");

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
                name: "FK_Order_Customer_CustomerId",
                schema: "Model",
                table: "Order",
                column: "CustomerId",
                principalSchema: "Model",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCardItem_Customer_CustomerId",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "CustomerId",
                principalSchema: "Model",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Customer_CustomerId",
                schema: "Identity",
                table: "User",
                column: "CustomerId",
                principalSchema: "Model",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}

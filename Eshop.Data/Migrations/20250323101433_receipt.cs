using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class receipt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receipt",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Date = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsFinalized = table.Column<bool>(type: "Bit", nullable: false),
                    Type = table.Column<byte>(type: "Tinyint", nullable: false),
                    AccountPartyId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipt_AccountParty_AccountPartyId",
                        column: x => x.AccountPartyId,
                        principalSchema: "Model",
                        principalTable: "AccountParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferReceipt",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Date = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsFinalized = table.Column<bool>(type: "Bit", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferReceipt_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferReceipt_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferReceipt_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptItem",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    Count = table.Column<double>(type: "Float", nullable: false),
                    Price = table.Column<long>(type: "BigInt", nullable: false),
                    DiscountPrice = table.Column<long>(type: "BigInt", nullable: true),
                    DiscountPercent = table.Column<int>(type: "Int", nullable: true),
                    ValueAdded = table.Column<int>(type: "Int", nullable: true),
                    ReceiptId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalSchema: "Model",
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptItem_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Model",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferReceiptItem",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    Count = table.Column<double>(type: "Float", nullable: false),
                    TransferReceiptId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    EnteredWarehouseLocationId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    EnteredWarehouseLocationId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExitedWarehouseLocationId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferReceiptItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferReceiptItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferReceiptItem_TransferReceipt_TransferReceiptId",
                        column: x => x.TransferReceiptId,
                        principalSchema: "Model",
                        principalTable: "TransferReceipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferReceiptItem_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferReceiptItem_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferReceiptItem_WarehouseLocation_EnteredWarehouseLocationId",
                        column: x => x.EnteredWarehouseLocationId,
                        principalSchema: "Model",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferReceiptItem_WarehouseLocation_EnteredWarehouseLocationId1",
                        column: x => x.EnteredWarehouseLocationId1,
                        principalSchema: "Model",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_AccountPartyId",
                schema: "Model",
                table: "Receipt",
                column: "AccountPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CreateById",
                schema: "Model",
                table: "Receipt",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_ModifiedById",
                schema: "Model",
                table: "Receipt",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_StoreId",
                schema: "Model",
                table: "Receipt",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_CreateById",
                schema: "Model",
                table: "ReceiptItem",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_ModifiedById",
                schema: "Model",
                table: "ReceiptItem",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_ProductId",
                schema: "Model",
                table: "ReceiptItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_ReceiptId",
                schema: "Model",
                table: "ReceiptItem",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptItem_WarehouseLocationId",
                schema: "Model",
                table: "ReceiptItem",
                column: "WarehouseLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceipt_CreateById",
                schema: "Model",
                table: "TransferReceipt",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceipt_ModifiedById",
                schema: "Model",
                table: "TransferReceipt",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceipt_StoreId",
                schema: "Model",
                table: "TransferReceipt",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceiptItem_CreateById",
                schema: "Model",
                table: "TransferReceiptItem",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceiptItem_EnteredWarehouseLocationId",
                schema: "Model",
                table: "TransferReceiptItem",
                column: "EnteredWarehouseLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceiptItem_EnteredWarehouseLocationId1",
                schema: "Model",
                table: "TransferReceiptItem",
                column: "EnteredWarehouseLocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceiptItem_ModifiedById",
                schema: "Model",
                table: "TransferReceiptItem",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceiptItem_ProductId",
                schema: "Model",
                table: "TransferReceiptItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferReceiptItem_TransferReceiptId",
                schema: "Model",
                table: "TransferReceiptItem",
                column: "TransferReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptItem",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "TransferReceiptItem",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Receipt",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "TransferReceipt",
                schema: "Model");
        }
    }
}

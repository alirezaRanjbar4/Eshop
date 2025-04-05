using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class serviceItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptItem",
                schema: "Model");

            migrationBuilder.CreateTable(
                name: "ReceiptProductItem",
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
                    table.PrimaryKey("PK_ReceiptProductItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptProductItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptProductItem_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalSchema: "Model",
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptProductItem_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptProductItem_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptProductItem_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Model",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptServiceItem",
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
                    ServiceId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptServiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptServiceItem_Product_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReceiptServiceItem_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalSchema: "Model",
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptServiceItem_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Model",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptServiceItem_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptServiceItem_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptProductItem_CreateById",
                schema: "Model",
                table: "ReceiptProductItem",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptProductItem_ModifiedById",
                schema: "Model",
                table: "ReceiptProductItem",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptProductItem_ProductId",
                schema: "Model",
                table: "ReceiptProductItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptProductItem_ReceiptId",
                schema: "Model",
                table: "ReceiptProductItem",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptProductItem_WarehouseLocationId",
                schema: "Model",
                table: "ReceiptProductItem",
                column: "WarehouseLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptServiceItem_CreateById",
                schema: "Model",
                table: "ReceiptServiceItem",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptServiceItem_ModifiedById",
                schema: "Model",
                table: "ReceiptServiceItem",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptServiceItem_ProductEntityId",
                schema: "Model",
                table: "ReceiptServiceItem",
                column: "ProductEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptServiceItem_ReceiptId",
                schema: "Model",
                table: "ReceiptServiceItem",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptServiceItem_ServiceId",
                schema: "Model",
                table: "ReceiptServiceItem",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptProductItem",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ReceiptServiceItem",
                schema: "Model");

            migrationBuilder.CreateTable(
                name: "ReceiptItem",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ReceiptId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Count = table.Column<double>(type: "Float", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    DiscountPercent = table.Column<int>(type: "Int", nullable: true),
                    DiscountPrice = table.Column<long>(type: "BigInt", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Price = table.Column<long>(type: "BigInt", nullable: false),
                    ValueAdded = table.Column<int>(type: "Int", nullable: true)
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
        }
    }
}

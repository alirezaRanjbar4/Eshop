using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ShoppingCardItem",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Model");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    AccountPartyId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Status = table.Column<byte>(type: "Tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AccountParty_AccountPartyId",
                        column: x => x.AccountPartyId,
                        principalSchema: "Model",
                        principalTable: "AccountParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCardItem",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    AccountPartyId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    Count = table.Column<int>(type: "Int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCardItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCardItem_AccountParty_AccountPartyId",
                        column: x => x.AccountPartyId,
                        principalSchema: "Model",
                        principalTable: "AccountParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCardItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCardItem_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShoppingCardItem_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FinalAmount = table.Column<int>(type: "Int", nullable: false),
                    FinalPrice = table.Column<long>(type: "BigInt", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    PrimaryPrice = table.Column<long>(type: "BigInt", nullable: false),
                    RequestedAmount = table.Column<int>(type: "Int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Model",
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountPartyId",
                schema: "Model",
                table: "Order",
                column: "AccountPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreateById",
                schema: "Model",
                table: "Order",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ModifiedById",
                schema: "Model",
                table: "Order",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StoreId",
                schema: "Model",
                table: "Order",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_CreateById",
                schema: "Model",
                table: "OrderItem",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ModifiedById",
                schema: "Model",
                table: "OrderItem",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                schema: "Model",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                schema: "Model",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCardItem_AccountPartyId",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "AccountPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCardItem_CreateById",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCardItem_ModifiedById",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCardItem_ProductId",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "ProductId");
        }
    }
}

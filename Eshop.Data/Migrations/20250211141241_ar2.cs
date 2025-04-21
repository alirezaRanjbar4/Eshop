using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class ar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImage",
                schema: "Model");

            migrationBuilder.CreateTable(
                name: "Image",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false),
                    URL = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Image_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Image_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrice",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Price = table.Column<long>(type: "BigInt", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrice_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProductPrice_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPrice_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransfer",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Count = table.Column<int>(type: "Int", nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    Type = table.Column<byte>(type: "Tinyint", nullable: false),
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
                    table.PrimaryKey("PK_ProductTransfer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTransfer_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTransfer_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTransfer_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTransfer_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Model",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_CreateById",
                schema: "Model",
                table: "Image",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ModifiedById",
                schema: "Model",
                table: "Image",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ProductId",
                schema: "Model",
                table: "Image",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_CreateById",
                schema: "Model",
                table: "ProductPrice",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_ModifiedById",
                schema: "Model",
                table: "ProductPrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrice_ProductId",
                schema: "Model",
                table: "ProductPrice",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_CreateById",
                schema: "Model",
                table: "ProductTransfer",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_ModifiedById",
                schema: "Model",
                table: "ProductTransfer",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_ProductId",
                schema: "Model",
                table: "ProductTransfer",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransfer_WarehouseLocationId",
                schema: "Model",
                table: "ProductTransfer",
                column: "WarehouseLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ProductPrice",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ProductTransfer",
                schema: "Model");

            migrationBuilder.CreateTable(
                name: "ProductImage",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Name = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImage_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductImage_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_CreateById",
                schema: "Model",
                table: "ProductImage",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ModifiedById",
                schema: "Model",
                table: "ProductImage",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                schema: "Model",
                table: "ProductImage",
                column: "ProductId");
        }
    }
}

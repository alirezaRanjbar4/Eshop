using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class Service : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrice_Product_ProductId",
                schema: "Model",
                table: "ProductPrice");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                schema: "Model",
                table: "ProductPrice",
                type: "UniqueIdentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceEntityId",
                schema: "Model",
                table: "Image",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Service",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Service_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCategory",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    ServiceId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Model",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Model",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceCategory_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicePrice",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Price = table.Column<long>(type: "BigInt", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ServiceId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePrice_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Model",
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePrice_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicePrice_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_ServiceEntityId",
                schema: "Model",
                table: "Image",
                column: "ServiceEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_CreateById",
                schema: "Model",
                table: "Service",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Service_ModifiedById",
                schema: "Model",
                table: "Service",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Service_StoreId",
                schema: "Model",
                table: "Service",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_CategoryId",
                schema: "Model",
                table: "ServiceCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_CreateById",
                schema: "Model",
                table: "ServiceCategory",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_ModifiedById",
                schema: "Model",
                table: "ServiceCategory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCategory_ServiceId",
                schema: "Model",
                table: "ServiceCategory",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrice_CreateById",
                schema: "Model",
                table: "ServicePrice",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrice_ModifiedById",
                schema: "Model",
                table: "ServicePrice",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePrice_ServiceId",
                schema: "Model",
                table: "ServicePrice",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Service_ServiceEntityId",
                schema: "Model",
                table: "Image",
                column: "ServiceEntityId",
                principalSchema: "Model",
                principalTable: "Service",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrice_Product_ProductId",
                schema: "Model",
                table: "ProductPrice",
                column: "ProductId",
                principalSchema: "Model",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Service_ServiceEntityId",
                schema: "Model",
                table: "Image");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrice_Product_ProductId",
                schema: "Model",
                table: "ProductPrice");

            migrationBuilder.DropTable(
                name: "ServiceCategory",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ServicePrice",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Service",
                schema: "Model");

            migrationBuilder.DropIndex(
                name: "IX_Image_ServiceEntityId",
                schema: "Model",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ServiceEntityId",
                schema: "Model",
                table: "Image");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                schema: "Model",
                table: "ProductPrice",
                type: "UniqueIdentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "UniqueIdentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrice_Product_ProductId",
                schema: "Model",
                table: "ProductPrice",
                column: "ProductId",
                principalSchema: "Model",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

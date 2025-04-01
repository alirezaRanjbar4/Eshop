using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class storePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NextPaymentDate",
                schema: "Model",
                table: "Store",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "StorePayment",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    Amount = table.Column<long>(type: "BigInt", nullable: false),
                    Date = table.Column<DateTime>(type: "Datetime", nullable: false),
                    PaymentMethod = table.Column<byte>(type: "Tinyint", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorePayment_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePayment_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePayment_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorePayment_CreateById",
                schema: "Model",
                table: "StorePayment",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_StorePayment_ModifiedById",
                schema: "Model",
                table: "StorePayment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StorePayment_StoreId",
                schema: "Model",
                table: "StorePayment",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorePayment",
                schema: "Model");

            migrationBuilder.DropColumn(
                name: "NextPaymentDate",
                schema: "Model",
                table: "Store");
        }
    }
}

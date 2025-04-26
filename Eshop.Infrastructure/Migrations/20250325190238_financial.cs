using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class financial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrentCredit",
                schema: "Model",
                table: "AccountParty",
                type: "BigInt",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "FinancialDocument",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    Amount = table.Column<long>(type: "BigInt", nullable: false),
                    Date = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Type = table.Column<byte>(type: "Tinyint", nullable: false),
                    PaymentMethod = table.Column<byte>(type: "Tinyint", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    AccountPartyId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialDocument_AccountParty_AccountPartyId",
                        column: x => x.AccountPartyId,
                        principalSchema: "Model",
                        principalTable: "AccountParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialDocument_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialDocument_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialDocument_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptFinancialDocument",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    ReceiptId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FinancialDocumentId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptFinancialDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReceiptFinancialDocument_FinancialDocument_FinancialDocumentId",
                        column: x => x.FinancialDocumentId,
                        principalSchema: "Model",
                        principalTable: "FinancialDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptFinancialDocument_Receipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalSchema: "Model",
                        principalTable: "Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptFinancialDocument_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptFinancialDocument_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDocument_AccountPartyId",
                schema: "Model",
                table: "FinancialDocument",
                column: "AccountPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDocument_CreateById",
                schema: "Model",
                table: "FinancialDocument",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDocument_ModifiedById",
                schema: "Model",
                table: "FinancialDocument",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialDocument_StoreId",
                schema: "Model",
                table: "FinancialDocument",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFinancialDocument_CreateById",
                schema: "Model",
                table: "ReceiptFinancialDocument",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFinancialDocument_FinancialDocumentId",
                schema: "Model",
                table: "ReceiptFinancialDocument",
                column: "FinancialDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFinancialDocument_ModifiedById",
                schema: "Model",
                table: "ReceiptFinancialDocument",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptFinancialDocument_ReceiptId",
                schema: "Model",
                table: "ReceiptFinancialDocument",
                column: "ReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceiptFinancialDocument",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "FinancialDocument",
                schema: "Model");

            migrationBuilder.DropColumn(
                name: "CurrentCredit",
                schema: "Model",
                table: "AccountParty");
        }
    }
}

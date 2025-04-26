using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class refresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Token = table.Column<string>(type: "Nvarchar", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "Datetime", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Datetime", nullable: false),
                    CreatedByIp = table.Column<string>(type: "Nvarchar", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "Datetime", nullable: true),
                    RevokedByIp = table.Column<string>(type: "Nvarchar", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "Nvarchar", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_CreateById",
                schema: "Model",
                table: "RefreshToken",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ModifiedById",
                schema: "Model",
                table: "RefreshToken",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "Model",
                table: "RefreshToken",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "Model");
        }
    }
}

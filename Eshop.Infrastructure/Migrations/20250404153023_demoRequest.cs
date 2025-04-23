using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class demoRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoRequest",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: true),
                    StoreName = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "Nvarchar(1000)", maxLength: 1000, nullable: true),
                    Phone = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: true),
                    WorkBranch = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    AdminDescription = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    IsAnswered = table.Column<bool>(type: "Bit", nullable: false, defaultValue: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemoRequest_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemoRequest_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DemoRequest_CreateById",
                schema: "Model",
                table: "DemoRequest",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_DemoRequest_ModifiedById",
                schema: "Model",
                table: "DemoRequest",
                column: "ModifiedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoRequest",
                schema: "Model");
        }
    }
}

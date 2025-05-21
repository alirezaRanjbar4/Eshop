using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class scheduler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SchedulerTask",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Title = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    Priority = table.Column<byte>(type: "Tinyint", nullable: false),
                    Type = table.Column<byte>(type: "Tinyint", nullable: false),
                    RepetType = table.Column<byte>(type: "Tinyint", nullable: true),
                    Date = table.Column<DateOnly>(type: "Date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "Time", nullable: false),
                    ReminderDateTime = table.Column<DateTime>(type: "Datetime", nullable: true),
                    RepeatCount = table.Column<int>(type: "Int", nullable: false),
                    RelatedId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    StoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulerTask_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulerTask_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchedulerTask_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchedulerTaskVendor",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    IsDone = table.Column<bool>(type: "Bit", nullable: false),
                    Date = table.Column<DateOnly>(type: "Date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "Time", nullable: false),
                    ReminderDateTime = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Description = table.Column<string>(type: "Nvarchar(4000)", maxLength: 4000, nullable: true),
                    SchedulerTaskId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    VendorId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulerTaskVendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulerTaskVendor_SchedulerTask_SchedulerTaskId",
                        column: x => x.SchedulerTaskId,
                        principalSchema: "Model",
                        principalTable: "SchedulerTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchedulerTaskVendor_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchedulerTaskVendor_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchedulerTaskVendor_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "Model",
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTask_CreateById",
                schema: "Model",
                table: "SchedulerTask",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTask_ModifiedById",
                schema: "Model",
                table: "SchedulerTask",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTask_StoreId",
                schema: "Model",
                table: "SchedulerTask",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTaskVendor_CreateById",
                schema: "Model",
                table: "SchedulerTaskVendor",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTaskVendor_ModifiedById",
                schema: "Model",
                table: "SchedulerTaskVendor",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTaskVendor_SchedulerTaskId",
                schema: "Model",
                table: "SchedulerTaskVendor",
                column: "SchedulerTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulerTaskVendor_VendorId",
                schema: "Model",
                table: "SchedulerTaskVendor",
                column: "VendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulerTaskVendor",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "SchedulerTask",
                schema: "Model");
        }
    }
}

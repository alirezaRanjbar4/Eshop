using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rasam.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Model");

            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    WrongPasswordCount = table.Column<int>(type: "int", nullable: false),
                    LastLoginOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    RoutePath = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "Nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    EnglishName = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "Bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Store_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Store_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "UniqueIdentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Model",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Category_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerStore",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    CustomerId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerStore_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Model",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerStore_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerStore_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Status = table.Column<byte>(type: "Tinyint", nullable: false),
                    CustomerId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Model",
                        principalTable: "Customer",
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
                name: "Product",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false),
                    SKU = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    MeasurementUnit = table.Column<byte>(type: "Tinyint", nullable: false),
                    OpenToSell = table.Column<bool>(type: "Bit", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendor_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendor_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "Nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsLock = table.Column<bool>(type: "Bit", nullable: false),
                    StoreId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouse_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "Model",
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Warehouse_User_ModifiedById",
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
                    RequestedAmount = table.Column<int>(type: "Int", nullable: false),
                    FinalAmount = table.Column<int>(type: "Int", nullable: false),
                    PrimaryPrice = table.Column<long>(type: "BigInt", nullable: false),
                    FinalPrice = table.Column<long>(type: "BigInt", nullable: false),
                    OrderId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Model",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductCategory_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "Nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ShoppingCardItem",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Count = table.Column<int>(type: "Int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCardItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCardItem_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Model",
                        principalTable: "Customer",
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
                name: "WarehouseLocation",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Name = table.Column<string>(type: "Nvarchar(50)", maxLength: 50, nullable: false),
                    LocationNumber = table.Column<int>(type: "Int", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseLocation_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseLocation_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalSchema: "Model",
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductWarehouseLocation",
                schema: "Model",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newsequentialid()"),
                    Count = table.Column<int>(type: "Int", nullable: false),
                    WarehouseLocationId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "Datetime", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreateById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifiedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWarehouseLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductWarehouseLocation_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Model",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductWarehouseLocation_User_CreateById",
                        column: x => x.CreateById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductWarehouseLocation_User_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductWarehouseLocation_WarehouseLocation_WarehouseLocationId",
                        column: x => x.WarehouseLocationId,
                        principalSchema: "Model",
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_CreateById",
                schema: "Model",
                table: "Category",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ModifiedById",
                schema: "Model",
                table: "Category",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentId",
                schema: "Model",
                table: "Category",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_StoreId",
                schema: "Model",
                table: "Category",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CreateById",
                schema: "Model",
                table: "Customer",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ModifiedById",
                schema: "Model",
                table: "Customer",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                schema: "Model",
                table: "Customer",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_CreateById",
                schema: "Model",
                table: "CustomerStore",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_CustomerId",
                schema: "Model",
                table: "CustomerStore",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_ModifiedById",
                schema: "Model",
                table: "CustomerStore",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerStore_StoreId",
                schema: "Model",
                table: "CustomerStore",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CreateById",
                schema: "Model",
                table: "Order",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                schema: "Model",
                table: "Order",
                column: "CustomerId");

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
                name: "IX_Product_CreateById",
                schema: "Model",
                table: "Product",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ModifiedById",
                schema: "Model",
                table: "Product",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Product_StoreId",
                schema: "Model",
                table: "Product",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CategoryId",
                schema: "Model",
                table: "ProductCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_CreateById",
                schema: "Model",
                table: "ProductCategory",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ModifiedById",
                schema: "Model",
                table: "ProductCategory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategory_ProductId",
                schema: "Model",
                table: "ProductCategory",
                column: "ProductId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouseLocation_CreateById",
                schema: "Model",
                table: "ProductWarehouseLocation",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouseLocation_ModifiedById",
                schema: "Model",
                table: "ProductWarehouseLocation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouseLocation_ProductId",
                schema: "Model",
                table: "ProductWarehouseLocation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouseLocation_WarehouseLocationId",
                schema: "Model",
                table: "ProductWarehouseLocation",
                column: "WarehouseLocationId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                schema: "Identity",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCardItem_CreateById",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCardItem_CustomerId",
                schema: "Model",
                table: "ShoppingCardItem",
                column: "CustomerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Store_CreateById",
                schema: "Model",
                table: "Store",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Store_ModifiedById",
                schema: "Model",
                table: "Store",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "User",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "Identity",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CreateById",
                schema: "Model",
                table: "Vendor",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_ModifiedById",
                schema: "Model",
                table: "Vendor",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_StoreId",
                schema: "Model",
                table: "Vendor",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_UserId",
                schema: "Model",
                table: "Vendor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_CreateById",
                schema: "Model",
                table: "Warehouse",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_ModifiedById",
                schema: "Model",
                table: "Warehouse",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_StoreId",
                schema: "Model",
                table: "Warehouse",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_CreateById",
                schema: "Model",
                table: "WarehouseLocation",
                column: "CreateById");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_ModifiedById",
                schema: "Model",
                table: "WarehouseLocation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseLocation_WarehouseId",
                schema: "Model",
                table: "WarehouseLocation",
                column: "WarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerStore",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "OrderItem",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ProductCategory",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ProductImage",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "ProductWarehouseLocation",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "RoleClaim",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ShoppingCardItem",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserLogin",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserToken",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Vendor",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "WarehouseLocation",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Warehouse",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "Model");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Identity");
        }
    }
}

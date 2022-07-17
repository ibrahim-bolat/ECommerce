using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ECommerce.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UserIdendityNo = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AddressTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AddressType = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    MainStreet = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    NeighborhoodOrVillage = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    District = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Country = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    RegionOrState = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    BuildingNo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    FlatNo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    AddressDetails = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DefaultAddress = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ImagePath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ImageAltText = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Profil = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedByName = table.Column<string>(type: "text", nullable: true),
                    ModifiedByName = table.Column<string>(type: "text", nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedByName", "CreatedTime", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Name", "NormalizedName", "Note" },
                values: new object[] { 1, "6c95e1a1-27eb-4a06-9760-df0449f4352d", "Owner", new DateTime(2022, 7, 15, 23, 5, 45, 309, DateTimeKind.Local).AddTicks(6602), true, false, "Owner", new DateTime(2022, 7, 15, 23, 5, 45, 309, DateTimeKind.Local).AddTicks(6614), "Admin", "ADMİN", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedByName", "CreatedTime", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedByName", "ModifiedTime", "NormalizedEmail", "NormalizedUserName", "Note", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserIdendityNo", "UserName" },
                values: new object[] { 1, 0, "49bbccdd-4dfc-4eff-8ef2-247811da2391", "Owner", new DateTime(2022, 7, 15, 23, 5, 45, 310, DateTimeKind.Local).AddTicks(6176), null, "bolat6606@hotmail.com", true, "İbrahim", true, false, "Bolat", false, null, "Owner", new DateTime(2022, 7, 15, 23, 5, 45, 310, DateTimeKind.Local).AddTicks(6183), "BOLAT6606@HOTMAIL.COM", "BOLAT6606", null, "AQAAAAEAACcQAAAAEI/T4NTywSt9IgSGWtRLed/xepGxofVv0o0x3Cw61DL+AJxo2e1kwfAnqLfk4D5wYg==", "05325757966", false, "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA", false, null, "bolat6606" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "AddressDetails", "AddressTitle", "AddressType", "BuildingNo", "City", "Country", "CreatedByName", "CreatedTime", "DefaultAddress", "District", "FlatNo", "IsActive", "IsDeleted", "MainStreet", "ModifiedByName", "ModifiedTime", "NeighborhoodOrVillage", "Note", "PostalCode", "RegionOrState", "Street", "UserId" },
                values: new object[,]
                {
                    { 1, "Naci Bekir Mahallesi ,Atılım Cad. Ateş Sok. No:40/7 06500 Yenimahalle/Ankara/Türkiye", "Evim", "Ev", "40", "Ankara", "Turkiye", "Admin", new DateTime(2022, 7, 15, 23, 5, 45, 319, DateTimeKind.Local).AddTicks(1835), false, "Yenimahalle", "7", false, false, "Atılım", "Admin", new DateTime(2022, 7, 15, 23, 5, 45, 319, DateTimeKind.Local).AddTicks(1848), "Naci Bekir", null, "06500", "İç Anadolu", "Ateş", 1 },
                    { 2, "Mustafa Kemal Mahallesi ,Eskişehir Yolu  Kütahya Sok. No:280/7 06500 Çankaya/Ankara/Türkiye", "İş", "İş", "280", "Ankara", "Turkiye", "Admin", new DateTime(2022, 7, 15, 23, 5, 45, 319, DateTimeKind.Local).AddTicks(1875), true, "Çankaya", "7", false, false, "Eskişehir Yolu", "Admin", new DateTime(2022, 7, 15, 23, 5, 45, 319, DateTimeKind.Local).AddTicks(1876), "Mustafa Kemal", null, "06100", "İç Anadolu", "Kütahya", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserImages",
                columns: new[] { "Id", "CreatedByName", "CreatedTime", "ImageAltText", "ImagePath", "ImageTitle", "IsActive", "IsDeleted", "ModifiedByName", "ModifiedTime", "Note", "Profil", "UserId" },
                values: new object[] { 1, "Admin", new DateTime(2022, 7, 15, 23, 5, 45, 319, DateTimeKind.Local).AddTicks(3165), "Profil", "/admin/images/layout_img/g1.jpg", "ProfilResmi", false, false, "Admin", new DateTime(2022, 7, 15, 23, 5, 45, 319, DateTimeKind.Local).AddTicks(3168), null, true, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

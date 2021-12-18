using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prjAllShow.Backend.Migrations.IdentityDB
{
    public partial class InitialIdentityCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllShowRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllShowRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllShowUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_AllShowUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllShowUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllShowUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllShowUserClaims_AllShowUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllShowUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllShowUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllShowUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AllShowUserLogins_AllShowUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllShowUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllShowUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllShowUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AllShowUserTokens_AllShowUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllShowUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpSex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmpAccountState = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSetting_AllShowUsers_Id",
                        column: x => x.Id,
                        principalTable: "AllShowUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MemEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemDiminutive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemSex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemPic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemAccountState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemCheckNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MemUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberSetting_AllShowUsers_Id",
                        column: x => x.Id,
                        principalTable: "AllShowUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmpNo = table.Column<int>(type: "int", nullable: true),
                    ShThePic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShBoss = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShTel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShAbout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShLogoPic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShAdState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShAdTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShAdPic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShPopShop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShCheckState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShCheckDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShPwdState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShStopRightStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShStopRightEnddate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopSetting_AllShowUsers_Id",
                        column: x => x.Id,
                        principalTable: "AllShowUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllShowRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllShowRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllShowRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllShowUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllShowUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AllShowUserRoles_AllShowUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AllShowUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllShowUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllShowRoleClaims_RoleId",
                table: "AllShowRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AllShowUserClaims_UserId",
                table: "AllShowUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AllShowUserLogins_UserId",
                table: "AllShowUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AllShowUserRoles_RoleId",
                table: "AllShowUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AllShowUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AllShowUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllShowRoleClaims");

            migrationBuilder.DropTable(
                name: "AllShowRoles");

            migrationBuilder.DropTable(
                name: "AllShowUserClaims");

            migrationBuilder.DropTable(
                name: "AllShowUserLogins");

            migrationBuilder.DropTable(
                name: "AllShowUserRoles");

            migrationBuilder.DropTable(
                name: "AllShowUserTokens");

            migrationBuilder.DropTable(
                name: "EmployeeSetting");

            migrationBuilder.DropTable(
                name: "MemberSetting");

            migrationBuilder.DropTable(
                name: "ShopSetting");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AllShowUsers");
        }
    }
}

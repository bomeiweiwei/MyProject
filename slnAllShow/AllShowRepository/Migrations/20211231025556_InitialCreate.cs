using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllShowRepository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorityFunction",
                columns: table => new
                {
                    AuthorityNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorityName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorityFunction", x => x.AuthorityNo);
                });

            migrationBuilder.CreateTable(
                name: "DbFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Contnet = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmpAccount = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmpPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    EmpSex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    EmpBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpTel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmpAccountState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmpNo);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MemPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemDiminutive = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MemName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MemSex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    MemTel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MemAddress = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    MemPic = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MemAccountState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    MemCheckNumber = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MemCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MemUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemNo);
                });

            migrationBuilder.CreateTable(
                name: "ShClass",
                columns: table => new
                {
                    ShClassNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShClassName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShClass", x => x.ShClassNo);
                });

            migrationBuilder.CreateTable(
                name: "Announcement",
                columns: table => new
                {
                    AnnouncementNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<int>(type: "int", nullable: true),
                    AnnouncementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AnnouncementContent = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.AnnouncementNo);
                    table.ForeignKey(
                        name: "FK_Announcement_Employee_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employee",
                        principalColumn: "EmpNo");
                });

            migrationBuilder.CreateTable(
                name: "Authority",
                columns: table => new
                {
                    EmpNo = table.Column<int>(type: "int", nullable: false),
                    AuthorityNo = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authority", x => new { x.EmpNo, x.AuthorityNo });
                    table.ForeignKey(
                        name: "FK_Authority_AuthorityFunction_AuthorityNo",
                        column: x => x.AuthorityNo,
                        principalTable: "AuthorityFunction",
                        principalColumn: "AuthorityNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authority_Employee_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employee",
                        principalColumn: "EmpNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shop",
                columns: table => new
                {
                    ShNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpNo = table.Column<int>(type: "int", nullable: true),
                    ShThePic = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ShName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShAccount = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShPwd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShBoss = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ShContact = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ShAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ShTel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ShEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ShAbout = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ShLogoPic = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ShUrl = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShAdState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ShAdTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShAdPic = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ShPopShop = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ShCheckState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ShStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShCheckDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShPwdState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ShStopRightStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShStopRightEnddate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop", x => x.ShNo);
                    table.ForeignKey(
                        name: "FK_Shop_Employee_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employee",
                        principalColumn: "EmpNo");
                });

            migrationBuilder.CreateTable(
                name: "MemberList",
                columns: table => new
                {
                    OrderNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemNo = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberList", x => x.OrderNo);
                    table.ForeignKey(
                        name: "FK_MemberList_Member_MemNo",
                        column: x => x.MemNo,
                        principalTable: "Member",
                        principalColumn: "MemNo");
                });

            migrationBuilder.CreateTable(
                name: "ProductClass",
                columns: table => new
                {
                    ProClassNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShClassNo = table.Column<int>(type: "int", nullable: false),
                    ProClassName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductClass", x => x.ProClassNo);
                    table.ForeignKey(
                        name: "FK_ProductClass_ShClass_ShClassNo",
                        column: x => x.ShClassNo,
                        principalTable: "ShClass",
                        principalColumn: "ShClassNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    AdNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShNo = table.Column<int>(type: "int", nullable: true),
                    EmpNo = table.Column<int>(type: "int", nullable: true),
                    AdTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AdApplyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdPrice = table.Column<int>(type: "int", nullable: false),
                    AdPic = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AdURL = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AdCheckState = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.AdNo);
                    table.ForeignKey(
                        name: "FK_Advertisement_Employee_EmpNo",
                        column: x => x.EmpNo,
                        principalTable: "Employee",
                        principalColumn: "EmpNo");
                    table.ForeignKey(
                        name: "FK_Advertisement_Shop_ShNo",
                        column: x => x.ShNo,
                        principalTable: "Shop",
                        principalColumn: "ShNo");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteShopList",
                columns: table => new
                {
                    ShNo = table.Column<int>(type: "int", nullable: false),
                    MemNo = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteShopList", x => new { x.ShNo, x.MemNo });
                    table.ForeignKey(
                        name: "FK_FavoriteShopList_Member_MemNo",
                        column: x => x.MemNo,
                        principalTable: "Member",
                        principalColumn: "MemNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteShopList_Shop_ShNo",
                        column: x => x.ShNo,
                        principalTable: "Shop",
                        principalColumn: "ShNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShClassList",
                columns: table => new
                {
                    ShClassNo = table.Column<int>(type: "int", nullable: false),
                    ShNo = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShClassList", x => new { x.ShClassNo, x.ShNo });
                    table.ForeignKey(
                        name: "FK_ShClassList_ShClass_ShClassNo",
                        column: x => x.ShClassNo,
                        principalTable: "ShClass",
                        principalColumn: "ShClassNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShClassList_Shop_ShNo",
                        column: x => x.ShNo,
                        principalTable: "Shop",
                        principalColumn: "ShNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopOrder",
                columns: table => new
                {
                    ShoporderNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<int>(type: "int", nullable: true),
                    ShNo = table.Column<int>(type: "int", nullable: true),
                    OrderPrice = table.Column<int>(type: "int", nullable: false),
                    ReferredToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RecipientTel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RecipientAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PayType = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopOrder", x => x.ShoporderNo);
                    table.ForeignKey(
                        name: "FK_ShopOrder_MemberList_OrderNo",
                        column: x => x.OrderNo,
                        principalTable: "MemberList",
                        principalColumn: "OrderNo");
                    table.ForeignKey(
                        name: "FK_ShopOrder_Shop_ShNo",
                        column: x => x.ShNo,
                        principalTable: "Shop",
                        principalColumn: "ShNo");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShNo = table.Column<int>(type: "int", nullable: true),
                    ProClassNo = table.Column<int>(type: "int", nullable: true),
                    ProName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProPrice = table.Column<int>(type: "int", nullable: false),
                    ProStatement = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ProPic1 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProPic2 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProPic3 = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ProCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProOffshelfDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProPop = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProNo);
                    table.ForeignKey(
                        name: "FK_Product_ProductClass_ProClassNo",
                        column: x => x.ProClassNo,
                        principalTable: "ProductClass",
                        principalColumn: "ProClassNo");
                    table.ForeignKey(
                        name: "FK_Product_Shop_ShNo",
                        column: x => x.ShNo,
                        principalTable: "Shop",
                        principalColumn: "ShNo");
                });

            migrationBuilder.CreateTable(
                name: "OrderList",
                columns: table => new
                {
                    ProNo = table.Column<int>(type: "int", nullable: false),
                    ShoporderNo = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => new { x.ProNo, x.ShoporderNo });
                    table.ForeignKey(
                        name: "FK_OrderList_Product_ProNo",
                        column: x => x.ProNo,
                        principalTable: "Product",
                        principalColumn: "ProNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderList_ShopOrder_ShoporderNo",
                        column: x => x.ShoporderNo,
                        principalTable: "ShopOrder",
                        principalColumn: "ShoporderNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_EmpNo",
                table: "Advertisement",
                column: "EmpNo");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_ShNo",
                table: "Advertisement",
                column: "ShNo");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_EmpNo",
                table: "Announcement",
                column: "EmpNo");

            migrationBuilder.CreateIndex(
                name: "IX_Authority_AuthorityNo",
                table: "Authority",
                column: "AuthorityNo");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteShopList_MemNo",
                table: "FavoriteShopList",
                column: "MemNo");

            migrationBuilder.CreateIndex(
                name: "IX_MemberList_MemNo",
                table: "MemberList",
                column: "MemNo");

            migrationBuilder.CreateIndex(
                name: "IX_OrderList_ShoporderNo",
                table: "OrderList",
                column: "ShoporderNo");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProClassNo",
                table: "Product",
                column: "ProClassNo");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ShNo",
                table: "Product",
                column: "ShNo");

            migrationBuilder.CreateIndex(
                name: "IX_ProductClass_ShClassNo",
                table: "ProductClass",
                column: "ShClassNo");

            migrationBuilder.CreateIndex(
                name: "IX_ShClassList_ShNo",
                table: "ShClassList",
                column: "ShNo");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_EmpNo",
                table: "Shop",
                column: "EmpNo");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_OrderNo",
                table: "ShopOrder",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_ShNo",
                table: "ShopOrder",
                column: "ShNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "Announcement");

            migrationBuilder.DropTable(
                name: "Authority");

            migrationBuilder.DropTable(
                name: "DbFiles");

            migrationBuilder.DropTable(
                name: "FavoriteShopList");

            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.DropTable(
                name: "ShClassList");

            migrationBuilder.DropTable(
                name: "AuthorityFunction");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ShopOrder");

            migrationBuilder.DropTable(
                name: "ProductClass");

            migrationBuilder.DropTable(
                name: "MemberList");

            migrationBuilder.DropTable(
                name: "Shop");

            migrationBuilder.DropTable(
                name: "ShClass");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}

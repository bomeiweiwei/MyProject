// <auto-generated />
using System;
using AllShow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AllShowRepository.Migrations
{
    [DbContext(typeof(AllShowDBContext))]
    [Migration("20220112075150_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AllShow.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AdNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AdApplyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdCheckState")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("AdPic")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("AdPrice")
                        .HasColumnType("int");

                    b.Property<DateTime>("AdStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AdTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("AdTitle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("AdURL")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("EmpNo")
                        .HasColumnType("int");

                    b.Property<int?>("ShNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpNo");

                    b.HasIndex("ShNo");

                    b.ToTable("Advertisement");
                });

            modelBuilder.Entity("AllShow.Models.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AnnouncementNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AnnouncementContent")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("AnnouncementType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EmpNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmpNo");

                    b.ToTable("Announcement");
                });

            modelBuilder.Entity("AllShow.Models.Authority", b =>
                {
                    b.Property<int>("EmpNo")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("AuthorityNo")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("EmpNo", "AuthorityNo");

                    b.HasIndex("AuthorityNo");

                    b.ToTable("Authority");
                });

            modelBuilder.Entity("AllShow.Models.AuthorityFunction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AuthorityNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AuthorityName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("AuthorityFunction");
                });

            modelBuilder.Entity("AllShow.Models.DbFiles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("Contnet")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DbFiles");
                });

            modelBuilder.Entity("AllShow.Models.EmployeeSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmpNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EmpAccount")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("EmpAccountState")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<DateTime>("EmpBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmpEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("EmpName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("EmpPwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpSex")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("EmpTel")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LeaveDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("AllShow.Models.FavoriteShopList", b =>
                {
                    b.Property<int>("ShNo")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("MemNo")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ShNo", "MemNo");

                    b.HasIndex("MemNo");

                    b.ToTable("FavoriteShopList");
                });

            modelBuilder.Entity("AllShow.Models.MemberList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("MemNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MemNo");

                    b.ToTable("MemberList");
                });

            modelBuilder.Entity("AllShow.Models.MemberSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MemNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("MemAccountState")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("MemAddress")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime?>("MemBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemCheckNumber")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<DateTime>("MemCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("MemDiminutive")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("MemEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("MemName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("MemPic")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("MemPwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemSex")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("MemTel")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("MemUpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("AllShow.Models.OrderList", b =>
                {
                    b.Property<int>("ProNo")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("ShoporderNo")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProNo", "ShoporderNo");

                    b.HasIndex("ShoporderNo");

                    b.ToTable("OrderList");
                });

            modelBuilder.Entity("AllShow.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ProClassNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ProCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("ProOffshelfDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProPic1")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProPic2")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProPic3")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ProPop")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("ProPrice")
                        .HasColumnType("int");

                    b.Property<string>("ProState")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("ProStatement")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("ProUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ShNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProClassNo");

                    b.HasIndex("ShNo");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("AllShow.Models.ProductClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProClassNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ProClassName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("ShClassNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShClassNo");

                    b.ToTable("ProductClass");
                });

            modelBuilder.Entity("AllShow.Models.ShClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ShClassNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ShClassName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("ShClass");
                });

            modelBuilder.Entity("AllShow.Models.ShClassList", b =>
                {
                    b.Property<int>("ShClassNo")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("ShNo")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ShClassNo", "ShNo");

                    b.HasIndex("ShNo");

                    b.ToTable("ShClassList");
                });

            modelBuilder.Entity("AllShow.Models.ShopOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ShoporderNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("OrderNo")
                        .HasColumnType("int");

                    b.Property<int>("OrderPrice")
                        .HasColumnType("int");

                    b.Property<string>("OrderState")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("PayType")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("RecipientAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("RecipientTel")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("ReferredToDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ShNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderNo");

                    b.HasIndex("ShNo");

                    b.ToTable("ShopOrder");
                });

            modelBuilder.Entity("AllShow.Models.ShopSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ShNo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("EmpNo")
                        .HasColumnType("int");

                    b.Property<string>("ShAbout")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ShAccount")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ShAdPic")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ShAdState")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("ShAdTitle")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ShAddress")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ShBoss")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("ShCheckDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShCheckState")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("ShContact")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ShEmail")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("ShEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShLogoPic")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ShName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ShPopShop")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("ShPwd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShPwdState")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<DateTime?>("ShStartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShStopRightEnddate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShStopRightStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShTel")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ShThePic")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("ShUrl")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("EmpNo");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("AllShow.Models.Advertisement", b =>
                {
                    b.HasOne("AllShow.Models.EmployeeSetting", "Employee")
                        .WithMany("Advertisement")
                        .HasForeignKey("EmpNo");

                    b.HasOne("AllShow.Models.ShopSetting", "Shop")
                        .WithMany("Advertisement")
                        .HasForeignKey("ShNo");

                    b.Navigation("Employee");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("AllShow.Models.Announcement", b =>
                {
                    b.HasOne("AllShow.Models.EmployeeSetting", "Employee")
                        .WithMany("Announcement")
                        .HasForeignKey("EmpNo");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AllShow.Models.Authority", b =>
                {
                    b.HasOne("AllShow.Models.AuthorityFunction", "AuthorityFunction")
                        .WithMany("Authority")
                        .HasForeignKey("AuthorityNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllShow.Models.EmployeeSetting", "Employee")
                        .WithMany("Authorities")
                        .HasForeignKey("EmpNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AuthorityFunction");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AllShow.Models.FavoriteShopList", b =>
                {
                    b.HasOne("AllShow.Models.MemberSetting", "Member")
                        .WithMany("FavoriteShopList")
                        .HasForeignKey("MemNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllShow.Models.ShopSetting", "Shop")
                        .WithMany("FavoriteShopList")
                        .HasForeignKey("ShNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("AllShow.Models.MemberList", b =>
                {
                    b.HasOne("AllShow.Models.MemberSetting", "Member")
                        .WithMany("MemberList")
                        .HasForeignKey("MemNo");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("AllShow.Models.OrderList", b =>
                {
                    b.HasOne("AllShow.Models.Product", "Product")
                        .WithMany("OrderList")
                        .HasForeignKey("ProNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllShow.Models.ShopOrder", "ShopOrder")
                        .WithMany("OrderList")
                        .HasForeignKey("ShoporderNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShopOrder");
                });

            modelBuilder.Entity("AllShow.Models.Product", b =>
                {
                    b.HasOne("AllShow.Models.ProductClass", "ProductClass")
                        .WithMany()
                        .HasForeignKey("ProClassNo");

                    b.HasOne("AllShow.Models.ShopSetting", "Shop")
                        .WithMany("Product")
                        .HasForeignKey("ShNo");

                    b.Navigation("ProductClass");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("AllShow.Models.ProductClass", b =>
                {
                    b.HasOne("AllShow.Models.ShClass", "ShClass")
                        .WithMany("ProductClass")
                        .HasForeignKey("ShClassNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShClass");
                });

            modelBuilder.Entity("AllShow.Models.ShClassList", b =>
                {
                    b.HasOne("AllShow.Models.ShClass", "ShClass")
                        .WithMany("ShClassList")
                        .HasForeignKey("ShClassNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AllShow.Models.ShopSetting", "Shop")
                        .WithMany("ShClassList")
                        .HasForeignKey("ShNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShClass");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("AllShow.Models.ShopOrder", b =>
                {
                    b.HasOne("AllShow.Models.MemberList", "MemberList")
                        .WithMany("ShopOrder")
                        .HasForeignKey("OrderNo");

                    b.HasOne("AllShow.Models.ShopSetting", "Shop")
                        .WithMany("ShopOrder")
                        .HasForeignKey("ShNo");

                    b.Navigation("MemberList");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("AllShow.Models.ShopSetting", b =>
                {
                    b.HasOne("AllShow.Models.EmployeeSetting", "Employee")
                        .WithMany("Shop")
                        .HasForeignKey("EmpNo");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AllShow.Models.AuthorityFunction", b =>
                {
                    b.Navigation("Authority");
                });

            modelBuilder.Entity("AllShow.Models.EmployeeSetting", b =>
                {
                    b.Navigation("Advertisement");

                    b.Navigation("Announcement");

                    b.Navigation("Authorities");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("AllShow.Models.MemberList", b =>
                {
                    b.Navigation("ShopOrder");
                });

            modelBuilder.Entity("AllShow.Models.MemberSetting", b =>
                {
                    b.Navigation("FavoriteShopList");

                    b.Navigation("MemberList");
                });

            modelBuilder.Entity("AllShow.Models.Product", b =>
                {
                    b.Navigation("OrderList");
                });

            modelBuilder.Entity("AllShow.Models.ShClass", b =>
                {
                    b.Navigation("ProductClass");

                    b.Navigation("ShClassList");
                });

            modelBuilder.Entity("AllShow.Models.ShopOrder", b =>
                {
                    b.Navigation("OrderList");
                });

            modelBuilder.Entity("AllShow.Models.ShopSetting", b =>
                {
                    b.Navigation("Advertisement");

                    b.Navigation("FavoriteShopList");

                    b.Navigation("Product");

                    b.Navigation("ShClassList");

                    b.Navigation("ShopOrder");
                });
#pragma warning restore 612, 618
        }
    }
}

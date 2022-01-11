using AllShow.Models;
using AllShow.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Data
{
    public class AllShowDBContext : DbContext
    {
        public AllShowDBContext(DbContextOptions<AllShowDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authority>().HasKey(m => new
            {
                m.EmpNo,
                m.AuthorityNo
            });
            modelBuilder.Entity<FavoriteShopList>().HasKey(m => new
            {
                m.ShNo,
                m.MemNo
            });
            modelBuilder.Entity<OrderList>().HasKey(m => new
            {
                m.ProNo,
                m.ShoporderNo
            });
            modelBuilder.Entity<ShClassList>().HasKey(m => new
            {
                m.ShClassNo,
                m.ShNo
            });
        }


        public DbSet<EmployeeSetting> EmployeeSetting { get; set; }
        public DbSet<Advertisement> Advertisement { get; set; }
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<AuthorityFunction> AuthorityFunction { get; set; }
        public DbSet<Authority> Authority { get; set; }
        public DbSet<FavoriteShopList> FavoriteShopList { get; set; }
        public DbSet<MemberList> MemberList { get; set; }
        public DbSet<MemberSetting> MemberSetting { get; set; }
        public DbSet<OrderList> OrderList { get; set; }
        public DbSet<ProductClass> ProductClass { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ShClass> ShClass { get; set; }
        public DbSet<ShopSetting> ShopSetting { get; set; }
        public DbSet<ShClassList> ShClassList { get; set; }
        public DbSet<ShopOrder> ShopOrder { get; set; }
        public DbSet<DbFiles> DbFiles { get; set; }       
    }
}

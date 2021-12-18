using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using prjAllShow.Backend.Data;
using prjAllShow.Backend.Models;
using prjAllShow.Backend.Models.Identity;

namespace prjAllShow.Backend.Seed
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //var defaultUser = new ApplicationUser();
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            string defaultPWD = "123456";
            //var hashedPassword = passwordHasher.HashPassword(defaultUser, defaultPWD);

            List<int> adminIds = new List<int>();
            List<EmployeeSetting> employees = new List<EmployeeSetting>();
            employees.Add(
                new EmployeeSetting
                {
                    EmpName = "吳king",
                    EmpEmail = "scott@gmail.com",                    
                    EmpTel = "0975123210",
                });
            adminIds.Add(1);

            List<int> factoryIds = new List<int>();
            List<ShopSetting> shops = new List<ShopSetting>();
            shops.Add(
                new ShopSetting 
                {
                    ShContact = "Perter",
                    ShTel = "02-2222222",
                    ShEmail = "shop1@gmail.com",
                });
            factoryIds.Add(2);

            List<int> customerIds = new List<int>();
            List<MemberSetting> members = new List<MemberSetting>();
            members.Add(
                new MemberSetting
                {
                    MemTel = "0974123622",
                    MemEmail = "mem1@gmail.com",
                    MemName = "史考特老虎",                   
                });
            customerIds.Add(3);

            //https://stackoverflow.com/questions/49971308/seed-database-user-and-role-table-in-asp-net-core-2
            using (var context = new IdentityDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<IdentityDBContext>>()))
            {
                #region Roles
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(
                         new ApplicationRole
                         {
                             //Id = 1,
                             Description = "This Is Admin",
                             Name = "Admin",
                             NormalizedName = "ADMIN"

                         },
                         new ApplicationRole
                         {
                             //Id = 2,
                             Description = "This Is Factory",
                             Name = "Factory",
                             NormalizedName = "FACTORY"

                         },
                         new ApplicationRole
                         {
                             //Id = 3,
                             Description = "This Is Customer",
                             Name = "Customer",
                             NormalizedName = "CUSTOMER"

                         });

                    context.SaveChanges();
                }
                #endregion
                #region Users
                if (!context.Users.Any())
                {
                    foreach (var item in employees)
                    {
                        ApplicationUser user = new ApplicationUser
                        {
                            PhoneNumber = item.EmpTel,
                            UserName = item.EmpName,
                            NormalizedUserName = item.EmpName.ToUpper(),
                            Email = item.EmpEmail,
                            NormalizedEmail = item.EmpEmail.ToUpper(),
                            //PasswordHash = hashedPassword,
                            LockoutEnabled = true,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            IsAdmin = true
                        };
                        var hashedPassword = passwordHasher.HashPassword(user, defaultPWD);
                        user.PasswordHash = hashedPassword;

                        context.Users.Add(user);
                        context.SaveChanges();
                    }

                    foreach (var item in shops)
                    {
                        ApplicationUser user = new ApplicationUser
                        {
                            PhoneNumber = item.ShTel,
                            UserName = item.ShContact,
                            NormalizedUserName = item.ShContact.ToUpper(),
                            Email = item.ShEmail,
                            NormalizedEmail = item.ShEmail.ToUpper(),
                            //PasswordHash = hashedPassword,
                            LockoutEnabled = true,
                            SecurityStamp = Guid.NewGuid().ToString()
                        };
                        var hashedPassword = passwordHasher.HashPassword(user, defaultPWD);
                        user.PasswordHash = hashedPassword;

                        context.Users.Add(user);
                        context.SaveChanges();
                    }

                    foreach (var item in members)
                    {
                        ApplicationUser user = new ApplicationUser
                        {
                            PhoneNumber = item.MemTel,
                            UserName = item.MemName,
                            NormalizedUserName = item.MemName.ToUpper(),
                            Email = item.MemEmail,
                            NormalizedEmail = item.MemEmail.ToUpper(),
                            //PasswordHash = hashedPassword,
                            LockoutEnabled = true,
                            SecurityStamp = Guid.NewGuid().ToString()
                        };
                        var hashedPassword = passwordHasher.HashPassword(user, defaultPWD);
                        user.PasswordHash = hashedPassword;

                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
                #endregion
                #region UserRoles
                if (!context.UserRoles.Any())
                {
                    foreach (var Id in adminIds)
                    {
                        IdentityUserRole<int> ur = new IdentityUserRole<int>
                        {
                            RoleId = 1,
                            UserId = Id
                        };

                        context.UserRoles.Add(ur);
                        context.SaveChanges();
                    }

                    foreach (var Id in factoryIds)
                    {
                        IdentityUserRole<int> ur = new IdentityUserRole<int>
                        {
                            RoleId = 2,
                            UserId = Id
                        };

                        context.UserRoles.Add(ur);
                        context.SaveChanges();
                    }

                    foreach (var Id in customerIds)
                    {
                        IdentityUserRole<int> ur = new IdentityUserRole<int>
                        {
                            RoleId = 3,
                            UserId = Id
                        };

                        context.UserRoles.Add(ur);
                        context.SaveChanges();
                    }
                }
                #endregion
            }
        }
    }
}

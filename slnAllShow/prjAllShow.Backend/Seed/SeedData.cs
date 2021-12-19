using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            //List<int> adminIds = new List<int>();
            List<EmployeeSetting> employees = new List<EmployeeSetting>();
            //List<int> factoryIds = new List<int>();
            List<ShopSetting> shops = new List<ShopSetting>();
            //List<int> customerIds = new List<int>();
            List<MemberSetting> members = new List<MemberSetting>();

            int appUserId = 1;
            Dictionary<int, int> appUserDict = new Dictionary<int, int>();//Key：角色，Value：appUserId++ => 存進UserRoles

            var defaultUser = new ApplicationUser();
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            string defaultPWD = "1qaz@WSX3edc";
            var hashedPassword = passwordHasher.HashPassword(defaultUser, defaultPWD);

            using (var context = new AllShowDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AllShowDBContext>>()))
            {
                #region AllShowDB
                #region AuthorityFunction
                if (!context.AuthorityFunction.Any())
                {
                    context.AuthorityFunction.AddRange(
                        new AuthorityFunction
                        {
                            //AuthorityNo = 1,
                            AuthorityName = "權限管理"
                        },
                        new AuthorityFunction
                        {
                            //AuthorityNo = 2,
                            AuthorityName = "人事管理"
                        }, new AuthorityFunction
                        {
                            //AuthorityNo = 3,
                            AuthorityName = "商店管理"
                        }, new AuthorityFunction
                        {
                            //AuthorityNo = 4,
                            AuthorityName = "會員管理"
                        }, new AuthorityFunction
                        {
                            //AuthorityNo = 5,
                            AuthorityName = "交易管理"
                        }, new AuthorityFunction
                        {
                            //AuthorityNo = 6,
                            AuthorityName = "公告管理"
                        }, new AuthorityFunction
                        {
                            //AuthorityNo = 7,
                            AuthorityName = "分類管理"
                        }
                    );
                    context.SaveChanges();
                }
                #endregion
                #region Employee
                if (!context.EmployeeSetting.Any())
                {
                    context.EmployeeSetting.AddRange(
                        new EmployeeSetting
                        {
                            //EmpNo = 1,
                            EmpName = "系統管理者",
                            EmpAccount = "allshow@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "allshow@gmail.com",
                            EmpSex = "1",
                            EmpBirth = new DateTime(1987, 2, 10),
                            EmpTel = "0975123210",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        },
                        new EmployeeSetting
                        {
                            //EmpNo = 2,
                            EmpName = "吳king",
                            EmpAccount = "scott@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "scott@gmail.com",
                            EmpSex = "1",
                            EmpBirth = new DateTime(1970, 10, 28),
                            EmpTel = "0975123210",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        },
                        new EmployeeSetting
                        {
                            //EmpNo = 3,
                            EmpName = "湯O睿",
                            EmpAccount = "emp2@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "emp2@gmail.com",
                            EmpSex = "1",
                            EmpBirth = new DateTime(1986, 1, 1),
                            EmpTel = "0975123211",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        },
                        new EmployeeSetting
                        {
                            //EmpNo = 4,
                            EmpName = "鍾O偉",
                            EmpAccount = "emp3@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "emp3@gmail.com",
                            EmpSex = "1",
                            EmpBirth = new DateTime(1987, 1, 1),
                            EmpTel = "0975123212",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        },
                        new EmployeeSetting
                        {
                            //EmpNo = 5,
                            EmpName = "李O瑄",
                            EmpAccount = "emp4@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "emp4@gmail.com",
                            EmpSex = "0",
                            EmpBirth = new DateTime(1988, 1, 1),
                            EmpTel = "0975123213",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        },
                        new EmployeeSetting
                        {
                            //EmpNo = 6,
                            EmpName = "張O銘",
                            EmpAccount = "emp5@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "emp5@gmail.com",
                            EmpSex = "1",
                            EmpBirth = new DateTime(1989, 1, 1),
                            EmpTel = "0975123214",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        },
                        new EmployeeSetting
                        {
                            //EmpNo = 7,
                            EmpName = "蕭O凱",
                            EmpAccount = "emp6@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "emp6@gmail.com",
                            EmpSex = "1",
                            EmpBirth = new DateTime(1990, 1, 1),
                            EmpTel = "0975123215",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        },
                        new EmployeeSetting
                        {
                            //EmpNo = 8,
                            EmpName = "吳O諺",
                            EmpAccount = "emp7@gmail.com",
                            EmpPwd = hashedPassword,
                            EmpEmail = "emp7@gmail.com",
                            EmpSex = "1",
                            EmpBirth = new DateTime(1991, 1, 1),
                            EmpTel = "0975123216",
                            HireDate = new DateTime(2021, 11, 30),
                            EmpAccountState = "1"
                        });
                    context.SaveChanges();
                }
                employees = context.EmployeeSetting.ToList();
                for (int i = 0; i < employees.Count(); i++)
                {
                    appUserDict.Add(appUserId, 1);
                    appUserId++;
                }
                //adminIds = employees.Select(x => x.Id).ToList();                
                #endregion
                #region Authority
                if (!context.Authority.Any())
                {
                    context.Authority.AddRange(
                        new Authority
                        {
                            EmpNo = 1,
                            AuthorityNo = 1,
                            Note = ""
                        },
                        new Authority
                        {
                            EmpNo = 2,
                            AuthorityNo = 2,
                            Note = ""
                        },
                        new Authority
                        {
                            EmpNo = 3,
                            AuthorityNo = 3,
                            Note = ""
                        },
                        new Authority
                        {
                            EmpNo = 4,
                            AuthorityNo = 4,
                            Note = ""
                        },
                        new Authority
                        {
                            EmpNo = 5,
                            AuthorityNo = 5,
                            Note = ""
                        }
                    );
                    context.SaveChanges();
                }
                #endregion
                #region Announcement
                if (!context.Announcement.Any())
                {
                    context.Announcement.AddRange(
                        new Announcement
                        {
                            //AnnouncementNo = 1,
                            EmpNo = 1,
                            AnnouncementType = "h1",
                            AnnouncementContent = "hello1",
                            CreateDate = new DateTime(),
                            UpdateDate = new DateTime(),
                            StartDate = new DateTime(),
                            EndDate = new DateTime().AddDays(100)
                        },
                        new Announcement
                        {
                            //AnnouncementNo = 2,
                            EmpNo = 2,
                            AnnouncementType = "h2",
                            AnnouncementContent = "hello2",
                            CreateDate = new DateTime(),
                            UpdateDate = new DateTime(),
                            StartDate = new DateTime(),
                            EndDate = new DateTime().AddDays(100)
                        },
                        new Announcement
                        {
                            //AnnouncementNo = 3,
                            EmpNo = 3,
                            AnnouncementType = "h3",
                            AnnouncementContent = "hello3",
                            CreateDate = new DateTime(),
                            UpdateDate = new DateTime(),
                            StartDate = new DateTime(),
                            EndDate = new DateTime().AddDays(100)
                        },
                        new Announcement
                        {
                            //AnnouncementNo = 4,
                            EmpNo = 4,
                            AnnouncementType = "h4",
                            AnnouncementContent = "hello4",
                            CreateDate = new DateTime(),
                            UpdateDate = new DateTime(),
                            StartDate = new DateTime(),
                            EndDate = new DateTime().AddDays(100)
                        },
                        new Announcement
                        {
                            //AnnouncementNo = 5,
                            EmpNo = 5,
                            AnnouncementType = "h5",
                            AnnouncementContent = "hello5",
                            CreateDate = new DateTime(),
                            UpdateDate = new DateTime(),
                            StartDate = new DateTime(),
                            EndDate = new DateTime().AddDays(100)
                        }
                    );
                    context.SaveChanges();
                }
                #endregion
                #region ShClass
                if (!context.ShClass.Any())
                {
                    context.ShClass.AddRange(
                        new ShClass
                        {
                            //ShClassNo = 1,
                            ShClassName = "服飾館"
                        },
                        new ShClass
                        {
                            //ShClassNo = 2,
                            ShClassName = "3C館"
                        },
                        new ShClass
                        {
                            //ShClassNo = 3,
                            ShClassName = "美食館"
                        },
                        new ShClass
                        {
                            //ShClassNo = 4,
                            ShClassName = "鞋子館"
                        },
                        new ShClass
                        {
                            //ShClassNo = 5,
                            ShClassName = "書店館"
                        },
                        new ShClass
                        {
                            //ShClassNo = 6,
                            ShClassName = "其他"
                        }
                    );
                    context.SaveChanges();
                }
                #endregion
                #region Shop
                if (!context.ShopSetting.Any())
                {
                    context.ShopSetting.AddRange(
                        new ShopSetting
                        {
                            //ShNo = 1,
                            EmpNo = 1,
                            ShName = "Peter的寶庫",
                            ShAccount = "shop1@gmail.com",
                            ShPwd = hashedPassword,
                            ShBoss = "Perter",
                            ShContact = "Perter",
                            ShAddress = "臺北市信義區菸廠路93號",
                            ShTel = "02-2222222",
                            ShEmail = "shop1@gmail.com",
                            ShAbout = "書中自有黃金屋",
                            ShAdState = "0",
                            ShPopShop = "1",
                            ShCheckState = "1",
                            ShPwdState = "0",
                            ShStartDate = DateTime.Now,
                            ShEndDate = DateTime.Now.AddYears(1),
                            ShCheckDate = DateTime.Now,
                            ShThePic = "",
                            ShLogoPic = "",
                            ShUrl = "",
                            ShAdTitle = "",
                            ShAdPic = "",
                        },
                        new ShopSetting
                        {
                            //ShNo = 2,
                            EmpNo = 1,
                            ShName = "誠品書店",
                            ShAccount = "shop2@gmail.com",
                            ShPwd = hashedPassword,
                            ShBoss = "吳清友",
                            ShContact = "吳清友",
                            ShAddress = "臺北市信義區菸廠路88號",
                            ShTel = "02-2222222",
                            ShEmail = "shop2@gmail.com",
                            ShAbout = "以獨特的「誠品經驗」，開創出獨具人文創意且具執行績效的策略發展，帶來更為精緻、多元、更具國際視野的文化生活，成為華人市場上最具影響力的文創品牌。所有的誠品人將懷著無限的感激與自我期許。每當午夜夢迴我赫然發現，其實是這片土地，是這裏的人們，孕育滋養了誠品，誠品是這個社會時空環境下的集體創作。",
                            ShAdState = "0",
                            ShPopShop = "1",
                            ShCheckState = "1",
                            ShPwdState = "0",
                            ShStartDate = DateTime.Now,
                            ShEndDate = DateTime.Now.AddYears(1),
                            ShCheckDate = DateTime.Now,
                            ShThePic = "",
                            ShLogoPic = "",
                            ShUrl = "",
                            ShAdTitle = "",
                            ShAdPic = "",
                        },
                        new ShopSetting
                        {
                            //ShNo = 3,
                            EmpNo = 1,
                            ShName = "金石堂",
                            ShAccount = "shop3@gmail.com",
                            ShPwd = hashedPassword,
                            ShBoss = "周傳芳",
                            ShContact = "周傳芳",
                            ShAddress = "臺北市信義區菸廠路87號",
                            ShTel = "02-2222222",
                            ShEmail = "shop3@gmail.com",
                            ShAbout = "金石堂以「精誠所至，金石為開」的企業精神自我期許。金石堂的經營團隊與通路營運的寶貴經驗，專長於建立完善之進銷退存管理，並以顧客需求滿足為宗旨，除提供豐沛藏書外，尚提供世界各品牌的專業文具及禮品，讓最懂品味的愛書人擁有深度及廣度的知性生活，成為台灣最具特色之書店風景。",
                            ShAdState = "0",
                            ShPopShop = "1",
                            ShCheckState = "1",
                            ShPwdState = "0",
                            ShStartDate = DateTime.Now,
                            ShEndDate = DateTime.Now.AddYears(1),
                            ShCheckDate = DateTime.Now,
                            ShThePic = "",
                            ShLogoPic = "",
                            ShUrl = "",
                            ShAdTitle = "",
                            ShAdPic = "",
                        },
                        new ShopSetting
                        {
                            //ShNo = 4,
                            EmpNo = 1,
                            ShName = "今日書局",
                            ShAccount = "shop4@gmail.com",
                            ShPwd = hashedPassword,
                            ShBoss = "王小明",
                            ShContact = "王小明",
                            ShAddress = "臺北市信義區菸廠路90號",
                            ShTel = "02-2222222",
                            ShEmail = "shop4@gmail.com",
                            ShAbout = "實體店面的時代已經慢慢過時,現在轉攻網路市場",
                            ShAdState = "0",
                            ShPopShop = "1",
                            ShCheckState = "1",
                            ShPwdState = "0",
                            ShStartDate = DateTime.Now,
                            ShEndDate = DateTime.Now.AddYears(1),
                            ShCheckDate = DateTime.Now,
                            ShThePic = "",
                            ShLogoPic = "",
                            ShUrl = "",
                            ShAdTitle = "",
                            ShAdPic = "",
                        }
                    );
                    context.SaveChanges();
                }
                shops = context.ShopSetting.ToList();
                for (int i = 0; i < shops.Count(); i++)
                {
                    appUserDict.Add(appUserId, 2);
                    appUserId++;
                }
                //factoryIds = shops.Select(sh => sh.Id).ToList();
                #endregion
                #region ShClassList
                if (!context.ShClassList.Any())
                {
                    context.ShClassList.AddRange(
                        new ShClassList
                        {
                            ShClassNo = 5,
                            ShNo = 1,
                            Note = ""
                        });
                    context.SaveChanges();
                }
                #endregion
                #region Advertisement
                if (!context.Advertisement.Any())
                {
                    context.Advertisement.AddRange(
                        new Advertisement
                        {
                            //AdNo = 1,
                            ShNo = 1,
                            EmpNo = 1,
                            AdTitle = "helloworld1",
                            AdApplyDate = DateTime.Now,
                            AdStartDate = DateTime.Now,
                            AdTime = DateTime.Now.AddDays(30),
                            AdPrice = 6000,
                            AdPic = "",
                            AdURL = "",
                            AdCheckState = "1",
                        },
                        new Advertisement
                        {
                            //AdNo = 2,
                            ShNo = 1,
                            EmpNo = 1,
                            AdTitle = "helloworld2",
                            AdApplyDate = DateTime.Now,
                            AdStartDate = DateTime.Now,
                            AdTime = DateTime.Now.AddDays(30),
                            AdPrice = 7000,
                            AdPic = "",
                            AdURL = "",
                            AdCheckState = "1",
                        },
                        new Advertisement
                        {
                            //AdNo = 3,
                            ShNo = 1,
                            EmpNo = 1,
                            AdTitle = "helloworld3",
                            AdApplyDate = DateTime.Now,
                            AdStartDate = DateTime.Now,
                            AdTime = DateTime.Now.AddDays(30),
                            AdPrice = 8000,
                            AdPic = "",
                            AdURL = "",
                            AdCheckState = "1",
                        }
                    );
                    context.SaveChanges();
                }
                #endregion
                #region ProductClass
                if (!context.ProductClass.Any())
                {
                    context.ProductClass.AddRange(
                        new ProductClass
                        {
                            //ProClassNo = 1,
                            ShClassNo = 5,
                            ProClassName = "工具書"
                        }
                    );
                    context.SaveChanges();
                }
                #endregion
                #region Product
                if (!context.Product.Any())
                {
                    context.Product.AddRange(
                        new Product
                        {
                            //ProNo = 1,
                            ShNo = 1,
                            ProClassNo = 1,
                            ProName = "JAVA是簡單的",
                            ProPrice = 500,
                            ProStatement = "JAVA是簡單的，萬般皆物件，要什麼給什麼",
                            ProState = "0",
                            ProPic1 = "",
                            ProPic2 = "",
                            ProPic3 = "",
                            ProCreateDate = DateTime.Now,
                            ProUpdateDate = DateTime.Now,
                            ProOffshelfDate = DateTime.Now.AddYears(1),
                            ProPop = "0"
                        },
                        new Product
                        {
                            //ProNo = 2,
                            ShNo = 1,
                            ProClassNo = 1,
                            ProName = "淺談Socket小姐",
                            ProPrice = 9999,
                            ProStatement = "Socket小姐一次只能接待一個客人",
                            ProState = "0",
                            ProPic1 = "",
                            ProPic2 = "",
                            ProPic3 = "",
                            ProCreateDate = DateTime.Now,
                            ProUpdateDate = DateTime.Now,
                            ProOffshelfDate = DateTime.Now.AddYears(1),
                            ProPop = "0"
                        },
                        new Product
                        {
                            //ProNo = 3,
                            ShNo = 1,
                            ProClassNo = 1,
                            ProName = "看故事學JAVA的API",
                            ProPrice = 600,
                            ProStatement = "API裡面每個類別都有一段可歌可泣的故事",
                            ProState = "0",
                            ProPic1 = "",
                            ProPic2 = "",
                            ProPic3 = "",
                            ProCreateDate = DateTime.Now,
                            ProUpdateDate = DateTime.Now,
                            ProOffshelfDate = DateTime.Now.AddYears(1),
                            ProPop = "0"
                        }
                    );
                    context.SaveChanges();
                }
                #endregion
                #region Member
                if (!context.MemberSetting.Any())
                {
                    context.MemberSetting.AddRange(
                        new MemberSetting
                        {
                            //MemNo = 1,
                            MemEmail = "mem1@gmail.com",
                            MemPwd = hashedPassword,
                            MemDiminutive = "史考特老虎",
                            MemName = "史考特老虎",
                            MemSex = "1",
                            MemBirth = new DateTime(1965, 2, 2),
                            MemTel = "0974123622",
                            MemAddress = "中壢市中央街16號1樓",
                            MemPic = "",
                            MemAccountState = "1",
                            MemCheckNumber = "AAA",
                            MemCreateDate = DateTime.Now,
                            MemUpdateDate = DateTime.Now
                        },
                        new MemberSetting
                        {
                            //MemNo = 2,
                            MemEmail = "mem2@gmail.com",
                            MemPwd = hashedPassword,
                            MemDiminutive = "小皇帝",
                            MemName = "小皇帝",
                            MemSex = "1",
                            MemBirth = new DateTime(1987, 3, 3),
                            MemTel = "0974123633",
                            MemAddress = "中壢市中央街16號2樓",
                            MemPic = "",
                            MemAccountState = "1",
                            MemCheckNumber = "AAA",
                            MemCreateDate = DateTime.Now,
                            MemUpdateDate = DateTime.Now
                        },
                        new MemberSetting
                        {
                            //MemNo = 3,
                            MemEmail = "mem3@gmail.com",
                            MemPwd = hashedPassword,
                            MemDiminutive = "圓仔",
                            MemName = "圓仔",
                            MemSex = "1",
                            MemBirth = new DateTime(1996, 4, 5),
                            MemTel = "0974123645",
                            MemAddress = "中壢市中央街16號3樓",
                            MemPic = "",
                            MemAccountState = "1",
                            MemCheckNumber = "AAA",
                            MemCreateDate = DateTime.Now,
                            MemUpdateDate = DateTime.Now
                        }
                    );
                    context.SaveChanges();
                }
                members = context.MemberSetting.ToList();
                for (int i = 0; i < members.Count(); i++)
                {
                    appUserDict.Add(appUserId, 3);
                    appUserId++;
                }
                //customerIds = members.Select(m => m.Id).ToList();                
                #endregion
                #endregion
            }

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
                            PasswordHash = hashedPassword,
                            LockoutEnabled = true,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            IsAdmin = true
                        };
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
                            PasswordHash = hashedPassword,
                            LockoutEnabled = true,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            IsAdmin = false
                        };
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
                            PasswordHash = hashedPassword,
                            LockoutEnabled = true,
                            SecurityStamp = Guid.NewGuid().ToString(),
                            IsAdmin = false
                        };
                        context.Users.Add(user);
                        context.SaveChanges();
                    }
                }
                #endregion
                #region UserRoles
                if (!context.UserRoles.Any())
                {
                    foreach (KeyValuePair<int, int> item in appUserDict)
                    {
                        IdentityUserRole<int> ur = new IdentityUserRole<int>
                        {
                            RoleId = item.Value,
                            UserId = item.Key,
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

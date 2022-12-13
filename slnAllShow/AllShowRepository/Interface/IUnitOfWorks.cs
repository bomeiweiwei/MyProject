using AllShow.Models;
using AllShow.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow.Interface
{
    public interface IUnitOfWorks
    {
        IGenericRepository<EmployeeSetting> EmployeeRepository { get; }
        IGenericRepository<ShClass> ShClassRepository { get; }
        IGenericRepository<ShClassList> ShClassListRepository { get; }
        IGenericRepository<ShopSetting> ShopSettingRepository { get; }
        IGenericRepository<ProductClass> ProductClassRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }

        void SaveChanges();
    }
}

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

        void SaveChanges();
    }
}

using AllShow.Models;
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

        void SaveChanges();
    }
}

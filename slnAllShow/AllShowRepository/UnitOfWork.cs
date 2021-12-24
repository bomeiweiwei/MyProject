using AllShow.Data;
using AllShow.Interface;
using AllShow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShow
{
    public class UnitOfWork : IUnitOfWorks
    {
        private AllShowDBContext _context;
        private GenericRepository<EmployeeSetting> _employeeRepository;

        public UnitOfWork(AllShowDBContext context)
        {
            _context = context;
        }

        public IGenericRepository<EmployeeSetting> EmployeeRepository
        {
            get
            {
                if (this._employeeRepository == null)
                {
                    this._employeeRepository = new GenericRepository<EmployeeSetting>(_context);
                }
                return _employeeRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

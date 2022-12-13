using AllShow.Data;
using AllShow.Interface;
using AllShow.Models;
using AllShow.Models.Identity;
//using AllShowRepository;
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
        private GenericRepository<ShClass> _ShClassRepository;
        private GenericRepository<ShClassList> _ShClassListRepository;
        private GenericRepository<ShopSetting> _ShopSettingRepository;
        private GenericRepository<ProductClass> _ProductClassRepository;
        private GenericRepository<Product> _ProductRepository;

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

        public IGenericRepository<ShClass> ShClassRepository
        {
            get
            {
                if (this._ShClassRepository == null)
                {
                    this._ShClassRepository = new GenericRepository<ShClass>(_context);
                }
                return _ShClassRepository;
            }
        }

        public IGenericRepository<ShClassList> ShClassListRepository
        {
            get
            {
                if (this._ShClassListRepository == null)
                {
                    this._ShClassListRepository = new GenericRepository<ShClassList>(_context);
                }
                return _ShClassListRepository;
            }
        }

        public IGenericRepository<ShopSetting> ShopSettingRepository
        {
            get
            {
                if (this._ShopSettingRepository == null)
                {
                    this._ShopSettingRepository = new GenericRepository<ShopSetting>(_context);
                }
                return _ShopSettingRepository;
            }
        }

        public IGenericRepository<ProductClass> ProductClassRepository
        {
            get
            {
                if (this._ProductClassRepository == null)
                {
                    this._ProductClassRepository = new GenericRepository<ProductClass>(_context);
                }
                return _ProductClassRepository;
            }
        }

        public IGenericRepository<Product> ProductRepository
        {
            get
            {
                if (this._ProductRepository == null)
                {
                    this._ProductRepository = new GenericRepository<Product>(_context);
                }
                return _ProductRepository;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

using AllShow.Interface;
using AllShow.Models;
using AllShowDTO;
using AllShowService.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService
{
    public class ShopService : IShopService
    {
        private readonly IUnitOfWorks _unitOfWork;
        private readonly IApplicationUserService _ApplicationUserService;
        private readonly IMapper _mapper;

        public ShopService(IUnitOfWorks unitOfWork, IApplicationUserService ApplicationUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _ApplicationUserService = ApplicationUserService;
            _mapper = mapper;
        }
        /// <summary>
        /// 新增商店
        /// </summary>
        /// <param name="model"></param>
        public void CreateShop(ShopSettingDTO model)
        {
            var entity = _mapper.Map<ShopSetting>(model);
            _unitOfWork.ShopSettingRepository.Insert(entity);
            _unitOfWork.SaveChanges();
        }
        /// <summary>
        /// 修改商店
        /// </summary>
        /// <param name="model"></param>
        public void UpdateShop(ShopSettingDTO model)
        {
            var entity = _mapper.Map<ShopSetting>(model);
            _unitOfWork.ShopSettingRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }
        /// <summary>
        /// 刪除商店
        /// </summary>
        /// <param name="id"></param>
        public void DeleteShop(int id)
        {
            var entity = _unitOfWork.ShopSettingRepository.Get(item => item.Id == id).FirstOrDefault();
            if (entity != null)
            {
                _unitOfWork.ShopSettingRepository.Delete(entity);
                _unitOfWork.SaveChanges();
            }
        }
        /// <summary>
        /// 取得所有商店
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ShopSettingDTO> GetShops()
        {            
            var identityUser = _ApplicationUserService.GetAll().Where(m => m.Role == "Factory").ToList();
            //有商店類別分類
            /*
            var query1 = from main_item in _unitOfWork.ShClassListRepository.Get()
                         join shclass in _unitOfWork.ShClassRepository.Get() on main_item.ShClassNo equals shclass.Id
                         join shop in _unitOfWork.ShopSettingRepository.Get() on main_item.ShNo equals shop.Id
                         join emp in _unitOfWork.EmployeeRepository.Get() on shop.EmpNo equals emp.Id
                         join ifactory in identityUser on shop.ShAccount equals ifactory.Email
                         select new ShopSettingDTO
                         {
                             Id = shop.Id,
                             EmpNo = shop.EmpNo,
                             ShThePic = shop.ShThePic,
                             ShName = shop.ShName,
                             ShAccount = shop.ShAccount,
                             ShPwd = shop.ShPwd,
                             ShBoss = shop.ShBoss,
                             ShContact = shop.ShContact,
                             ShAddress = shop.ShAddress,
                             ShTel = shop.ShTel,
                             ShEmail = shop.ShEmail,
                             ShAbout = shop.ShAbout,
                             ShLogoPic = shop.ShLogoPic,
                             ShUrl = shop.ShUrl,
                             ShAdState = shop.ShAdState,
                             ShAdTitle = shop.ShAdTitle,
                             ShAdPic = shop.ShAdPic,
                             ShPopShop = shop.ShPopShop,
                             ShCheckState = shop.ShCheckState,
                             ShStartDate = shop.ShStartDate,
                             ShEndDate = shop.ShEndDate,
                             ShCheckDate = shop.ShCheckDate,
                             ShPwdState = shop.ShPwdState,
                             ShStopRightStartDate = shop.ShStopRightStartDate,
                             ShStopRightEnddate = shop.ShStopRightEnddate,
                             EmpName = emp.EmpName
                         };
            */
            //
            var query2 = from shop in _unitOfWork.ShopSettingRepository.Get()
                         join emp in _unitOfWork.EmployeeRepository.Get() on shop.EmpNo equals emp.Id
                         join ifactory in identityUser on shop.ShAccount equals ifactory.Email
                         //where !query1.Select(x => x.Id).Contains(shop.Id)
                         select new ShopSettingDTO
                         {
                             Id = shop.Id,
                             EmpNo = shop.EmpNo,
                             ShThePic = shop.ShThePic,
                             ShName = shop.ShName,
                             ShAccount = shop.ShAccount,
                             ShPwd = shop.ShPwd,
                             ShBoss = shop.ShBoss,
                             ShContact = shop.ShContact,
                             ShAddress = shop.ShAddress,
                             ShTel = shop.ShTel,
                             ShEmail = shop.ShEmail,
                             ShAbout = shop.ShAbout,
                             ShLogoPic = shop.ShLogoPic,
                             ShUrl = shop.ShUrl,
                             ShAdState = shop.ShAdState,
                             ShAdTitle = shop.ShAdTitle,
                             ShAdPic = shop.ShAdPic,
                             ShPopShop = shop.ShPopShop,
                             ShCheckState = shop.ShCheckState,
                             ShStartDate = shop.ShStartDate,
                             ShEndDate = shop.ShEndDate,
                             ShCheckDate = shop.ShCheckDate,
                             ShPwdState = shop.ShPwdState,
                             ShStopRightStartDate = shop.ShStopRightStartDate,
                             ShStopRightEnddate = shop.ShStopRightEnddate,
                             EmpName = emp.EmpName
                         };
            //var result = query1.Union(query2);
            var result = query2;
            return result;
        }
        /// <summary>
        /// 取得某類別的商店
        /// </summary>
        /// <param name="shclassno"></param>
        /// <returns></returns>
        public List<ShopSettingDTO> GetShopsByClass(int shclassno)
        {
            //廠商腳色
            var identityUser = _ApplicationUserService.GetAll().Where(m => m.Role == "Factory").ToList();
            var query = (from main_item in _unitOfWork.ShClassListRepository.Get()
                         join shclass in _unitOfWork.ShClassRepository.Get() on main_item.ShClassNo equals shclass.Id
                         join shop in _unitOfWork.ShopSettingRepository.Get() on main_item.ShNo equals shop.Id
                         join emp in _unitOfWork.EmployeeRepository.Get() on shop.EmpNo equals emp.Id
                         join ifactory in identityUser on shop.ShAccount equals ifactory.Email
                         where shclass.Id == shclassno
                         select new ShopSettingDTO
                         {
                             Id = shop.Id,
                             EmpNo = shop.EmpNo,
                             ShThePic = shop.ShThePic,
                             ShName = shop.ShName,
                             ShAccount = shop.ShAccount,
                             ShPwd = shop.ShPwd,
                             ShBoss = shop.ShBoss,
                             ShContact = shop.ShContact,
                             ShAddress = shop.ShAddress,
                             ShTel = shop.ShTel,
                             ShEmail = shop.ShEmail,
                             ShAbout = shop.ShAbout,
                             ShLogoPic = shop.ShLogoPic,
                             ShUrl = shop.ShUrl,
                             ShAdState = shop.ShAdState,
                             ShAdTitle = shop.ShAdTitle,
                             ShAdPic = shop.ShAdPic,
                             ShPopShop = shop.ShPopShop,
                             ShCheckState = shop.ShCheckState,
                             ShStartDate = shop.ShStartDate,
                             ShEndDate = shop.ShEndDate,
                             ShCheckDate = shop.ShCheckDate,
                             ShPwdState = shop.ShPwdState,
                             ShStopRightStartDate = shop.ShStopRightStartDate,
                             ShStopRightEnddate = shop.ShStopRightEnddate,
                             EmpName = emp.EmpName,
                             ShClassName = shclass.ShClassName
                         }).ToList();
            return query;
        }
        /// <summary>
        /// 取得某商店By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ShopSettingDTO GetShopById(int id)
        {
            //廠商腳色
            var identityUser = _ApplicationUserService.GetAll().Where(m => m.Role == "Factory").ToList();

            var query = (from main_item in _unitOfWork.ShClassListRepository.Get()
                         join shclass in _unitOfWork.ShClassRepository.Get() on main_item.ShClassNo equals shclass.Id
                         join shop in _unitOfWork.ShopSettingRepository.Get() on main_item.ShNo equals shop.Id
                         join emp in _unitOfWork.EmployeeRepository.Get() on shop.EmpNo equals emp.Id
                         join ifactory in identityUser on shop.ShAccount equals ifactory.Email
                         where shop.Id == id
                         select new ShopSettingDTO
                         {
                             Id = shop.Id,
                             EmpNo = shop.EmpNo,
                             ShThePic = shop.ShThePic,
                             ShName = shop.ShName,
                             ShAccount = shop.ShAccount,
                             ShPwd = shop.ShPwd,
                             ShBoss = shop.ShBoss,
                             ShContact = shop.ShContact,
                             ShAddress = shop.ShAddress,
                             ShTel = shop.ShTel,
                             ShEmail = shop.ShEmail,
                             ShAbout = shop.ShAbout,
                             ShLogoPic = shop.ShLogoPic,
                             ShUrl = shop.ShUrl,
                             ShAdState = shop.ShAdState,
                             ShAdTitle = shop.ShAdTitle,
                             ShAdPic = shop.ShAdPic,
                             ShPopShop = shop.ShPopShop,
                             ShCheckState = shop.ShCheckState,
                             ShStartDate = shop.ShStartDate,
                             ShEndDate = shop.ShEndDate,
                             ShCheckDate = shop.ShCheckDate,
                             ShPwdState = shop.ShPwdState,
                             ShStopRightStartDate = shop.ShStopRightStartDate,
                             ShStopRightEnddate = shop.ShStopRightEnddate,
                             EmpName = emp.EmpName,
                             ShClassName = shclass.ShClassName
                         }).ToList();
            ShopSettingDTO targetShop = query.FirstOrDefault();
            targetShop.ShClassName = string.Join(",", query.Select(m => m.ShClassName));
            return targetShop;
        }
        /// <summary>
        /// 取得商店By 分頁
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<ShopSettingDTO> GetShopsByPage(int pageIndex, int pageSize)
        {            
            var result = GetShops().Skip(pageIndex * pageSize).Take(pageSize).ToList();
            return result;
        }
    }
}

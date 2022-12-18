using AllShow.Interface;
using AllShow.Models;
using AllShowService.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService
{
    public class ShClassService: IShClassService
    {
        private readonly IUnitOfWorks _unitOfWork;
        private readonly IMapper _mapper;

        public ShClassService(IUnitOfWorks unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<ShClass> GetShClass()
        {
            var list = _unitOfWork.ShClassRepository.Get().ToList();
            return _mapper.Map<List<ShClass>>(list);
        }

        public ShClass GetShClassId(int id)
        {
            var entity = _unitOfWork.ShClassRepository.Get(item => item.Id == id, item => item.OrderByDescending(m => m.Id)).FirstOrDefault();
            return _mapper.Map<ShClass>(entity);
        }


        public void CreateShClass(ShClass model)
        {
            var entity = _mapper.Map<ShClass>(model);
            _unitOfWork.ShClassRepository.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateShClass(ShClass model)
        {
            var entity = _mapper.Map<ShClass>(model);
            _unitOfWork.ShClassRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public void DeleteShClass(int id)
        {
            var entity = _unitOfWork.ShClassRepository.Get(item => item.Id == id).FirstOrDefault();
            _unitOfWork.ShClassRepository.Delete(entity);
            _unitOfWork.SaveChanges();
        }
    }
}

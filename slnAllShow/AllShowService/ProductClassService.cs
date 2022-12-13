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
    public class ProductClassService : IProductClassService
    {
        private readonly IUnitOfWorks _unitOfWork;
        private readonly IMapper _mapper;

        public ProductClassService(IUnitOfWorks unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<ProductClass> GetProductClass()
        {
            var list = _unitOfWork.ProductClassRepository.Get().ToList();
            return _mapper.Map<List<ProductClass>>(list);
        }

        public ProductClass GetProductClassId(int id)
        {
            var entity = _unitOfWork.ProductClassRepository.Get(item => item.Id == id).FirstOrDefault();
            return _mapper.Map<ProductClass>(entity);
        }


        public void CreateProductClass(ProductClass model)
        {
            var entity = _mapper.Map<ProductClass>(model);
            _unitOfWork.ProductClassRepository.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateProductClass(ProductClass model)
        {
            var entity = _mapper.Map<ProductClass>(model);
            _unitOfWork.ProductClassRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public void DeleteProductClass(int id)
        {
            var entity = _unitOfWork.ProductClassRepository.Get(item => item.Id == id).FirstOrDefault();
            _unitOfWork.ProductClassRepository.Delete(entity);
            _unitOfWork.SaveChanges();
        }
    }
}

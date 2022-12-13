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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWorks _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWorks unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<Product> GetProduct()
        {
            var list = _unitOfWork.ProductRepository.Get().ToList();
            return _mapper.Map<List<Product>>(list);
        }

        public Product GetProductId(int id)
        {
            var entity = _unitOfWork.ProductRepository.Get(item => item.Id == id).FirstOrDefault();
            return _mapper.Map<Product>(entity);
        }


        public void CreateProduct(Product model)
        {
            var entity = _mapper.Map<Product>(model);
            _unitOfWork.ProductRepository.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateProduct(Product model)
        {
            var entity = _mapper.Map<Product>(model);
            _unitOfWork.ProductRepository.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var entity = _unitOfWork.ProductRepository.Get(item => item.Id == id).FirstOrDefault();
            _unitOfWork.ProductRepository.Delete(entity);
            _unitOfWork.SaveChanges();
        }
    }
}

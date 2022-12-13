using AllShow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface IProductClassService
    {
        List<ProductClass> GetProductClass();

        ProductClass GetProductClassId(int id);

        void CreateProductClass(ProductClass model);

        void UpdateProductClass(ProductClass model);

        void DeleteProductClass(int id);
    }
}

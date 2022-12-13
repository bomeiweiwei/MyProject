using AllShow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface IProductService
    {
        List<Product> GetProduct();

        Product GetProductId(int id);

        void CreateProduct(Product model);

        void UpdateProduct(Product model);

        void DeleteProduct(int id);
    }
}

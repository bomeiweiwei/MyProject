using AllShow.Models;
using AllShowDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface IShopService
    {
        IEnumerable<ShopSettingDTO> GetShops();

        List<ShopSettingDTO> GetShopsByClass(int shclassno);

        List<ShopSettingDTO> GetShopsByPage(int pageIndex, int pageSize);

        ShopSettingDTO GetShopById(int id);

        void CreateShop(ShopSettingDTO model);

        void UpdateShop(ShopSettingDTO model);

        void DeleteShop(int id);
    }
}

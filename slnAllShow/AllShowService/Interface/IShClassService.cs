using AllShow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface IShClassService
    {
        List<ShClass> GetShClass();

        ShClass GetShClassId(int id);

        void CreateShClass(ShClass model);

        void UpdateShClass(ShClass model);

        void DeleteShClass(int id);
    }
}

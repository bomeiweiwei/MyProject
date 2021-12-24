using AllShowDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.Interface
{
    public interface IEmployeeSettingService
    {
        List<EmployeeSettingDTO> GetEmployee();

        EmployeeSettingDTO GetEmployeeId(int id);

        void CreateEmployee(EmployeeSettingDTO employee);

        void UpdateEmployee(EmployeeSettingDTO employee);

        void DeleteEmployee(int id);
    }
}

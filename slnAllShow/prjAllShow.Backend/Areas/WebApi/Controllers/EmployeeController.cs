using AllShowDTO;
using AllShowDTO.Infrastructure;
using AllShowService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prjAllShow.Backend.Areas.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeSettingService _employeeService;

        public EmployeeController(IEmployeeSettingService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ApiReponse<List<EmployeeSettingDTO>> List()
        {
            var employeeList = _employeeService.GetEmployee();
            return new ApiReponse<List<EmployeeSettingDTO>>(employeeList);
        }

        [HttpPost]
        [Route("create")]
        public BaseReponse Post([FromBody] EmployeeSettingDTO employee)
        {
            _employeeService.CreateEmployee(employee);
            return new BaseReponse { Success = true };
        }

        [HttpPost]
        [Route("update")]
        public BaseReponse Update([FromBody] EmployeeSettingDTO employee)
        {
            _employeeService.UpdateEmployee(employee);
            return new BaseReponse { Success = true };
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
        }
    }
}

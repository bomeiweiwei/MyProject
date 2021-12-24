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
    public class EmployeeSettingService : IEmployeeSettingService
    {
        private readonly IUnitOfWorks _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeSettingService(IUnitOfWorks unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<EmployeeSettingDTO> GetEmployee()
        {
            var employeeList = _unitOfWork.EmployeeRepository.Get().ToList();
            return _mapper.Map<List<EmployeeSettingDTO>>(employeeList);
        }

        public EmployeeSettingDTO GetEmployeeId(int id)
        {
            var employeeEntity = _unitOfWork.EmployeeRepository.Get(item => item.Id == id).FirstOrDefault();
            return _mapper.Map<EmployeeSettingDTO>(employeeEntity);
        }


        public void CreateEmployee(EmployeeSettingDTO employee)
        {
            var employeeEntity = _mapper.Map<EmployeeSetting>(employee);
            _unitOfWork.EmployeeRepository.Insert(employeeEntity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateEmployee(EmployeeSettingDTO employee)
        {
            var employeeEntity = _mapper.Map<EmployeeSetting>(employee);
            _unitOfWork.EmployeeRepository.Update(employeeEntity);
            _unitOfWork.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employeeEntity = _unitOfWork.EmployeeRepository.Get(item => item.Id == id).FirstOrDefault();
            _unitOfWork.EmployeeRepository.Delete(employeeEntity);
            _unitOfWork.SaveChanges();
        }
    }
}

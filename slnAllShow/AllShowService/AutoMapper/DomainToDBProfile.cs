using AllShow.Models;
using AllShowDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.AutoMapper
{
    public class DomainToDBProfile : Profile
    {
        public DomainToDBProfile()
        {
            CreateMap<EmployeeSettingDTO, EmployeeSetting>();
        }
    }
}

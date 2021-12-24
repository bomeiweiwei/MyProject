using AutoMapper;
using AllShow.Models;
using AllShowDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShowService.AutoMapper
{
    public class DBToDomainProfile : Profile
    {
        public DBToDomainProfile()
        {
            CreateMap<EmployeeSetting, EmployeeSettingDTO>();
        }
    }
}
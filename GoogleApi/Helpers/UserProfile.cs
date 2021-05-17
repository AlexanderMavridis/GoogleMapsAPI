using AutoMapper;
using GoogleApi.Dtos;
using GoogleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Helpers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Models.Attribute, AttributeDto>();
            CreateMap<AttributeDto, Models.Attribute>();
            CreateMap<EmployeeAttribute, EmployeeAttributeDto>();
            CreateMap<EmployeeAttributeDto, EmployeeAttribute>();
        }
    }
}

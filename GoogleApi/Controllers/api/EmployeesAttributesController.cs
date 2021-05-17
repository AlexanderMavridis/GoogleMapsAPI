using AutoMapper;
using GoogleApi.Dtos;
using GoogleApi.Models;
using GoogleApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesAttributesController : ControllerBase
    {
        private readonly IEmployeeAttributeRepository _employeeAttributeRepository;
        private readonly IMapper _mapper;

        public EmployeesAttributesController(IEmployeeAttributeRepository employeeAttributeRepository, IMapper mapper)
        {
            _employeeAttributeRepository = employeeAttributeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeAttributeDto>> GetEmployeesAttributes()
        {
            var records = await _employeeAttributeRepository.Get();
            var recordsDto = new List<EmployeeAttributeDto>();

            foreach (var item in records)
                recordsDto.Add(_mapper.Map<EmployeeAttributeDto>(item));

            return  recordsDto;
        }
    }
}

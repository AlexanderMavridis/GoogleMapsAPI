using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleApi.Data;
using GoogleApi.Models;
using GoogleApi.Repositories;
using GoogleApi.Dtos;
using GoogleApi.Helpers;
using AutoMapper;

namespace GoogleApi.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDto>> GetEmployees()
        {
            var employees = await _employeeRepository.Get();
            var employeesDto = new List<EmployeeDto>();

            foreach (var employee in employees)
                //employeesDto.Add(MyMapper.MapToEmployeeDto(employee));
                employeesDto.Add(_mapper.Map<EmployeeDto>(employee));

            return employeesDto;
        }

        [HttpGet("employee/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid id)
        {
            var employee = await _employeeRepository.Get(id);
            if (employee == null)
                return NotFound();

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }


        [HttpPut("updateEmployee/{id}")]
        public async Task<ActionResult> PutEmployee(Guid id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmpId)
                return BadRequest();

            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.Update(employee);

            return NoContent();
        }


        [HttpPut("updateEmployeesAttributes")]
        public async Task<ActionResult> PutEmployeesAttributes([FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            bool hasDuplicates = _employeeRepository.validEmployeeAttributes(employee);
            if (hasDuplicates == true)
                return BadRequest();

            await _employeeRepository.UpdateEmployeesAttributes(employee);
            return Ok();
        }


        [HttpPost("newEmployee")]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var employee = _mapper.Map<Employee>(employeeDto);
            var storedEmployee = await _employeeRepository.Create(employee);
            var newEmployeeDto = _mapper.Map<EmployeeDto>(storedEmployee);                  // polla var.... 

            return CreatedAtAction("GetEmployee", new { id = newEmployeeDto.EmpId }, newEmployeeDto);
        }

        [HttpPost("postEmployeesAttributes")]
        public async Task<ActionResult> PostEmployeesAttributes([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto.EmpId == default(Guid))                         // is it too much? because this works as second ajax with guid set
                return BadRequest();

            var employee = _mapper.Map<Employee>(employeeDto);
            bool hasDuplicates = _employeeRepository.validEmployeeAttributes(employee);
            if (hasDuplicates == true)
                return BadRequest();

            await _employeeRepository.PostEmployeesAttributes(employee);
            return Ok();
        }

        [HttpDelete("deleteEmployee/{id}")]
        public async Task<ActionResult> DeleteEmployee(Guid id)
        {
            var employeeToDelete = await _employeeRepository.Get(id);
            if (employeeToDelete == null)
                return NotFound();

            await _employeeRepository.Delete(employeeToDelete.EmpId);      //should i create-modify task<Employee> Delete in order to take the eemployeeToDelete obj and query only once against database?
            return NoContent();                                           // also why doesnt delete like this  --->  _employeeRepository.Delete(id); 
        }

        [HttpGet("getRelatedData/{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesWithSpecificAttribute(Guid id)
        {
            if (id == default(Guid))
                return BadRequest();

            var employees = await _employeeRepository.GetRelatedData(id);
            
            if (employees == null)
                return NoContent();
            else
            {
                var employeeDtoList = new List<EmployeeDto>();
                foreach (var employee in employees)
                    employeeDtoList.Add(_mapper.Map<EmployeeDto>(employee));

                return employeeDtoList;
            }
               
        }
    }
}

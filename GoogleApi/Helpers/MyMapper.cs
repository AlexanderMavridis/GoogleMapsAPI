using GoogleApi.Dtos;
using GoogleApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Helpers
{
    public class MyMapper
    {

        // Attribute Mapping
        public static AttributeDto MapToAttributeDto(Models.Attribute attribute)
        {
            var attributeDto = new AttributeDto
            {
                AttrId = attribute.AttrId,
                AttrName = attribute.AttrName,
                AttrValue = attribute.AttrValue,
                EmployeeAttributes = new Collection<EmployeeAttributeDto>()         // at this point we are ok with new Collection, lateer need to modify
            };

            return attributeDto;
        }

        public static Models.Attribute MapToAttribute(AttributeDto attributeDto)
        {
            var attribute = new Models.Attribute
            {
                AttrId = attributeDto.AttrId,
                AttrName = attributeDto.AttrName,
                AttrValue = attributeDto.AttrValue,
                EmployeeAttributes = new Collection<EmployeeAttribute>()
            };

            return attribute;
        }


        //Employee Mapping
        public static EmployeeDto MapToEmployeeDto(Employee employee)
        {
            var employeeDto = new EmployeeDto
            {
                EmpId = employee.EmpId,
                EmpName = employee.EmpName,
                EmpDateOfHire = employee.EmpDateOfHire,
                //EmployeeSupervisor = new EmployeeDto 
                //{
                //    EmpId = employee.EmployeeSupervisor.EmpId, 
                //    EmpName = employee.EmployeeSupervisor.EmpName,
                //    EmpDateOfHire = employee .EmployeeSupervisor.EmpDateOfHire,
                //    EmployeeAttributes = (ICollection<EmployeeAttributeDto>)employee.EmployeeSupervisor.EmployeeAttributes
                //},
                EmpSupervisorId = employee.EmpSupervisorId,
                EmployeeAttributes = FillEmpAttrList(employee)
            };

            return employeeDto;
        }

        public static Employee MapToEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                EmpId = employeeDto.EmpId,
                EmpName = employeeDto.EmpName,
                EmpDateOfHire = employeeDto.EmpDateOfHire,
                EmployeeSupervisor = new Employee(),
                EmpSupervisorId = employeeDto.EmpSupervisorId,
                EmployeeAttributes = new Collection<EmployeeAttribute>()
            };

            return employee;
        }

        public static List<EmployeeAttributeDto> FillEmpAttrList(Employee employee)
        {
            var empAttrDtoList = new List<EmployeeAttributeDto>();
            foreach (var data in employee.EmployeeAttributes)
            {
                empAttrDtoList.Add(new EmployeeAttributeDto { });     
            }

            return empAttrDtoList;
        }


        // EmployeeAttribute Mapping
        public static EmployeeAttributeDto MapToEmployeeAttributeDto(EmployeeAttribute employeeAttribute)
        {
            var employeeAttributeDto = new EmployeeAttributeDto
            {

            };

            return employeeAttributeDto;
        }
    }
}

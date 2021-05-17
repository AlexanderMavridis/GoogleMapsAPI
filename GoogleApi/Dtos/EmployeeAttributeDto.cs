using GoogleApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Dtos
{
    public class EmployeeAttributeDto
    {
        public Guid EmpattrEmployeeId { get; set; }
        public EmployeeDto EmpattrEmployee { get; set; }
        public Guid EmpattrAttributeId { get; set; }
        public AttributeDto EmpattrAttribute { get; set; }


        // No....
        //public EmployeeAttributeDto(EmployeeDto employeeDto, Guid empattrEmployeeId, AttributeDto attributeDto, Guid empattrAttributeId )
        //{
        //    this.EmpattrAttribute = attributeDto;
        //    this.EmpattrAttributeId = empattrAttributeId;
        //    this.EmpattrEmployee = employeeDto;
        //    this.EmpattrEmployeeId = empattrEmployeeId;
        //}

        //public static implicit operator EmployeeAttributeDto(EmployeeAttribute employeeAttribute)
        //{
        //    var employee = (EmployeeDto)employeeAttribute.EmpattrEmployee;
        //    var employeeId = employeeAttribute.EmpattrEmployeeId;
        //    var attribute = employeeAttribute.EmpattrAttribute;
        //    var attributeId = employeeAttribute.EmpattrAttributeId;

        //    return new EmployeeAttributeDto(employee, employeeId, attribute, attributeId);
        //}


    }
}

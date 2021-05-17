using GoogleApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Dtos
{
    public class EmployeeDto
    {
        public Guid EmpId { get; set; }

        [Required]
        [StringLength(100)]
        public string EmpName { get; set; }

        [Required]
        [Range(typeof(DateTime), "1990-12-01", "2021-01-31",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime EmpDateOfHire { get; set; }

        public Guid? EmpSupervisorId { get; set; }
        public EmployeeDto EmployeeSupervisor { get; set; }      
        public ICollection<EmployeeAttributeDto> EmployeeAttributes { get; set; }

        public EmployeeDto()
        {
            EmployeeAttributes = new List<EmployeeAttributeDto>();
        }
       
        public bool CarOwner { get; set; }

        //Latitude
        private decimal _residenceLat;

        [Required]
        public decimal ResidenceLat
        {
            get { return _residenceLat; }
            set
            {
                if (value <= 9999999999)
                {
                    _residenceLat = Math.Round(value, 8);
                }
                else
                    throw new InvalidOperationException();
            }
        }

        //Longitude
        private decimal _residenceLng;

        [Required]
        public decimal ResidenceLng
        {
            get { return _residenceLng; }
            set
            {
                if (value <= 99999999999)
                {
                    _residenceLng = Math.Round(value, 8);
                }
                else
                    throw new InvalidOperationException();
            }
        }


        // Not now....
        //public static explicit operator EmployeeDto(Employee employee)
        //{
        //    return new EmployeeDto
        //    {
        //        EmpId = employee.EmpId,
        //        EmpName = employee.EmpName,
        //        EmpDateOfHire = employee.EmpDateOfHire,
        //        EmployeeSupervisor = (EmployeeDto)employee.EmployeeSupervisor,
        //        EmployeeAttributes = (ICollection<EmployeeAttributeDto>)employee.EmployeeAttributes,
        //        EmpSupervisorId = employee.EmpSupervisorId
        //    };
        //}
    }
}

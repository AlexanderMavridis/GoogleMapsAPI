using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoogleApi.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Column("EMP_ID")]
        public Guid EmpId { get; set; }

        [Required]
        [Column("EMP_NAME")]
        [StringLength(100)]
        public string EmpName { get; set; }

        [Required]
        [Column("EMP_DateOfHire")]
        [Range(typeof(DateTime), "1990-12-01", "2021-01-31",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public DateTime EmpDateOfHire { get; set; }

        [ForeignKey("EmployeeSupervisor")]
        [Column("EMP_Supervisor")]
        public Guid? EmpSupervisorId { get; set; }
        public Employee EmployeeSupervisor { get; set; }       //   this nav prop shows all hierarchical data inside JSON! Why???

        public ICollection<EmployeeAttribute> EmployeeAttributes { get; set; }
      
        [Column("Car_owner")]
        public bool CarOwner { get; set; }

        //Latitude
        private decimal _residenceLat;

        [Required]
        [Column("Residence_lat")]
        public decimal ResidenceLat 
        {
            get { return _residenceLat; } 
            set {
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
        [Column("Residence_lng")]
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

        public Employee()
        {
            EmployeeAttributes = new Collection<EmployeeAttribute>();
        }
    }
}

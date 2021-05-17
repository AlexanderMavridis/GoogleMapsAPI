using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoogleApi.Models
{
    [Table("EmployeeAttribute")]
    public class EmployeeAttribute
    {
        [Key]
        [Column("EMPATTR_EmployeeID")]
        public Guid EmpattrEmployeeId { get; set; }

        [ForeignKey("EmpattrEmployeeId")]       // not sure if needed
        public Employee EmpattrEmployee { get; set; }

        [Key]
        [Column("EMPATTR_AttributeID")]
        public Guid EmpattrAttributeId { get; set; }

        [ForeignKey("EmpattrAttributeId")]         // not sure if needed
        public Attribute EmpattrAttribute { get; set; }
        
    }
}

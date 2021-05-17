using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GoogleApi.Models
{
    [Table("Attribute")]
    public class Attribute
    {
        [Key]
        [Column("ATTR_ID")]
        public Guid AttrId { get; set; }

        [Required]
        [Column("ATTR_Name")]
        [StringLength(50)]
        public string AttrName { get; set; }

        [Required]
        [Column("ATTR_Value")]
        [StringLength(50)]
        public string AttrValue { get; set; }

        public ICollection<EmployeeAttribute> EmployeeAttributes { get; set; }

        public Attribute()
        {
            EmployeeAttributes = new Collection<EmployeeAttribute>();
        }
    }
}

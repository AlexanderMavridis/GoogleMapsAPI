using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleApi.Dtos
{
    public class AttributeDto
    {
        public Guid AttrId { get; set; }

        [Required]
        [StringLength(50)]
        public string AttrName { get; set; }

        [Required]
        [StringLength(50)]
        public string AttrValue { get; set; }

        public ICollection<EmployeeAttributeDto> EmployeeAttributes { get; set; }
    }
}

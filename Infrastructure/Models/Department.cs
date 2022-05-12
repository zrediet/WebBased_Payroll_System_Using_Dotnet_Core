using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public  class Department : FullAuditEntity
    {
        [Required]
        [MaxLength(50)]
        [Display(Name="Department Name")]
        public string DepartmentName { get; set; }
         
    }
}

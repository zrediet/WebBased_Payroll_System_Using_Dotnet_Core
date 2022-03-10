using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class SubProject : FullAuditEntity
    {
        [Required]
        [MaxLength(10)]
        [Display(Name="Sub Project ID")]
        public string SubProjectId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Sub Project Name")]
        public string SubProjectName { get; set; }

        public virtual Project Project { get; set; }
        public string  ProjectId { get; set; }

        [Display(Name = "Chart Of Account")]
        public int ChartOfAccount { get; set; }

        [MaxLength(250)]
        public string Remark { get; set; }
    }
}

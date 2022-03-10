using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;

namespace Infrastructure.Models
{
    public class Project : FullAuditEntity
    {
        [Required]
        [MaxLength(10)]
        [Display(Name="Project ID")]
        public string ProjectId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Chart Of Account")]
        public int ChartOfAccount { get; set; }
        
        [MaxLength(250)]
        public string Remark { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public  class IncomeTaxSetting : FullAuditEntity
    {
        [Required]
        [Range(0.00, 999999999, ErrorMessage = "Value must be greater than OR equal to zero")]
      
        [Display(Name = "Starting Amount")] public float StartingAmount { get; set; }
        [Required]
        [Range(0.00, 999999999, ErrorMessage = "Value must be greater than OR equal to zero")]
       
        [Display(Name = "Ending Amount")] public float EndingAmount { get; set; }
        [Required]
        [Range(0.00, 999999999, ErrorMessage = "Value must be greater than OR equal to zero")]
      
        public float Percent { get; set; }
        [Required]
        [Range(0.00, 999999999, ErrorMessage = "Value must be greater than OR equal to zero")]
        
        public float Deductable { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
      
        public DateTime ActiveDate { get; set; }
    }
}

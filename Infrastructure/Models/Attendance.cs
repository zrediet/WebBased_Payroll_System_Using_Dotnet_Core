using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Attendance : FullAuditEntity
    {
        public virtual Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        [Required]
        [Display(Name = "From")] 
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime From { get; set; }
        
        [Required]
        [Display(Name = "To")] 
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime To { get; set; }

        [Display(Name = "No. Days")]
        public int NoDays { get; set; }

        [Display(Name = "Normal OT")] public int NormalOT { get; set; }
        [Display(Name = "Normal OT 2")] public int NormalOT2 { get; set; }
        [Display(Name = "Weekend OT")]public int WeekendOT { get; set; }
        [Display(Name = "HolyDay OT")] public int HolyDayOT { get; set; }



    }
}

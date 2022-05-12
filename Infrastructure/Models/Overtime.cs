using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Overtime : FullAuditEntity
    {
        public virtual Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        [Display(Name = "Normal OT")] public int NormalOT { get; set; }
        [Display(Name = "Normal OT 2")] public int NormalOT2 { get; set; }
        [Display(Name = "Weekend OT")]public int WeekendOT { get; set; }
        [Display(Name = "HolyDay OT")] public int HolyDayOT { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }
    }
}

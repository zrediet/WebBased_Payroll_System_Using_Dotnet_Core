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

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime Date { get; set; }

        public AttendanceType AttendanceType { get; set; }
        public Reason? Reason { get; set; }
        public string Remark { get; set; }
         
    }

    public enum AttendanceType
    {
        [Display(Name = "Absent")] Absent,
        [Display(Name = "Available")] Available,
        [Display(Name = "Day Off")] Day_Off,
        [Display(Name = "Holiday")] Holiday,
        [Display(Name = "On Leave")] On_Leave,
        [Display(Name = "Reason")] Reason
    }

    public enum Reason
    {
        [Display(Name="Sick Leave")] Sick_leave,
        [Display(Name="On Field")] on_field,
        [Display(Name="Other")] Other
    }
}

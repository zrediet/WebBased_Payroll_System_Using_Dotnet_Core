using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PayrollSetting : FullAuditEntity
    {
        [Display(Name = "General Payroll Setting")]
        public GeneralPSett GeneralPSett { get; set; }
        
        [Required]
        [Display(Name ="Value")]
        public float Value { get; set; }
         
    } 
     
    public enum GeneralPSett
    {
        [Display(Name = "Working Days")] WorkingDays,
        [Display(Name = "Pension Employee")] PensionEmployee,
        [Display(Name = "Pension Company")] PensionCompany,
        [Display(Name = "Normal OT")] NormalOT,
        [Display(Name = "Normal OT 2")] NormalOT2,
        [Display(Name = "Weekend OT")] WeekendOT, 
        [Display(Name = "HolyDay OT")] HolidayOT,
        [Display(Name = "Payment Date")] PaymentDate,
        [Display(Name = "Max Non-Taxable Allowance Amount")] MaxNonTaxableAllowanceAmount

    }

}

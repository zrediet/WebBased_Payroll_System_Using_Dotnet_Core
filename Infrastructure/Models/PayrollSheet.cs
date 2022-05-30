using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PayrollSheet : FullAuditEntity
    {
        public virtual Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "From")]
        public DateTime PayStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "To")]
        public DateTime PayEnd { get; set; }

        //[Display(Name = "Round")]
        //public PaymentRound PaymentRound { get; set; }

        [Display(Name="No.Days")]
        public int NoDays { get; set; }

        [Display(Name = "Basic Salary")]
        public float BasicSalary { get; set; }
        [Display(Name = "Allowance")]
        public float Allowance { get; set; }
        [Display(Name = "Over Time")]
        public float OverTime { get; set; }
        [Display(Name = "Gross Salary")]
        public float GrossSalary { get; set; }
        [Display(Name = "Income Tax")]
        public float IncomeTax { get; set; }
        [Display(Name = "Pension Employee")]
        public float PensionEmployee { get; set; }
        [Display(Name = "Pension Company")] public float PensionCompany { get; set; }
        [Display(Name = "Loan")]
        public float Loan { get; set; }
        [Display(Name = "Penality")]
        public float Penality { get; set; }
        [Display(Name = "Other Deductions")]
        public float? OtherDeduction { get; set; }
        [Display(Name = "Net Pay")]
        public float NetPay { get; set; }
        [Display(Name = "Non-Taxable Allowance")]
        public float NonTaxableAllowance { get; set; }

        public PayrollStatus PayrollStatus { get; set; }
    }
     

    public enum PayrollStatus
    {
        [Display(Name = "Demo")] Demo,
        [Display(Name = "Final")] Final
    }

}

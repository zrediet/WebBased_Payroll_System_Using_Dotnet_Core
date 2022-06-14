using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public  class Deduction : FullAuditEntity
    {
        public float Amount { get; set; }
        public DeductionType DeductionType { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.InProgress;

        public virtual Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        public float MonthlySettlement { get; set; }

    }

    public enum DeductionType
    {
        Loan,
        Court
    }

    public enum PaymentStatus
    {
        [Display(Name="In Progress")]
        InProgress,
        [Display(Name="Paid In Full")]
        PaidInFull
    }

}

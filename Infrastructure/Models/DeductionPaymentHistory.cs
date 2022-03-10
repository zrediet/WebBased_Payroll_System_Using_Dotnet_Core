using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class DeductionPaymentHistory : FullAuditEntity
    {
        public virtual Deduction Deduction { get; set; }
        public string DeductionId { get; set; }

        [Display(Name="Amount Paid")]
        public float AmountPaid { get; set; }
        [Display(Name = "Balance")]
        public float Balance { get; set; }
         
    }
}

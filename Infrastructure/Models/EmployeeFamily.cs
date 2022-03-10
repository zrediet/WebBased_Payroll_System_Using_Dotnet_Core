using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class EmployeeFamily : FullAuditEntity
    {
        public string FullName { get; set; }
        public virtual Employee Employee { get; set; }
        public string EmployeeId { get; set; }

        [Display(Name = "Family Allocated Amount")]
        public float Amount { get; set; }

        [Display(Name = "Bank Account")]
        public string BankAccount { get; set; }
    }
}

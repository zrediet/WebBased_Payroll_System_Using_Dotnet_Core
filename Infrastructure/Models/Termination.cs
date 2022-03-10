using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public  class Termination : FullAuditEntity
    {
        public virtual Employee Employee{ get; set; }
        public string EmployeeId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Termination Date")]
        public DateTime TerminationDate { get; set; }

        public TerminationReason TerminationReason { get; set; }
    }


    public enum TerminationReason
    {
        [Display(Name = "Contract End")] ContractEnd,
        [Display(Name = "Death")] Death,
        [Display(Name = "Other")] Other
    }
}

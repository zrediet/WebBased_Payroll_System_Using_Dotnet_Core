using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class EmployeeSalary : FullAuditEntity
    {
        public virtual Employee Employee { get; set; }
        public string EmployeeId { get; set; }


        [Required]
        public float BasicSalary { get; set; }

        public float? TransportAllowance { get; set; }
        public float? HomeAllowance { get; set; }
        public float? PositionAllowance { get; set; }
        public float? OtherAllowance { get; set; }


        public SalaryStatus? SalaryStatus { get; set; }
        public bool IsTaxable { get; set; } = false;
    }

    public enum SalaryStatus
    {
        [Display(Name="Active")] Active,
        [Display(Name="In-Active")] InActive
    }
}

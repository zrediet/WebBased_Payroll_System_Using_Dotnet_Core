using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AllowancePackageDetail : FullAuditEntity
    {
        public string AllowanceName { get; set; }
        
        [Display(Name = "Amount")]
        public float Value { get; set; }
        public virtual AllowancePackage AllowancePackage { get; set; }
        public string AllowancePackageId { get; set; }
        [Display(Name = "Is Taxable?")]
        public bool Taxable { get; set; }
        
    }
}

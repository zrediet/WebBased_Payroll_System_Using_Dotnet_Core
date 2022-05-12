using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AllowancePackage : FullAuditEntity
    {
        public string PackageName { get; set; }
        public string PackageVersion { get; set; } 

    }
}

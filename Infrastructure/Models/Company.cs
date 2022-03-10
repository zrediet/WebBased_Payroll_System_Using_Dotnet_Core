using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Company : FullAuditEntity
    {
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set;}
        public string CompanyPhone { get; set;}
        public string Email { get; set; }
        public string Website { get; set; }
        public string Fax { get; set; }
        public string Location { get; set; }
    }
}

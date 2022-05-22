﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Company : FullAuditEntity
    {
        
        [Required] public string CompanyName { get; set; }
        [Required] public string CompanyDescription { get; set;}
        [Required] public string CompanyPhone { get; set;}
        [Required] public string Email { get; set; }
        public string Website { get; set; }
        public string Fax { get; set; }
        public string Location { get; set; }
    }
}

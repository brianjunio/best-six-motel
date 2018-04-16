using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Company
    {   [Key]
        public int CompanyID { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Owner { get; set; }

        public string PhoneNumber { get; set; }

    }
}

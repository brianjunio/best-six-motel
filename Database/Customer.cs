using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; } 
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string PaymentInfo { get; set; }
        public string PhoneNo { get; set; }
        [ForeignKey("Room")]
        public int RoomNo { get; set;}
        public virtual Room Room { get; set; }
        public string LastEdit { get; set; }
    }
}

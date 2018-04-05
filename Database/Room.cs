using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Room
    {   [Key]
        public int RoomNo { get; set; }
        public string BedType { get; set; }
        public int NoOfBeds { get; set; }
        public string Smoking { get; set; }
        public double Price { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }
        public string Legend { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public virtual Customer Customer { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * 
 * 
 * 
 */
namespace Database
{
    class Transactions
    {   [Key]
        // This will be the key for the TR table.
        public int TrNumber { get; set; }

        public int RoomNo { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public virtual Customer Customer { get; set; }

    }
}

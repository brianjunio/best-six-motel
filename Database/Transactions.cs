using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Primary Key - TrNumber
 * Foreign Keys - ID, Room
 * Authored by - Coleton B.
 */
namespace Database
{
    public class Transactions
    { 
        [Key]
        public int TrNumber { get; set; } 

        [ForeignKey("ID")]
        public string ID { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Room")]
        public int RoomNo { get; set; }
        public virtual Room Room { get; set; }

        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public DateTime DateModified { get; set; }
    }
}

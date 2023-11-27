using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Models
{
    public class reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public int TableId { get; set; }
        public Tables Table { get; set; }

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public DateTime ReservationDateTime { get; set; }

        public Tables Tables { get; set; }
    }
}

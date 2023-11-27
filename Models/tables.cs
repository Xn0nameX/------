using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Курсач.Models
{
    public class Tables
    {
        [Key]
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsReserved { get; set; }
        public int? ReservedByUserId { get; set; } // Разрешаем null
        public Userr ReservedByUser { get; set; }

        public ICollection<reservation> Reservations { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Models
{
    public class Orderitem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Orders Orders { get; set; }

        public int MenuItemId { get; set; }
        public Menuitem Menuitem { get; set; }

        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public bool IsCompleted { get; set; }
        public string Note { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Models
{
    public class Menuitem
    {
        [Key]
        public int MenuItemId { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
        public ICollection<Orderitem> Orderitem { get; set; }
        public menuitemcategory menuitemcategory { get; set; }
    }
}

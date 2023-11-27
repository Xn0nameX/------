using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Курсач.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public Userr Userr { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusId { get; set; }
        public Orderstatus Orderstatus { get; set; }

        public ICollection<Orderitem> Orderitem { get; set; }
        public payment payment { get; set; }


    }
}

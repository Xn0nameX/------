using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Курсач.Models
{
    public class UserOrderDto
    {
        public int OrderId { get; set; }

        public string OrderStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}

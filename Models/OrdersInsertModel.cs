using System;
using System.ComponentModel.DataAnnotations;

namespace Курсач.Models
{
    public class OrdersInsertModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int StatusId { get; set; }

        public List<OrderitemInsertModel> OrderItems { get; set; }
    }
}

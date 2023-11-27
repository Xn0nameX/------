using System;
using System.ComponentModel.DataAnnotations;

namespace Курсач.Models
{
    public class OrderitemInsertModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal ItemPrice { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public string Note { get; set; }
    }
}

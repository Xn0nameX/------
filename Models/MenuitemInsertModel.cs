using System;
using System.ComponentModel.DataAnnotations;

namespace Курсач.Models
{
    public class MenuitemInsertModel
    {
        [Required]
        public string ItemName { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool Availability { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Models
{
    public class payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int OrderId { get; set; }
        public Orders order { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDateTime { get; set; }
    }
}

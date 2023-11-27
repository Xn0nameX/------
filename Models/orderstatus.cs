using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Курсач.Models
{
    public class Orderstatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

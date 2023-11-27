using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсач.Models
{
    public class TablesInsertModel
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public bool IsReserved { get; set; }
        public int? ReservedByUserId { get; set; }
    }
}

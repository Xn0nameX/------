using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Курсач.Models
{
    public class Userr
    {
        [Key]
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public UserRole UserRole { get; set; }
        public string DOB { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Tables> Tables { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

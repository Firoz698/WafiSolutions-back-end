using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WafiSolutions.core
{
    public class Employee
    {
        public Employee()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            Image = null;
            Email = "";
            Mobile = "";
            DateOfBirth = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public byte[]? Image { get; set; }
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}

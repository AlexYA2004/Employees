using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Entities
{
    public class Employee : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        public int GetFullAge()
        {
            var today = DateTime.Today;

            var age = today.Year - DateOfBirth.Year;

            if (DateOfBirth.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}

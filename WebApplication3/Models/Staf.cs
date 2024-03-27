using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace restSakei.Models
{
    public class Staf
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [UniqueContactNumber]
        [Required]
        public string ContactNumber { get; set; }

        public decimal Salary { get; set; }

        
        public DateTime HiringDate { get; set; }

        public string Position { get; set; }
    }
    }

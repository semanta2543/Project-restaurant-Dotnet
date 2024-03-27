
using restSakei.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models 
{
    public class ContactUsModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^06\d{8}$", ErrorMessage = "Contact number duhet të fillojë me 06")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact number duhet të jetë saktësisht 10 karaktere")]
        public string ContactNumber { get; set; }
        [Required]
        public string Message { get; set; }

    }
}




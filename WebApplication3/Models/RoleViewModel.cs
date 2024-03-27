using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class RoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}

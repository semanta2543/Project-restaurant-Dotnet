using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Category
    {
        internal readonly object Produkt;

        public int CategoryId { get; set; }
        [Required]
        [StringLength(25)]
        public string CategoryName { get; set; } = null!;
    }
}

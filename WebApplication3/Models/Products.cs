using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public string? Discription { get; set; }

        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string? Image { get; set; } = String.Empty;


        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Product Image")]
        [FileExtensions(Extensions = "jpg")]
        [Required]
        public IFormFile ImageFile { get; set; }
    }
}

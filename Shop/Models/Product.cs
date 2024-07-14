using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Product")]
    public class Product 
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(60, ErrorMessage = "Title can't be longer than 60 characters")]
        [MinLength(3, ErrorMessage = "Title can't be shorter than 3 characters")]
        public required string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Description can't be longer than 1024 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Category Id")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
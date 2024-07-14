using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(60, ErrorMessage = "Title can't be longer than 60 characters")]
        [MinLength(3, ErrorMessage = "Title can't be shorter than 3 characters")]
        public int Title { get; set; }
    }
}
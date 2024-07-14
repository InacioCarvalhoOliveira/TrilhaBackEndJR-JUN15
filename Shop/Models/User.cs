using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
     [Table("User")]
     public class User
     {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "This field must have at least 3 characters")]
        [MaxLength(20, ErrorMessage = "This field must have at most 20 characters")]
        public required string UserName { get; set; }

        [MinLength(8, ErrorMessage = "This field must have at least 8 characters")]
        [MaxLength(15, ErrorMessage = "This field must have at most 15 characters")]
        [Required(ErrorMessage = "This field is required")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Role { get; set;}
     }
 }
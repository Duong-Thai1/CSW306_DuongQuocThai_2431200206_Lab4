using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuongQuocThai_2431200206_Lab4.Models
{
    [Table("Categories")]

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        //public ICollection<Book>? Books { get; set; }
        [Required(ErrorMessage = "Please enter Title")]
        [StringLength(4000, ErrorMessage = "The length should be less than 4000")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please enter CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Please enter UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
        [Required(ErrorMessage = "Please enter IsActive")]
        public bool IsActive { get; set; }
        public string? Avatar { get; set; }

    }
}

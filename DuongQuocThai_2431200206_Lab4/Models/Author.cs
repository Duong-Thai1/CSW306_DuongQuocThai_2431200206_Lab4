using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuongQuocThai_2431200206_Lab4.Models
{
    [Table("Authors")]

    public class Author
    {
        [Key]
        public int? AuthorId { get; set; }
        //public ICollection<Book>? Books { get; set; }
        [Required(ErrorMessage = "Please enter FirstName")]
        [StringLength(100, ErrorMessage = "The length should be less than 100")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter LastName")]
        [StringLength(100, ErrorMessage = "The length should be less than 100")]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(100, ErrorMessage = "The length should be less than 100")]
        public string? Biography { get; set; }
        [StringLength(100, ErrorMessage = "The length should be less than 100")]
        public string? Nationality { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "The format of email is incorrect")]
        [StringLength(100, ErrorMessage = "The length should be less than 100")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "The length should be less than 100")]
        public string? Website { get; set; }
        [Required(ErrorMessage = "Please enter CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Please enter if author is active")]
        public bool IsActive { get; set; }
        public string? Avatar { get; set; }

    }
}

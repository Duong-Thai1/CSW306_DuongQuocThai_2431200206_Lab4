using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuongQuocThai_2431200206_Lab4.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int? BookId { get; set; }
        //public ICollection<Loan>? Loans { get; set; }

        [Required(ErrorMessage = "Please enter Title")]
        [StringLength(200, ErrorMessage = "The length should be less than 200")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? BookCode { get; set; }
        [Required(ErrorMessage = "Please enter Publisher")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "Please enter PublishedYear")]
        public DateTime PublishedYear { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        [Required(ErrorMessage = "Please enter TotalCopies")]
        public int TotalCopies { get; set; }
        [Required(ErrorMessage = "Please enter AvailableCopies")]
        public int AvailableCopies { get; set; }
        [Required(ErrorMessage = "Please enter CreatedDate")]
        public DateTime CreatedDate { get; set; }
        public string? Avatar { get; set; }
        public string? Pdf { get; set; }
        public bool IsActive { get; set; }
    }
}

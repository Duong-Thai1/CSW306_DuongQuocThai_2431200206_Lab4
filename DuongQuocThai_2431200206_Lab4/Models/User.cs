using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuongQuocThai_2431200206_Lab4.Models
{
    [Table("Users")]

    public class User
    {
        [Key]
        public int UserId { get; set; }
        //public ICollection<Loan> Loans { get; set; }

        [Required(ErrorMessage = "Please enter Fullname")]
        [StringLength(200, ErrorMessage = "The length should be less than 200")]
        public string Fullname { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [EmailAddress(ErrorMessage = "The format of email is incorrect")]
        [StringLength(100, ErrorMessage = "The length should be less than 100")]
        public string Email { get; set; }
        [StringLength(20, ErrorMessage = "The length should be less than 20")]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [Required(ErrorMessage = "Please enter Status")]
        public int Status { get; set; }
        [Required(ErrorMessage = "Please enter CreatedDate")]
        public DateTime CreatedDate { get; set; }
        public string? UserCode { get; set; }
        [Required(ErrorMessage = "Please enter if user is locked")]
        public bool IsLocked { get; set; }
        [Required(ErrorMessage = "Please enter if user is deleted")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Please enter if user is active")]
        public bool IsActive { get; set; }
        public string? ActiveCode { get; set; }
        public string? Avatar { get; set; }
    }
}

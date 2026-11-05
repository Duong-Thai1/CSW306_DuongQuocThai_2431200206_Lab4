using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuongQuocThai_2431200206_Lab4.Models
{
    [Table("Loans")]

    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        [Required(ErrorMessage = "Please enter UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "Please enter BookId")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required(ErrorMessage = "Please enter LoanDate")]
        public DateTime LoanDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required(ErrorMessage = "Please enter Status")]
        public int Status { get; set; }
    }
}

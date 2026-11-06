using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DuongQuocThai_2431200206_Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private LibraryDBContext _context;
        public LoansController(LibraryDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoans()
        {
            var loans = _context.Loans.ToList();
            if (loans.Count == 0)
            {
                return NotFound();
            }
            return Ok(loans);
        }

        [HttpGet("filterByUserId/{userId}")]
        public async Task<IActionResult> GetLoansByUserId(int userId)
        {
            var loans = _context.Loans.ToList();
            var filteredLoans = loans.FindAll(l => l.UserId == userId);
            if (filteredLoans.Count == 0)
            {
                return NotFound();
            }
            return Ok(filteredLoans);
        }

        [HttpGet("filterByStatus/{status}")]
        public async Task<IActionResult> GetLoansByStatus(int status)
        {
            var loans = _context.Loans.ToList();
            var filteredLoans = loans.FindAll(l => l.Status == status);
            if (filteredLoans.Count == 0)
            {
                return NotFound();
            }
            return Ok(filteredLoans);
        }

        [HttpGet("filterByLoanDateRange")]
        public async Task<IActionResult> GetLoans([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            if (fromDate == null)
            {
                return BadRequest();
            }
            if (toDate == null)
            {
                toDate = DateTime.Now;
            }
            var loans = _context.Loans.ToList();
            var filteredLoans = loans.FindAll(l => l.LoanDate >= fromDate && l.LoanDate <= toDate);
            if (filteredLoans.Count == 0)
            {
                return NotFound();
            }
            return Ok(filteredLoans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanDetails(int id)
        {
            var loans = _context.Loans.ToList();
            var loan = loans.Find(l => l.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }
            var user = _context.Users.ToList().Find(u => u.UserId == loan.UserId);
            var book = _context.Books.ToList().Find(b => b.BookId == loan.BookId);

            return Ok(new
            {
                LoanId = loan.LoanId,
                User = user,
                BorrowedBook = book,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = loan.Status
            });
        }
        [HttpPost]
        public async Task<IActionResult> CreateLoanRecord([FromBody] Loan loan)
        {
            if (loan == null)
            {
                return BadRequest();
            }
            var user = _context.Users.ToList().Find(u => u.UserId == loan.UserId);
            var book = _context.Books.ToList().Find(b => b.BookId == loan.BookId);
            if (user == null || book == null)
            {
                return BadRequest();
            }
            if (book.AvailableCopies == 0 || !book.IsActive)
            {
                return BadRequest("Book is not available");
            }
            loan.LoanDate = DateTime.Now;
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return Ok(loan);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var loans = _context.Loans.ToList();
            var loan = loans.Find(l => l.LoanId == id);
            if (loan == null)
            {
                return NotFound();
            }
            var book = _context.Books.ToList().Find(b => b.BookId == loan.BookId);
            if (book == null)
            {
                return BadRequest();
            }
            loan.ReturnDate = DateTime.Now;
            loan.Status = 1;
            book.AvailableCopies++;
            await _context.SaveChangesAsync();
            return Ok(loan);
        }
    }
}

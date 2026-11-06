using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DuongQuocThai_2431200206_Lab4.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class BooksReportsController : ControllerBase
    {
        private LibraryDBContext _context;
        public BooksReportsController(LibraryDBContext context)
        {
            _context = context;
        }
        [HttpGet("top-borrowed")]
        public async Task<IActionResult> GetBorrowedReports([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] int top)
        {
            var books = _context.Books.ToList();
            var loans = _context.Loans.ToList();
            var filteredLoans = loans.FindAll(l => l.LoanDate >= fromDate && l.LoanDate <= toDate);
            var filteredBookIds = new Dictionary<int, int>();
            foreach (Loan loan in filteredLoans) 
            {
                filteredBookIds.TryAdd(loan.BookId, 0);
                filteredBookIds[loan.BookId]++;
            }
            int currentSize = 0;
            var results = new List<Object>();
            foreach (int id in filteredBookIds.Keys)
            {
                if (currentSize == top)
                {
                    break;
                }
                results.Add(new
                {
                    BookId = id,
                    Title = books.Find(b => b.BookId == id).Title,
                    BorrowedTimes = filteredBookIds[id],

                });

                currentSize++;
            }
            return Ok(results);

        }

    }
}

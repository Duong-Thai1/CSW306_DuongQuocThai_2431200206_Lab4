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
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoanDetails(int id)
        {
            var loans = _context.Loans.ToList();
            // TODO: Use Dictionary to store User as key and List of borrowed books as value
            return Ok();
        }
    }
}

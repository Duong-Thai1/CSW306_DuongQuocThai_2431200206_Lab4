using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuongQuocThai_2431200206_Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private LibraryDBContext _context;
        public AuthorsController(LibraryDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = _context.Authors.ToList();
            if (authors.Count == 0)
            {
                return NotFound();
            }
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var authors = _context.Authors.ToList();
            var author = authors.Find(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> addAuthor([FromBody] Author author)
        {
            author.AuthorId = null;
            author.CreatedDate = DateTime.Now;
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateAuthor(int id, [FromBody] Author newAuthor)
        {
            var authors = _context.Authors.ToList();
            var author = authors.Find(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            author.FirstName = newAuthor.FirstName;
            author.LastName = newAuthor.LastName;
            author.DateOfBirth = newAuthor.DateOfBirth;
            author.Biography = newAuthor.Biography;
            author.Nationality = newAuthor.Nationality;
            author.Email = newAuthor.Email;
            author.Website = newAuthor.Website;
            author.IsActive = newAuthor.IsActive;
            author.Avatar = newAuthor.Avatar;
            await _context.SaveChangesAsync();
            return Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var authors = _context.Authors.ToList();
            var author = authors.Find(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            author.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

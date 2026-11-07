using DuongQuocThai_2431200206_Lab4.Dtos;
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

        [HttpPost("uploadAvatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] AuthorSubmissionDto submission)
        {
            var imageFile = submission.ImageFile;
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest();
            }
            var fileStream = new FileStream($"wwwroot\\carousel_images\\{submission.Name}", FileMode.Create);
            await imageFile.CopyToAsync(fileStream);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Author author)
        {
          
            author.AuthorId = null;
            author.CreatedDate = DateTime.Now;
            // Check for cases where author get to choose a list of default avatars
            if (author.Avatar != null)
            {
                var avatar = _context.Carousels.ToList().Find(item => item.ImageUrl == author.Avatar);
                if (avatar == null)
                {
                    return BadRequest();
                }
            }
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author newAuthor)
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

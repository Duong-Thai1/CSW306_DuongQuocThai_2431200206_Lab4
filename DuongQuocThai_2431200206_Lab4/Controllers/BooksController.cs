using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuongQuocThai_2431200206_Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDBContext _context;
        public BooksController(LibraryDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            List<Book> books = _context.Books.ToList();
            if (books.Count == 0)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet("title/{title}")]
        public IActionResult GetBooksByTitle(String title)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> foundBooks = books.FindAll(b => b.Title.Contains(title));
            if (foundBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBooks);
        }

        [HttpGet("categoryId/{categoryId}")]
        
        public IActionResult GetBooksByCategoryId(int categoryId)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> foundBooks = books.FindAll(b => b.CategoryId == categoryId);
            if (foundBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBooks);
        }

        [HttpGet("authorId/{authorId}")]
        public IActionResult GetBooksByAuthorId(int authorId)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> foundBooks = books.FindAll(b => b.AuthorId == authorId);
            if (foundBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBooks);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var books = _context.Books.ToList();
            var book = books.Find(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book) 
        {
            var authorID = book.AuthorId;
            var categoryID = book.CategoryId;

            var categories = _context.Categories.ToList();
            var authors = _context.Authors.ToList();

            Category category = categories.Find((c) => c.CategoryId == categoryID);
            book.Category = category;
            Author author = authors.Find((a) => a.AuthorId == authorID);
            book.Author = author;
            book.BookId = null;

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return Ok(book);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book newBook)
        {
            var books = _context.Books.ToList();
            var book = books.Find(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Author = newBook.Author;
            _context.Entry(book).State = EntityState.Modified; 

            await _context.SaveChangesAsync();
            return Ok(book);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var books = _context.Books.ToList();
            var book = books.Find(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            book.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

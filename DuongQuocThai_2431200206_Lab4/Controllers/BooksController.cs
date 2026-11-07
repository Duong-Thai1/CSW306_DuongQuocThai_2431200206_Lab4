using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<IActionResult> GetBooks()
        {
            List<Book> books = _context.Books.ToList();
            if (books.Count == 0)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet("filterByTitle/{title}")]
        public async Task<IActionResult> GetBooksByTitle(String title)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> foundBooks = books.FindAll(b => b.Title.Contains(title));
            if (foundBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBooks);
        }

        [HttpGet("filterByCategoryId/{categoryId}")]
        
        public async Task<IActionResult> GetBooksByCategoryId(int categoryId)
        {
            List<Book> books = _context.Books.ToList();
            List<Book> foundBooks = books.FindAll(b => b.CategoryId == categoryId);
            if (foundBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundBooks);
        }

        [HttpGet("filterByAuthorId/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthorId(int authorId)
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
        public async Task<IActionResult> GetBook(int id)
        {
            var books = _context.Books.ToList();
            var book = books.Find(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("{id}/read")]
        public async Task<IActionResult> ReadBook(int id)
        {
            var books = _context.Books.ToList();
            var book = books.Find(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            if (book.Pdf == null)
            {
                return NoContent();
            }
            return Ok(new
            {
                Url = $"book_pdfs/{book.Pdf}",
            });
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> getDeletedBooks()
        {
            var books = _context.Books.ToList();
            var deletedBooks = books.FindAll(b => !b.IsActive);
            if (deletedBooks.Count == 0)
            {
                return NotFound();
            }
            return Ok(deletedBooks);
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

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> RestoreBook(int id)
        {
            var books = _context.Books.ToList();
            var book = books.Find(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            book.IsActive = true;
            await _context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteBook(int id)
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

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDeleteBook(int id)
        {
            var books = _context.Books.ToList();
            var book = books.Find(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            books.Remove(book);
            await _context.SaveChangesAsync();
            var deletedBook = books.Find(b => b.BookId == id);
            if (deletedBook == null)
            {
                if (book.Pdf != null)
                {
                    System.IO.File.Delete($"wroot\\book_pdfs\\{book.Pdf}");
                }
                if (book.Avatar != null)
                {
                    System.IO.File.Delete($"wroot\\carousel_images\\{book.Avatar}");
                }
            }
            return NoContent();
        }
    }
}

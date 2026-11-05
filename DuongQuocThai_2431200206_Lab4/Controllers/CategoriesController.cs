using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DuongQuocThai_2431200206_Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private LibraryDBContext _context;
        public CategoriesController(LibraryDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = _context.Categories.ToList();
            if (categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetCategories(String name)
        {
            var categories = _context.Categories.ToList();
            var foundCategories = categories.FindAll(c => c.Name.ToLower().Contains(name.ToLower()));
            if (foundCategories.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundCategories);
        }

        [HttpGet("filterByActive")]
        public async Task<IActionResult> GetActiveCategories()
        {
            var categories = _context.Categories.ToList();
            var foundCategories = categories.FindAll(c => c.IsActive);
            if (foundCategories.Count == 0)
            {
                return NotFound();
            }
            return Ok(foundCategories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category newCategory)
        {
            var categories = _context.Categories.ToList();
            var category = categories.Find(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            category.Description = newCategory.Description;
            category.UpdatedDate = DateTime.Now;
            category.IsActive = newCategory.IsActive;
            category.Avatar = newCategory.Avatar;
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> ToggleCategoryById(int id)
        {
            var categories = _context.Categories.ToList();
            var category = categories.Find(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsActive = !category.IsActive;
            await _context.SaveChangesAsync();
            return Ok(category);
        }

    }
}

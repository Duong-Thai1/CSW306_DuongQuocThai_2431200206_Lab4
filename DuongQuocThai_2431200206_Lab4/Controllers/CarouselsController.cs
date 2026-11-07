using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DuongQuocThai_2431200206_Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarouselsController : ControllerBase
    {
        private LibraryDBContext _context;
        public CarouselsController(LibraryDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCarouselItems()
        {
            List<Carousel> items = _context.Carousels.ToList();
            if (items.Count == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarouselItemById(int id)
        {
            List<Carousel> items = _context.Carousels.ToList();
            Carousel item = items.Find(i => i.CarouselId == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarouselItem( [FromBody] Carousel item)
        {
            item.CreatedDate = DateTime.Now;
            item.UpdatedDate = DateTime.Now;
            if (item == null)
            {
                return BadRequest();
            }
            _context.Carousels.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarouselItem(int id, [FromBody] Carousel newItem)
        {
            List<Carousel> items = _context.Carousels.ToList();
            Carousel item = items.Find(i => i.CarouselId == id);
            if (item == null)
            {
                return BadRequest();
            }
            item.ImageUrl = newItem.ImageUrl; 
            item.Title = newItem.Title;
            item.Description = newItem.Description;
            item.LinkUrl = newItem.LinkUrl;
            item.Order = newItem.Order;
            item.IsActive = newItem.IsActive;
            item.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarouselItem(int id)
        {
            List<Carousel> items = _context.Carousels.ToList();
            Carousel item = items.Find(i => i.CarouselId == id);
            if (item == null)
            {
                return BadRequest();
            }
            _context.Carousels.Remove(item);
            await _context.SaveChangesAsync();
            Carousel deletedItem = _context.Carousels.ToList().Find(i => i.CarouselId == id);
            if (deletedItem == null)
            {
                System.IO.File.Delete($"wwwroot\\carousel_images\\{item.ImageUrl}");
            }
            return NoContent();
        }
    }
}

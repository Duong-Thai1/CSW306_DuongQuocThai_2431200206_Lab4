using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace DuongQuocThai_2431200206_Lab4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private LibraryDBContext _context;
        public UsersController(LibraryDBContext context)
        {
            _context = context;
        }
    }
}

using DuongQuocThai_2431200206_Lab4.Models;
using DuongQuocThai_2431200206_Lab4.Models.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using BCrypt.Net;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest();
            }

            var oldUser = _context.Users.ToList().Find(u => u.Email == newUser.Email);
            if (oldUser != null)
            {
                return Conflict("Email is already used");
            }
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.CreatedDate = DateTime.Now;
            newUser.UserCode = Guid.NewGuid().ToString();
            newUser.IsLocked = false;
            newUser.IsDeleted = false;
            newUser.IsActive = false;
            newUser.ActiveCode = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok("User is registered");
        }

        [HttpGet("activate")]
        public async Task<IActionResult> ActivateAccount([FromQuery] int userId, [FromQuery] string code)
        {
            int decodedCode = System.Int32.Parse(code);
            if (decodedCode < 100000 && decodedCode > 1000000) 
            {
                return BadRequest();
            }
            var user = _context.Users.ToList().Find(u => u.UserId == userId);
            if (user == null)
            {
                return BadRequest();
            }
            if (user.ActiveCode == null || !user.ActiveCode.Equals(code))
            {
                return BadRequest();
            }
            user.IsActive = true;
            await _context.SaveChangesAsync();

            return Ok("Account is activated");
        }
    }
}

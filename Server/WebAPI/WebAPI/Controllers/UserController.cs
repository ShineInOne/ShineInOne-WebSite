using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs.User;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // ----------------------------------------------------
        // 1️⃣ Register User
        // ----------------------------------------------------
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            // Check if email already exists
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("Email already registered");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Phone = dto.Phone,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "User registered successfully",
                user.Id,
                user.FullName,
                user.Email
            });
        }

        // ----------------------------------------------------
        // 2️⃣ Login
        // ----------------------------------------------------
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var user =_context.Users
                .FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid email or password");

            var hashedPassword = PasswordHasher.Hash(dto.Password);

            if (user.PasswordHash != hashedPassword)
                return Unauthorized("Invalid email or password");

            return Ok(new
            {
                message = "Login successful",
                user.Id,
                user.FullName,
                user.Email
            });
        }

        // ----------------------------------------------------
        // 3️⃣ Get All Users (Admin use)
        // ----------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users =_context.Users
                .Select(u => new
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.Phone
                })
                .ToList();

            return Ok(users);
        }

        // ----------------------------------------------------
        // 4️⃣ Get User By Id
        // ----------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user =  _context.Users.Find(id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
    }
}

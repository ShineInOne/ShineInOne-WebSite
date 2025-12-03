using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs.Categories;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(CategoryDto dto)
        {
            var category = new Categories
            {
                Name = dto.Name,
                Description = dto.Description
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Category created successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryDto dto)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = dto.Name;
            category.Description = dto.Description;
            _context.SaveChanges();
            return Ok("Category updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok("Category deleted successfully");
        }
    }
}

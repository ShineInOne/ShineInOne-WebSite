using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs.Products;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(ProductDto dto)
        {
            var product = new Products
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Product created succesfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDto dto)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.StockQuantity = dto.StockQuantity;
            product.ImageUrl = dto.ImageUrl;
            product.CategoryId = dto.CategoryId;
            _context.SaveChanges();
            return Ok("Product updated succesfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Product deleted succesfully");
        }
    }
}

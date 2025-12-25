using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs.Cart;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // ----------------------------------------------------
        // 1️⃣ Add Item to Cart
        // ----------------------------------------------------
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product == null)
                return NotFound("Product not found.");

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found.");

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c =>
                    c.UserId == dto.UserId &&
                    c.ProductId == dto.ProductId
                );

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                await _context.SaveChangesAsync();
                return Ok(existingItem);
            }

            var cartItem = new CartItem
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Quantity = 1,
                CreatedAt = DateTime.UtcNow
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return Ok(cartItem);
        }


        // ----------------------------------------------------
        // 2️⃣ Get Cart Items for User
        // ----------------------------------------------------
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var cart = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .Select(c => new
                {
                    c.Id,
                    c.Quantity,
                    Product = new
                    {
                        c.Product.Id,
                        c.Product.Name,
                        c.Product.Price,
                        c.Product.ImageUrl
                    }
                })
                .ToListAsync();

            return Ok(cart);
        }


        // ----------------------------------------------------
        // 3️⃣ Update Cart Item Quantity
        // ----------------------------------------------------
        [HttpPut("update")]
        public async Task<IActionResult> UpdateQuantity(UpdateCartDto dto)
        {
            var item = await _context.CartItems.FindAsync(dto.CartItemId);

            if (item == null)
                return NotFound("Cart item not found");

            if (dto.Quantity <= 0)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
                return Ok(null); // item removed
            }

            item.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();

            return Ok(item); // return updated item
        }


        // ----------------------------------------------------
        // 4️⃣ Remove Item from Cart
        // ----------------------------------------------------
        [HttpDelete("remove/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);

            if (item == null)
                return NotFound("Cart item not found");

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();   // 🔥 FIX

            return Ok(item); // return removed item (optional but useful)
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var items = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();   // 🔥 async

            if (!items.Any())
                return Ok("Cart already empty");

            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();   // 🔥 FIX

            return Ok(items.Count);
        }


    }
}
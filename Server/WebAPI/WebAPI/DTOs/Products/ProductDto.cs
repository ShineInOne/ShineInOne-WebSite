using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Products
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}

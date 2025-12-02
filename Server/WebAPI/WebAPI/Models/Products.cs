namespace WebAPI.Models
{
    public class Products
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public required decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
    }
}

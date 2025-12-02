namespace WebAPI.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public List<Products> Products { get; set; }
    }
}

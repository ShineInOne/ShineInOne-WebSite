using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class CartItem
    {
        
        public int Id { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public int ProductId { get; set; }

        [JsonIgnore]
        public Products Product { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

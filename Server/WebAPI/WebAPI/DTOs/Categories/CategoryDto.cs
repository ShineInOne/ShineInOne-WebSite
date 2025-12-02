using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Categories
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

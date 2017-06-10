using System.ComponentModel.DataAnnotations;

namespace Inventory.WebApi.Models
{
    public class ProductForPostDto
    {
        [Required]
        [MaxLength(200)]
        public string Image { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double Price { get; set; }

        public int ProductCategoryId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Stock Amount must be a positive number")]
        public int StockAmount { get; set; }
    }
}
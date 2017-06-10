using System.ComponentModel.DataAnnotations;

namespace Inventory.WebApi.Models
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
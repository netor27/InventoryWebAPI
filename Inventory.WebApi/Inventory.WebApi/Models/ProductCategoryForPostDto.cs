using System.ComponentModel.DataAnnotations;

namespace Inventory.WebApi.Models
{
    public class ProductCategoryForPostDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}

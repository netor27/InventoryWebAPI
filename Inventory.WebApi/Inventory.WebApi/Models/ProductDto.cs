namespace Inventory.WebApi.Models
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Image { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int ProductCategoryId { get; set; }
        public int StockAmount { get; set; }
    }
}
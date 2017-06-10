using Inventory.WebApi.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.WebApi.Extensions
{
    public static class ProductInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this ProductInfoContext context)
        {
            if (!context.ProductCategories.Any() && !context.Products.Any())
            {
                var productCategories = new List<ProductCategory>()
                {
                    new ProductCategory()
                    {
                        Name = "Sodas",
                        Products = new List<Product>()
                        {
                            new Product()
                            {
                                Name = "Coke",
                                Image = "http://www.coca-colaproductfacts.com/content/dam/productfacts/us/productDetails/ProductImages/Coke_12oz.png",
                                Price = 10.5d,
                                StockAmount = 100
                            },
                            new Product()
                            {
                                Name = "Pepsi",
                                Image = "https://www.caffeineinformer.com/wp-content/caffeine/pepsi-cola.jpg",
                                Price = 12.5d,
                                StockAmount = 50
                            },
                            new Product()
                            {
                                Name = "Dr. Pepper",
                                Image = "https://i5.walmartimages.com/asr/401aa80d-51fe-4ce3-bd20-0d464dee9b3a_1.19f858aeafc8573590179c137f444fc7.jpeg",
                                Price = 9.5d,
                                StockAmount = 75
                            },
                        }
                    },
                    new ProductCategory()
                    {
                        Name = "Chocolates",
                        Products = new List<Product>()
                        {
                            new Product()
                            {
                                Name = "Snickers",
                                Image = "https://www.snickers.com/Resources/images/nutrition/products/large/1_Snickers.jpg",
                                Price = 5.55d,
                                StockAmount = 600
                            },
                            new Product()
                            {
                                Name = "M&M's",
                                Image = "http://www.mms.com/Resources/img/nutrition/im-mms.png",
                                Price = 25.5d,
                                StockAmount = 400
                            },
                            new Product()
                            {
                                Name = "Reese's",
                                Image = "https://upload.wikimedia.org/wikipedia/en/9/97/Reese%27s-PB-Cups-Wrapper-Small.png",
                                Price = 9.5d,
                                StockAmount = 250
                            },
                          }
                    },
                    new ProductCategory()
                    {
                        Name = "Detergents",
                        Products = new List<Product>()
                        {
                            new Product()
                            {
                                Name = "Tide",
                                Image = "http://www.southernsavers.com/wp-content/uploads/2013/01/Tide-Liquid-Detergent.jpg",
                                Price = 55.55d,
                                StockAmount = 50
                            },
                            new Product()
                            {
                                Name = "Gain",
                                Image = "http://ghk.h-cdn.co/assets/cm/15/11/480x574/54feec56539e5-ghk-6-gain-original-laundry-detergent-iz0fiw-s2.jpg",
                                Price = 45d,
                                StockAmount = 150
                            },
                            new Product()
                            {
                                Name = "Ajax",
                                Image = "http://www.stain-removal-101.com/images/ajax-detergent-review-great-value-for-the-price-21476807.jpg",
                                Price = 59.5d,
                                StockAmount = 100
                            },
                        }
                    }
                };

                context.ProductCategories.AddRange(productCategories);
                context.SaveChanges();
            }
        }
    }
}
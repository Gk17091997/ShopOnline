using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Extensions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDto> ConvertDto(this IEnumerable<Product> products, IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDto
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Qty = product.Qty,
                        CategoryId = product.CategoryId,
                        CategoryName = productCategory.Name
                    }).ToList();
        }
        public static ProductDto ConvertDto(this Product product, ProductCategory productCategory)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name
            };
        }

        public static IEnumerable<CartItemDto> ConvertDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return (from cartitem in cartItems
                    join product in products on
                    cartitem.ProductId equals product.Id
                    select new CartItemDto
                    {
                        Id = cartitem.Id,
                        ProductId = cartitem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartitem.CartId,
                        Qty = cartitem.Qty,
                        TotalPrice = product.Price * cartitem.Qty,
                    }).ToList();
        } 
        public static CartItemDto ConvertDto(this CartItem cartitem, Product product)
        {
            return ( new CartItemDto
                    {
                        Id = cartitem.Id,
                        ProductId = cartitem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartitem.CartId,
                        Qty = cartitem.Qty,
                        TotalPrice = product.Price * cartitem.Qty,
                    });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;

namespace ShopOnline.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDb;

        public ProductRepository(ShopOnlineDbContext shopOnlineDb)
        {
            this.shopOnlineDb = shopOnlineDb;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
           var categories = await shopOnlineDb.ProductCategories.ToListAsync();
            return categories;
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var productCategory = await shopOnlineDb.ProductCategories.SingleOrDefaultAsync(x=>x.Id == id);
            return productCategory;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await shopOnlineDb.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var Products = await shopOnlineDb.Products.ToListAsync();
            return Products;
        }
    }
}

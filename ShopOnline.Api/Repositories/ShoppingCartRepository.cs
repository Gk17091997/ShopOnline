using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext dbContext;

        public ShoppingCartRepository(ShopOnlineDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private async Task<bool> CartItemExist(int CartId,int ProductId)
        {
            return await dbContext.CartItems.AnyAsync(x => x.CartId == CartId && x.ProductId == ProductId);
        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAdd)
        {
            if(await CartItemExist(cartItemToAdd.CartId, cartItemToAdd.ProductId) == false)
            {
                var item = await (from prod in dbContext.Products
                            where prod.Id == cartItemToAdd.ProductId
                            select new CartItem
                            {
                                CartId = cartItemToAdd.CartId,
                                ProductId = cartItemToAdd.ProductId,
                                Qty = cartItemToAdd.Qty
                            }).SingleOrDefaultAsync();
                if (item != null)
                {
                    var result = await dbContext.CartItems.AddAsync(item);
                    dbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }
            return null;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in dbContext.Carts
                          join cartitem in dbContext.CartItems on
                          cart.Id equals cartitem.CartId
                          where cartitem.Id == id
                          select new CartItem
                          {
                              Id = cartitem.Id,
                              ProductId = cartitem.ProductId,
                              Qty = cartitem.Qty,
                              CartId = cartitem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in dbContext.Carts
                          join cartitem in dbContext.CartItems on
                          cart.Id equals cartitem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartitem.Id,
                              ProductId = cartitem.ProductId,
                              Qty = cartitem.Qty,
                              CartId = cartitem.CartId
                          }).ToListAsync();
        }

        public Task<CartItem> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}

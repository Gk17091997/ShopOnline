using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient client;

        public ShoppingCartService(HttpClient client)
        {
            
        }
        public async Task<CartItemDto> AddItem(CartItemDto cartItemDto)
        {
            try
            {
                var response = await client.PutAsJsonAsync<CartItemDto>("api/ShoppingCart",cartItemDto);
                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(CartItemDto);
                }

                return await response.Content.ReadFromJsonAsync<CartItemDto>();
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public async Task<IEnumerable<CartItemDto>> GetItems(int userId)
        {
            return await client.GetFromJsonAsync<IEnumerable<CartItemDto>>("api/ShoppingCart/{UserId}/GetItems");
        }
    }
}

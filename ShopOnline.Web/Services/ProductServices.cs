using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class ProductServices : IProductServices
    {
        private readonly HttpClient client;

        public ProductServices(HttpClient client)
        {
            this.client = client;
        }
        public async Task<IEnumerable<ProductDto>> GetProduct()
        {
            return await client.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            return await client.GetFromJsonAsync<ProductDto>($"api/Product/{id}");
        }
    }
}

using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetProduct();
        Task<ProductDto> GetProduct(int id);
    }
}

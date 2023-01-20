using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductDetailBase:ComponentBase
    {
        [Inject]
        public IProductServices ProductServices { get; set; }

        [Parameter]
        public int id { get; set; }

        public ProductDto product { get; set; }
        protected override async Task OnInitializedAsync()
        {
            product = await ProductServices.GetProduct(id);
        }
    }
}

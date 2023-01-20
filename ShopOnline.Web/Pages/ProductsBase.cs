using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductServices productServices { get; set; }

        public IEnumerable<ProductDto> products { get; set; }

        protected async override Task OnInitializedAsync()
        {
            products = (await productServices.GetProduct()).ToList();
        }
        protected IOrderedEnumerable<IGrouping<int,ProductDto>> GetGroupedProductByCategory()
        {
            return (from prod in products
                    group prod by prod.CategoryId into prodByCatGrp
                    orderby prodByCatGrp.Key
                    select prodByCatGrp
                    );
        }
        protected string GetCategoryName(IGrouping<int,ProductDto> productDtos)
        {
            return productDtos.FirstOrDefault(x => x.CategoryId == productDtos.Key).CategoryName;
        }
    }
}

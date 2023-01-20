using Microsoft.AspNetCore.Components;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Pages
{
    public class DisplayProductBase : ComponentBase
    {
        [Parameter]
        public IEnumerable<ProductDto> products { get; set; }
    }
}

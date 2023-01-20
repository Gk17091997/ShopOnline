using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var product = await repository.GetItems();
                var productCategories = await repository.GetCategories();
                if (product == null|| productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDto = product.ConvertDto(productCategories);
                    return Ok(productDto);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retriveng data from database");
            }
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await repository.GetItem(id);
                if(product == null)
                {
                    return BadRequest();
                }
                else
                {
                    var productCategory = await repository.GetCategory(product.CategoryId);
                    var productDto = product.ConvertDto(productCategory);
                    return productDto;
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retriveng data from database");
            }
        }
    }
}

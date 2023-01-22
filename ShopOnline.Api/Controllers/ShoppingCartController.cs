using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Repositories.Contracts;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository,IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("{UserId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                var cartItem = await shoppingCartRepository.GetItems(userId);
                var product = await productRepository.GetItems();
                if(cartItem == null || product == null)
                {
                    return NotFound();
                }
                var result =  cartItem.ConvertDto(product);
                return Ok(result);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retriveng data from database");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDto>> Getitem(int id)
        {
            try
            {
                var cartItem = await shoppingCartRepository.GetItem(id);
                var product = await productRepository.GetItem(cartItem.ProductId);
                if (cartItem == null || product == null)
                {
                    return NotFound();
                }
                var result = cartItem.ConvertDto(product);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retriveng data from database");
            } 
        }
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAdd)
        {
            try
            {
                var newCartItem = await shoppingCartRepository.AddItem(cartItemToAdd);
                if(newCartItem == null)
                {
                    return NoContent();
                }
                var product = await productRepository.GetItem(newCartItem.ProductId);
                if(product == null)
                {
                    throw new Exception("something went worng while retriving data");
                }
                var newCartItemDto = newCartItem.ConvertDto(product);
                return CreatedAtAction(nameof(Getitem), new {id = newCartItemDto.Id},newCartItemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
  
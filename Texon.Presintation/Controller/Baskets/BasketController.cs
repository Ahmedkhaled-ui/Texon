using Microsoft.AspNetCore.Mvc;
using Texon.Service.Abstraction.IService;
using Texon.Shared.BasketsDto;

namespace Texon.Presentation.Controller.Baskets
{
    public class BasketController(IBasketService basketService) : ApiBaseController
    {

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdateBasket(CustomerBasketDto customerBasketDto, CancellationToken cancellationToken)
        {
            var result = await basketService.CreateOrUpdateBasketAsync(customerBasketDto, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetBasketById(string id, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasketByIdAsync(id, cancellationToken);
            if (basket == null)
            {
                return NotFound();
            }
            return Ok(basket);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBasket(string id, CancellationToken cancellationToken)
        {
            var result = await basketService.DeleteBasketAsync(id, cancellationToken);
            // return Ok(result);                                   
            return NoContent();
        }
    }
}

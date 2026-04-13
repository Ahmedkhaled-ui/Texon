using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Texon.Service.Abstraction.IService;
using Texon.Shared.AddressDTo;
using Texon.Shared.OrderDto;

namespace Texon.Presentation.Controller.Orders
{
    public class OrdersController(IOrderService orderService) : ApiBaseController
    {
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(int deliveryMethodId, string basketId, AddressDto shippingAddress)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue("email");
            if (string.IsNullOrEmpty(Email))
                return BadRequest("User email not found in token");
            var order = await orderService.CreateOrderAsync(Email, deliveryMethodId, basketId, shippingAddress);
            return HandleResult(order);

        }

        [HttpGet("get-all-zones")]
        public async Task<ActionResult<IEnumerable<Domin.Entities.DeliveryMethod.DeliveryMethods>>> GetAllZones()
        {

            var result = await orderService.GetDeliveryMethodsAsync();
            return Ok(result);
        }

        [HttpGet("get-all-Orders")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders([FromQuery] OrderQuary OrderQuary)
        {
             var result = await orderService.GetAllOrdersAsync("ar", OrderQuary);
            return Ok(result);
        }
        [HttpGet("{id},{Email}")]
        [Authorize]
        public async Task<ActionResult<OrderDto>> GetOrderById(Guid id, string UserEmail)
        {
            var result = await orderService.GetOrderByIdAsync(id, UserEmail);
            return HandleResult(result!);
        }

    }
}

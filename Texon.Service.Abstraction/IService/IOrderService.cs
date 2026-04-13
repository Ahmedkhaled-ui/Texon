using E_Commerce.Shared;
using Texon.Domin.Entities.Auth;
using Texon.Domin.Entities.DeliveryMethod;
using Texon.Service.Abstraction.Common;
using Texon.Shared.AddressDTo;
using Texon.Shared.OrderDto;
using Texon.Shared.ProductDto;

namespace Texon.Service.Abstraction.IService
{
    public interface IOrderService
    {
        Task<PagenatedResult<OrderDto>> GetAllOrdersAsync(string lang, OrderQuary OrderQuary);

        Task<Result<OrderDto>> CreateOrderAsync(string Email, int deliveryMethodId, string basketId, AddressDto shippingAddress);
        Task<Result<OrderDto>?> GetOrderByIdAsync(Guid id, string UserEmail);
        Task<IEnumerable<DeliveryMethods>> GetDeliveryMethodsAsync();
    }
}

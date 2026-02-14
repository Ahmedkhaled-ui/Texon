using Texon.Shared.BasketsDto;

namespace Texon.Service.Abstraction.IService
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketByIdAsync(string id, CancellationToken cancellationToken);
        Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto customerBasketDto, CancellationToken cancellationToken);
        Task<bool> DeleteBasketAsync(string id, CancellationToken cancellationToken);
    }
}

using AutoMapper;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Baskets;
using Texon.Service.Abstraction.IService;
using Texon.Shared.BasketsDto;

namespace Texon.Service.Service
{
    public class BasketService(IBasketRepository repository , IMapper mapper) : IBasketService
    {
        public async Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto customerBasketDto, CancellationToken cancellationToken)
        {
            try
            {
                var basket = mapper.Map<CustomerBasket>(customerBasketDto);
                var update = await repository.UpdateBasketAsync(basket);
                return mapper.Map<CustomerBasketDto>(update);
            }
            catch (Exception)
            {

                throw;
            }


        }

        public Task<bool> DeleteBasketAsync(string id, CancellationToken cancellationToken)
       => repository.DeleteBasketAsync(id);
        public async Task<CustomerBasketDto> GetBasketByIdAsync(string id, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasketAsync(id);
            return mapper.Map<CustomerBasketDto>(basket);



        }
    }
}

using Texon.Service.Abstraction.Common;
using Texon.Shared.DeliveryMethodDto;

namespace Texon.Service.Abstraction.IService
{
    public interface IDeliveryMethodService
    {
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodAsync();
        Task<Result<DeliveryMethodDto>> GetDeliveryMethodByIdAsunc(int id );
        Task<bool> CreateAsync(DeliveryMethodDto dto); 
        Task<Result<bool>> UpdateAsync(int id, DeliveryMethodDto dto);
        Task<Result<bool>> DeleteAsync(int id); 
    }
}

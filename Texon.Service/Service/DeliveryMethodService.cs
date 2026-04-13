using AutoMapper;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.DeliveryMethod;
using Texon.Service.Abstraction.Common;
using Texon.Service.Abstraction.IService;
using Texon.Shared.DeliveryMethodDto;

namespace Texon.Service.Service
{
    public class DeliveryMethodService(IUnitofWork unitofWork , IMapper mapper) : IDeliveryMethodService
    {
        public async Task<bool> CreateAsync(DeliveryMethodDto dto)
        {
            if (dto == null)
                return false;

            var method = mapper.Map<DeliveryMethods>(dto);
            await unitofWork.GetRepository<DeliveryMethods, int>().AddAsync(method);
            return await unitofWork.SaveChangesAsync() > 0;

        }

       

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodAsync()
        {
            var result = await unitofWork.GetRepository<DeliveryMethods, int>().GetAllAsync();
            if (result == null)
                return null;

            return mapper.Map<IEnumerable<DeliveryMethodDto>>(result);
        }

        public async Task<Result<DeliveryMethodDto>> GetDeliveryMethodByIdAsunc(int id)
        {
            var result = await unitofWork.GetRepository<DeliveryMethods, int>().GetByIdAsync(id);
            if (result == null)
                return Error.NotFound("", "");

            return mapper.Map<DeliveryMethodDto>(result);
        }

        public async Task<Result<bool>> UpdateAsync(int id, DeliveryMethodDto dto)
        {
            var result = await unitofWork.GetRepository<DeliveryMethods, int>().GetByIdAsync(id);
            if (result == null)
                return Error.NotFound("Method Not Found", "هذه المنطقة غير موجودة");

            result.Cost = dto.Cost;
            result.Description = dto.Description;
            result.ShortName = dto.ShortName;
            result.DeliveryTime = dto.DeliveryTime;

            unitofWork.GetRepository<DeliveryMethods,int>().update(result);

            return await unitofWork.SaveChangesAsync() > 0;

        }


        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var repo = unitofWork.GetRepository<DeliveryMethods, int>();
            var method = await repo.GetByIdAsync(id);

            if (method == null) return Error.NotFound("Method Not Found", "المنطقة غير موجودة");

            repo.Remove(method);
            return await unitofWork.SaveChangesAsync() > 0;
        }
    }
}

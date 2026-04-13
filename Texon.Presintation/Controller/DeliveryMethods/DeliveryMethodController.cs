using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Texon.Service.Abstraction.IService;
using Texon.Shared.DeliveryMethodDto;

namespace Texon.Presentation.Controller.DeliveryMethods
{
    public class DeliveryMethodController(IDeliveryMethodService deliveryMethod) : ApiBaseController
    {

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CreateDeliveryMethod(DeliveryMethodDto dto)
        {
            var result = await deliveryMethod.CreateAsync(dto);
            return Ok(result);

        }
        [HttpPut]
        [Authorize]

        public async Task<ActionResult> UpdateDeliveryMethod(DeliveryMethodDto dto , int id)
        {
            var result = await deliveryMethod.UpdateAsync(id, dto);
            return HandleResult(result);

        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllDeliveryMethod()
        {
            var result = await deliveryMethod.GetAllDeliveryMethodAsync();
            return Ok(result);

        }
        [HttpGet("Id")]
        public async Task<ActionResult> GetDeliveryMethodById(int id)
        {
            var result = await deliveryMethod.GetDeliveryMethodByIdAsunc(id);
            return HandleResult(result);

        }
        [HttpDelete]
        [Authorize]

        public async Task<ActionResult> DeleteDeliveryMethod(int id)
        {
            var result = await deliveryMethod.DeleteAsync(id);
            return HandleResult(result);

        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Texon.Service.Abstraction.IService;
using Texon.Shared.AddressDTo;
using Texon.Shared.AuthDtos;

namespace Texon.Presentation.Controller.Users
{
    [Authorize]
    public class UserController(IUserService userService):ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetUser()
        {
            string email = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await userService.GetByEmailAsync(email);
            return HandleResult(user);
        }

        [HttpGet("Address")]
        public async Task <ActionResult<AddressDto>> GetAddress()
        {

            string email = User.FindFirstValue(ClaimTypes.Email)!;
            var user = await userService.GetAddressAsync(email);
            return HandleResult(user);

        }

        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            string email = User.FindFirstValue(ClaimTypes.Email)!;
            var result = await userService.UpdateAddressAsync( address, email);
            return HandleResult(result);

        }

    }
}

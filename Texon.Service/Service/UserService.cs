using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Texon.Domin.Entities.Auth;
using Texon.Service.Abstraction.Common;
using Texon.Service.Abstraction.IService;
using Texon.Service.Contracts;
using Texon.Shared.AddressDTo;
using Texon.Shared.AuthDtos;

namespace Texon.Service.Service
{
    public class UserService(UserManager<ApplicationUser> userManager , ITokenService tokenService
,        IMapper mapper) : IUserService
    {
        public async Task<Result<UserResponse>> GetByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return Error.NotFound("User Not Found", $"User With Email {email} Was Not Found");

            var roles = await userManager.GetRolesAsync(user);

            var token = tokenService.GetToken(user, roles);
            return new UserResponse(user.Email, user.UserName , token);
                }
        public async Task<Result<AddressDto>> GetAddressAsync(string email)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
                  if (user == null)
                return Error.NotFound("Address Not Found", $"User With Email {email} Dose Not have Address");

                  return mapper.Map<AddressDto>(user.Address);


        }


        public async Task<Result<AddressDto>> UpdateAddressAsync(AddressDto addressDto, string email)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return Error.NotFound("Address Not Found", $"User With Email {email} Dose Not have Address");

            if(user.Address is not null)
            {
                user.Address.FirstName=addressDto.FirstName;
                user.Address.LastName=addressDto.LastName;
                user.Address.City=addressDto.City;
                user.Address.Street=addressDto.Street;
                user.Address.Country=addressDto.Country;
            }
            else
            {
                user.Address=mapper.Map<Address>(addressDto);
            }
            await userManager.UpdateAsync(user);
            return mapper.Map<AddressDto>(user.Address );

        }
    }
}

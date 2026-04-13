using Texon.Service.Abstraction.Common;
using Texon.Shared.AddressDTo;
using Texon.Shared.AuthDtos;

namespace Texon.Service.Abstraction.IService
{
    public interface IUserService
    {
        Task<Result<UserResponse>> GetByEmailAsync(string email);
        Task<Result<AddressDto>> GetAddressAsync(string email);
        Task<Result<AddressDto>> UpdateAddressAsync(AddressDto addressDto , string Email);
    }
}

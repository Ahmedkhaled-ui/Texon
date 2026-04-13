using Texon.Service.Abstraction.Common;
using Texon.Shared.AuthDtos;

namespace Texon.Service.Abstraction.IService
{
    public interface IAuthService
    {
        Task<Result<UserResponse>> RegisterAsync(RegisterRequest request);
        Task<Result<UserResponse>>LoginAsync(LoginRequest request);
        Task<bool> CheckEmailAsync(string email);
    }
}

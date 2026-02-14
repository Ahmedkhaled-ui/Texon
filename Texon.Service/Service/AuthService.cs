using Microsoft.AspNetCore.Identity;
using Texon.Domin.Entities.Auth;
using Texon.Service.Abstraction.Common;
using Texon.Service.Abstraction.IService;
using Texon.Shared.AuthDtos;

namespace Texon.Service.Service
{
    public class AuthService(UserManager<ApplicationUser> userManager) : IAuthService
    {
        public async Task<Result<UserResponse>> LoginAsync(LoginRequest request)
        {

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
       return Error.Failure("UserNotFound", "No user found with the provided email.");
            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
         return Error.Unauthorized("InvalidCredentials", "The provided password is incorrect.");

            return new UserResponse
            (
              user.Email,
                user.UserName,
                 "dummy"
                );
        }

        public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest request)
        {

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
                PhoneNumber = request.phoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return new UserResponse

                           (
                  user.Email,
                    user.UserName,
                     "dummy"
                    );
            }
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));


        }
    }
    
}

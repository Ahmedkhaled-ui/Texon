using Microsoft.AspNetCore.Identity;
using Texon.Domin.Entities.Auth;
using Texon.Service.Abstraction.Common;
using Texon.Service.Abstraction.IService;
using Texon.Service.Contracts;
using Texon.Shared.AuthDtos;

namespace Texon.Service.Service
{
    public class AuthService(UserManager<ApplicationUser> userManager , ITokenService tokenService) : IAuthService
    {
        public async Task<bool> CheckEmailAsync(string email)
     => await userManager.FindByEmailAsync(email) != null;

        public async Task<Result<UserResponse>> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
                return Error.Unauthorized("SignInErrors", "Invalid email or password.");

            var roles = await userManager.GetRolesAsync(user);
            var token = tokenService.GetToken(user, roles);

            return new UserResponse(user.Email, user.UserName, token);
        }
        public async Task<Result<UserResponse>> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Error.Conflict("UserExists", "This email is already registered.");

            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email.Split('@')[0], 
                PhoneNumber = request.phoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var firstError = result.Errors.FirstOrDefault();
                return Error.Validation(firstError?.Code ?? "RegistrationError", firstError?.Description ?? "Failed to register user.");
            }


            var token = tokenService.GetToken(user, []);
            return new UserResponse(user.Email, user.UserName, token);
        }
    }
    
}

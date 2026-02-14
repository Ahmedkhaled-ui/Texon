using Microsoft.AspNetCore.Mvc;
using Texon.Service.Abstraction.IService;
using Texon.Shared.AuthDtos;

namespace Texon.Presentation.Controller.Auth
{
    public class AuthController(IAuthService authService) : ApiBaseController
    {

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await authService.RegisterAsync(registerRequest);


            return HandleResult(result);

        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var result = await authService.LoginAsync(loginRequest);
          
            return HandleResult(result);



        }
    }
}

using eCommerce.Core.DTO;
using eCommerce.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

      private readonly IUserService _userService;   


      public AuthController (IUserService userService)
      {
            _userService = userService; 
      }

        [HttpPost("register")]

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (registerDto == null)
                return BadRequest("Invalid registration data");

            AuthenticationDto? authenticationResponse = await _userService.Register(registerDto);

            if (authenticationResponse == null || authenticationResponse.Success == false)
            {
                return BadRequest(authenticationResponse);
            }

            return Ok(authenticationResponse); 
        }

        [HttpPost("login")]
        public async Task <IActionResult> LogIn(LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest("Invalid login data");

            AuthenticationDto? authenticationResponse = await _userService.Login(loginDto);

            if(authenticationResponse == null || authenticationResponse.Success == false)
                return Unauthorized(authenticationResponse);  

            return Ok(authenticationResponse);
        }
    }
}

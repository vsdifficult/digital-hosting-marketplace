using Microsoft.AspNetCore.Mvc; 
using HostMarket.Core.Services.Interfaces;
using HostMarket.Shared.Dto;
namespace HostMarket.Api.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthController (IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> EndpointSignUpAsync([FromBody]UserRegisterDto registerDto)
        {
            try
            {
                var user = await _authenticationService.SignUpAsync(registerDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

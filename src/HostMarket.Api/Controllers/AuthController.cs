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
        public async Task<IActionResult> EndpointSignAsync([FromBody] UserRegisterDto registerDto)
        {
            try
            {
                var result = await _authenticationService.SignUpAsync(registerDto);
                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult> EndpointsSignInAsync([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var result = await _authenticationService.SignInAsync(userLoginDTO);
                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<ActionResult> EndpointsVerificationAsync([FromBody] VerificationDto verificationDto)
        {
            try
            {
                var result = await _authenticationService.VerificationAsync(verificationDto);
                if (result.Success) return Ok(result);
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

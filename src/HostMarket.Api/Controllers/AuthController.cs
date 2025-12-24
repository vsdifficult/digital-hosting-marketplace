using Microsoft.AspNetCore.Mvc; 
using HostMarket.Core.Services.Interfaces;
using HostMarket.Shared.Dto;
namespace HostMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AdminAuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("signup/admin")]
        public async Task<IActionResult> EndpointSignUpAdminAsync([FromBody] UserRegisterDto registerDto)
        {
            try
            {
                var user = await _authenticationService.SignUpAdminAsync(registerDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("signup/serverManager")]
        public async Task<IActionResult> EndpointSignUpServerManagerAsync([FromBody] UserRegisterDto registerDto)
        {
            try
            {
                var user = await _authenticationService.SignUpServerManagerAsync(registerDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


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
        public async Task<IActionResult> EndpointSignUpUserAsync([FromBody] UserRegisterDto registerDto)
        {
            try
            {
                var user = await _authenticationService.SignUpUserAsync(registerDto);
                return Ok(user);
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
                var user = await _authenticationService.SignInAsync(userLoginDTO);
                return Ok(user);
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
                var user = await _authenticationService.VerificationAsync(verificationDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

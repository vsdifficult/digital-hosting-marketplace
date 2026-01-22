using HostMarket.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerBffService _buyerBffService;
        public BuyerController (IBuyerBffService buyerBffService)
        {
            _buyerBffService = buyerBffService;
        }

        [HttpGet("userContext")]
        public async Task<IActionResult> EndpointGetUserContextAsync()
        {
            // a temporary solution until the implementation of the new logic with the API
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return BadRequest("Invalid or missing user ID.");

            try
            {
                var result = await _buyerBffService.GetUserContextAsync(userId);
                if(result.Success) return Ok(result.User);
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("userBalance")]
        public async Task<IActionResult> EndpointGetUserBalance()
        {
            // a temporary solution until the implementation of the new logic with the API
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return BadRequest("Invalid or missing user ID.");

            try
            {
                var result = await _buyerBffService.GetUserBalanceAsync(userId);
                if (result.Success) return Ok(result.Balance);
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

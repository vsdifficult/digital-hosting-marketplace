using HostMarket.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerBffService _buyerBffService;
        public BuyerController (IBuyerBffService buyerBffService)
        {
            _buyerBffService = buyerBffService;
        }

        [HttpGet("userContext")]
        public async Task<IActionResult> EndpointGetUserContextAsync(Guid id)
        {
            try
            {
                var user = await _buyerBffService.GetUserContextAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("userBalance")]
        public async Task<IActionResult> EndpointGetUserBalance(Guid id)
        {
            try
            {
                var balance = await _buyerBffService.GetUserBalanceAsync(id);
                return Ok(balance);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

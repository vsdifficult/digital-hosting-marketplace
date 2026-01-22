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
    public class ServerController : ControllerBase
    {
        private readonly IServerBFFService _serverBFFService;
        public ServerController(IServerBFFService serverBFFService)
        {
            _serverBFFService = serverBFFService;
        }

        [HttpPost("rentServer")]
        public async Task<IActionResult> EndpointRentServerAsync(Guid serverId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
                return BadRequest("Invalid or missing user ID.");

            try
            {
                var result = await _serverBFFService.ServerRentalAsync(userId, serverId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

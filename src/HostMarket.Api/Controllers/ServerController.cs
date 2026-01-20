using HostMarket.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    [Authorize(Roles = "Admin,ServerManager")]
    public class ServerController : ControllerBase
    {
        private readonly IServerBFFService _serverBFFService;
        public ServerController(IServerBFFService serverBFFService)
        {
            _serverBFFService = serverBFFService;
        }

        [HttpPost("rentServer")]
        public async Task<IActionResult> EndpointRentServerAsync(Guid userId, Guid serverId)
        {
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

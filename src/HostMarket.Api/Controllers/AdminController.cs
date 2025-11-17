using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostMarket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBFFService _adminBFFService;
        public AdminController (IAdminBFFService adminBFFService)
        {
            _adminBFFService = adminBFFService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> EndpointCreateServerAsync(CreateServerDTO createDTO) 
        {
            try
            {
                var server = await _adminBFFService.CreateServerAsync(createDTO);
                return Ok(server);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllServers")]
        public async Task<IActionResult> EndpiontGetAllServersAsync()
        {
            try
            {
                var servers = await _adminBFFService.GetAllServersAsync();
                return Ok(servers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> EndpointDeleteServerAsync(Guid id)
        {
            try
            {
                if (await _adminBFFService.DeleteServerAsync(id)) return Ok(true);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

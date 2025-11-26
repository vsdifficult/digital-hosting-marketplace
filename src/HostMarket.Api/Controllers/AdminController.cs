using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

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
        [HttpPost("createServer")]
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

        [HttpDelete("deleteServer")]
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
        [HttpPut("updateServer")]
        public async Task<IActionResult> EndpointUpdateServerAsync(Guid id)
        {
            try
            {
                if (await _adminBFFService.UpdateServerInfoAsync(id)) return Ok(true);
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getServerStatus")]
        public async Task<IActionResult> EndpointGetServerStatusAsync(Guid id)
        {
            try
            {
                return Ok(await _adminBFFService.GetServerStatusAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //actions with the tariff

        [HttpPost("createTariff")]
        public async Task<IActionResult> EndpointCreateTariffASync(CreateTariffDto createTariffDto)
        {
            try
            {
                var tariff = await _adminBFFService.CreateTariffAsync(createTariffDto);
                return Ok(tariff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllTariffs")]
        public async Task<IActionResult> EndpointGetAllTariffsAsync()
        {
            try
            {
                var tariffs = await _adminBFFService.GetAllTariffsAsync();
                return Ok(tariffs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("deleteTariff")]
        public async Task<IActionResult> EndpointDeleteTariffAsync(Guid id)
        {
            try
            {
                if (await _adminBFFService.DeleteTariffAsync(id)) return Ok(true);
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateTariff")]
        public async Task<IActionResult> EndpointUpdateTariffAsync(Guid id)
        {
            try
            {
                if (await _adminBFFService.UpdateTariffAsync(id)) return Ok(true);
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

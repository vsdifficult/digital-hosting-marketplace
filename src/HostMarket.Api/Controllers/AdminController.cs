using HostMarket.Core.Services.Implementations.Bff;
using HostMarket.Core.Services.Interfaces;
using HostMarket.Infrastructure.Data.DTO;
using HostMarket.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HostMarket.Api.Controllers
{
    // controller for servers
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,ServerManager")]
    public class AdminServerController : ControllerBase
    {
        private readonly IAdminBFFService _adminBFFService;
        public AdminServerController(IAdminBFFService adminBFFService)
        {
            _adminBFFService = adminBFFService;
        }
        [HttpPost("createServer")]
        public async Task<IActionResult> EndpointCreateServerAsync(CreateServerDTO createDTO)
        {
            try
            {
                var result = await _adminBFFService.CreateServerAsync(createDTO);
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllServers")]
        public async Task<IActionResult> EndpiontGetAllServersAsync()
        {
            var result = await _adminBFFService.GetAllServersAsync();
            try
            {
                if (result.Success) return Ok(result.DataList);
                else return BadRequest(result.ErrorMessage);
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
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("updateServer")]
        public async Task<IActionResult> EndpointUpdateServerAsync(Guid id)
        {
            var result = await _adminBFFService.UpdateServerInfoAsync(id);
            try
            {
                if (await _adminBFFService.UpdateServerInfoAsync(id)) return Ok(true);
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getServerStatus")]
        public async Task<IActionResult> EndpointGetServerStatusAsync(Guid id)
        {
            var result = await _adminBFFService.GetServerStatusAsync(id);
            try
            {
                return Ok(result.Status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


    // controller for tariffs
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,ServerManager")]
    public class AdminTariffController : ControllerBase
    {
        private readonly IAdminBFFService _adminBFFService;
        public AdminTariffController(IAdminBFFService adminBFFService)
        {
            _adminBFFService = adminBFFService;
        }

        [HttpPost("createTariff")]
        public async Task<IActionResult> EndpointCreateTariffASync(CreateTariffDto createTariffDto)
        {
            try
            {
                var result = await _adminBFFService.CreateTariffAsync(createTariffDto);
                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllTariffs")]
        public async Task<IActionResult> EndpointGetAllTariffsAsync()
        {
            var result = await _adminBFFService.GetAllTariffsAsync();
            try
            {
                if (result.Success) return Ok(result.DataListTariff);
                else return BadRequest(result.ErrorMessage);
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
                else return BadRequest();
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
                if (await _adminBFFService.UpdateTariffAsync(id)) return Ok();
                else return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

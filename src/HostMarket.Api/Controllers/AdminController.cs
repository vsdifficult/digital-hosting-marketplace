using Microsoft.AspNetCore.Mvc; 
using HostMarket.Core.Services.Interfaces;
using HostMarket.Shared.Dto;
using HostMarket.Infrastructure.Data.DTO;

namespace HostMarket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IAdminBFFService _adminBFFService;

    public AdminController(IAdminBFFService adminBFFService)
    {
        _adminBFFService = adminBFFService;
    }

    [HttpPost("create")]
    public async Task<Guid> EndpointCreateServerAsync()
    {
        try
        {
            var user = await _adminBFFService.CreateServerAsync();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

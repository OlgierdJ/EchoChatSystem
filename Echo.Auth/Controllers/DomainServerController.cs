using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Echo.Domain.Shared.Interfaces.Services;

namespace Echo.Auth.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DomainServerController : ControllerBase
{
    private readonly IDomainServerService _domainServerService;

    public DomainServerController(IDomainServerService domainServerService)
    {
        this._domainServerService = domainServerService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterResourceApplicationRequestDTO requestDTO)
    {
        try
        {
            var result = await _domainServerService.RegisterAsync(requestDTO);

            if (!result)
            {
                return Problem("Something went wrong. Contact an Admin / Server representative");
            }
            return Ok();
        }
        catch (Exception ex)
        {

            return Problem(ex.Message);
        }
    }
}




using CoreLib.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DomainCoreApi.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class DomainController : ControllerBase
{
    private readonly IUserGroupService service;

    public DomainController(IUserGroupService service)
    {
        this.service = service;
    }

    [Authorize]
    [HttpGet("GetGroups/{accountId}")]
    public async Task<IActionResult> GetGroups(ulong accountId)
    {
        try
        {
            var result = await service.GetGroups(accountId);
            if (result == null)
            {
                return Problem("Something went wrong. Contact an Admin / Server representative");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {

            return Problem(ex.Message);
        }
    }
}

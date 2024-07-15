using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

[Authorize]
public class UserController : ControllerBase
{
    [HttpGet]
    [Route("api/user/info")]
    public IActionResult Get()
    {
        var claims = User.Claims.Select(c => new {c.Type, c.Value});
        return new JsonResult(claims);
    }
}
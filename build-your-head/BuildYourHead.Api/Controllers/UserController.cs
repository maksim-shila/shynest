using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildYourHead.Api.Controllers;

[Route("userInfo")]
[Authorize]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var claims = User.Claims.Select(c => new {c.Type, c.Value});
        return new JsonResult(claims);
    }
}
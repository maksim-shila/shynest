using BuildYourHead.Api.Controllers.RequestHandlers.Image;
using BuildYourHead.Api.Controllers.Requests.Image;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

[ApiController]
public class ImageController : ControllerBase
{
    [HttpPost]
    [Route("/api/image")]
    public IActionResult Post(PostImageRequest request)
    {
        var handler = GetRequestHandler<PostImageRequestsHandler>();
        var result = handler.Handle(request);
        return Ok(result);
    }
}
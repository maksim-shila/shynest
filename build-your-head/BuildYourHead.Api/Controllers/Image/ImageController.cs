using BuildYourHead.Api.Controllers.Image.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BuildYourHead.Api.Controllers.Image
{
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
}

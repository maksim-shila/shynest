using Microsoft.AspNetCore.Mvc;

namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class PostProductImageRequest
    {
        public string? ImagePath { get; set; }
        public bool Primary { get; set; }
    }
}

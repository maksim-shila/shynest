namespace BuildYourHead.Api.Controllers.Requests.Product;

public class PostProductImageRequest
{
    public string? ImagePath { get; set; }
    public bool Primary { get; set; }
}
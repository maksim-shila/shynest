using BuildYourHead.Api.Controllers.Requests.Image;
using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.RequestHandlers.Image;

public class PostImageRequestsHandler : IRequestHandler
{
    private readonly IImageService _imageService;

    public PostImageRequestsHandler(IImageService imageService)
    {
        _imageService = imageService;
    }

    public string Handle(PostImageRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.ImageBase64))
        {
            throw new ValidationException("Image shouldn't be empty");
        }

        return _imageService.Upload(request.ImageBase64);
    }
}
using System.Text;
using BuildYourHead.Application.Exceptions;
using BuildYourHead.Infrastructure.ImageStorage;

namespace BuildYourHead.Application.Services.Impl;

public class ImageService : IImageService
{
    private static readonly Encoding Encoding = Encoding.ASCII;
    private readonly IImageStorage _storage;

    public ImageService(IImageStorage storage)
    {
        _storage = storage;
    }

    public void Delete(IList<string> imagesPaths)
    {
        _storage.Delete(imagesPaths);
    }

    public string Get(string path)
    {
        var image = _storage.Get(path);
        if (image == null)
        {
            throw new NotFoundException($"Image {path} not found");
        }

        return Encoding.GetString(image.Content);
    }

    public string Upload(string imageBase64)
    {
        var bytes = Encoding.GetBytes(imageBase64);
        return _storage.Upload(bytes).Path;
    }
}
using System.Text;
using BuildYourHead.Application.Exceptions;
using BuildYourHead.Persistence;

namespace BuildYourHead.Application.Services.Impl;

public class ImageService : IImageService
{
    private static readonly Encoding Encoding = Encoding.ASCII;
    private readonly IUnitOfWork _uow;

    public ImageService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public void Delete(IList<string> imagesPaths)
    {
        _uow.Images.Delete(imagesPaths);
        _uow.Save();
    }

    public string Get(string path)
    {
        var image = _uow.Images.Get(path);
        if (image == null)
        {
            throw new NotFoundException($"Image {path} not found");
        }

        return Encoding.GetString(image.Content);
    }

    public string Upload(string imageBase64)
    {
        var bytes = Encoding.GetBytes(imageBase64);
        return _uow.Images.Upload(bytes).Path;
    }
}
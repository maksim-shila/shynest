namespace BuildYourHead.Application.Services;

public interface IImageService
{
    string Upload(string imageBase64);
    string Get(string path);
    void Delete(IList<string> imagesPaths);
}
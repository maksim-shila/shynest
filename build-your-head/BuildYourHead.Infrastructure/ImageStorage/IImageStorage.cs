namespace BuildYourHead.Infrastructure.ImageStorage;

public interface IImageStorage
{
    Image? Get(string path);
    Image Upload(byte[] data);
    Image Upload(string path, byte[] data);
    void Delete(string path);
    void Delete(IEnumerable<string> paths);
}
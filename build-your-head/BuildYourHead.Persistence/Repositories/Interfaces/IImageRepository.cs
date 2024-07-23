using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Persistence.Repositories.Interfaces;

public interface IImageRepository : IRepository<ImageEntity, int>
{
    ImageEntity? Get(string path);
    ImageEntity Upload(byte[] data);
    ImageEntity Upload(string path, byte[] data);
    void Delete(string path);
    void Delete(IEnumerable<string> paths);
}
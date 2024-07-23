using BuildYourHead.Persistence.Entities;
using BuildYourHead.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Persistence.Repositories.Impl;

public class ImageRepository : RepositoryBase<ImageEntity, int>, IImageRepository
{
    public ImageRepository(DbContext context) : base(context)
    {
    }

    public ImageEntity? Get(string path)
    {
        return DbSet.FirstOrDefault(x => x.Path == path);
    }

    public ImageEntity Upload(byte[] data)
    {
        return Upload(Guid.NewGuid().ToString(), data);
    }

    public ImageEntity Upload(string path, byte[] data)
    {
        var entity = new ImageEntity {Path = path, Content = data};
        var entry = DbSet.Add(entity);
        return entry.Entity;
    }

    public void Delete(string path)
    {
        var entry = DbSet.First(x => x.Path == path);
        DbSet.Remove(entry);
    }

    public void Delete(IEnumerable<string> paths)
    {
        var entries = DbSet.Where(x => paths.Contains(x.Path));
        DbSet.RemoveRange(entries);
    }
}
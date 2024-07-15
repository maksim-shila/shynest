namespace BuildYourHead.Infrastructure.ImageStorage;

public class Image
{
    internal int Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public byte[] Content { get; set; } = Array.Empty<byte>();
}
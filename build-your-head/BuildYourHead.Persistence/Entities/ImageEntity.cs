namespace BuildYourHead.Persistence.Entities;

public class ImageEntity
{
    public int Id { get; set; }
    public string Path { get; set; } = string.Empty;
    public byte[] Content { get; set; } = Array.Empty<byte>();
}
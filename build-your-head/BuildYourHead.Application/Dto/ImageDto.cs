namespace BuildYourHead.Application.Dto;

public class ImageDto
{
    public int Id { get; set; }
    public byte[] Content { get; set; } = Array.Empty<byte>();
}
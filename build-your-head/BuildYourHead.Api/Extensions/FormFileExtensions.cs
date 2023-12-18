namespace BuildYourHead.Api.Extensions
{
    public static class FormFileExtensions
    {
        public static byte[] ToByteArray(this IFormFile formFile)
        {
            using var ms = new MemoryStream();
            formFile.CopyTo(ms);
            return ms.ToArray();
        }
    }
}

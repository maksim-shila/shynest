using System.ComponentModel.DataAnnotations;

namespace Shynest.Identity.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
    }
}

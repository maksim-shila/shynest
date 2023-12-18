using BuildYourHead.Api.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BuildYourHead.Api.Controllers
{
    public class AuthorizationController : Controller
    {
        [HttpGet]
        [Route("/api/login/{userName}")]
        public IActionResult Login([FromRoute] string userName)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };
            var signingCredentials = new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.Add(AuthOptions.Lifetime);

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }
    }
}

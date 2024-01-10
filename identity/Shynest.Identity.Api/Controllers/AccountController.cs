using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shynest.Identity.Api.Models;

namespace Shynest.Identity.Api.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly IIdentityServerInteractionService _interactionService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(
        IIdentityServerInteractionService interactionService,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _interactionService = interactionService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public record LoginModel(
        string UserName,
        string Password,
        string ReturnUrl,
        bool IsPersistent = false);

    public record RegisterModel(
        string UserName,
        string Email,
        [DataType(DataType.Password)] string Password,
        [DataType(DataType.Password)] string ConfirmPassword,
        string ReturnUrl);

    [Route("/login")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IResult> PostAccountLogin(LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            model.IsPersistent,
            true);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.ToString());
        }

        var context = await _interactionService.GetAuthorizationContextAsync(model.ReturnUrl);
        var returnUrl = context != null ? model.ReturnUrl : "/";

        return Results.Ok(new { returnUrl });
    }

    [Route("/register")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IResult> PostAccountRegister(RegisterModel model)
    {
        if (model.Password != model.ConfirmPassword)
        {
            return Results.BadRequest("Passwords do not match");
        }

        if (await _userManager.FindByNameAsync(model.UserName) != null)
        {
            return Results.Conflict("User already exists.");
        }

        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return Results.Conflict("Email already in use.");
        }

        var result = await _userManager.CreateAsync(new ApplicationUser()
        {
            UserName = model.UserName,
            Email = model.Email
        }, model.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest(result.ToString());
        }

        var context = await _interactionService.GetAuthorizationContextAsync(model.ReturnUrl);
        var returnUrl = context != null ? model.ReturnUrl : "/";

        return Results.Ok(new { returnUrl });
    }
}
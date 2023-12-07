using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shynest.Identity.Data;
using Shynest.Identity.Models;

namespace Shynest.Identity.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IIdentityServerInteractionService interactionService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interactionService = interactionService;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login(string? returnUrl)
        {
            var viewModel = new LoginViewModel { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(
                viewModel.UserName,
                viewModel.Password,
                isPersistent: false,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl ?? "/");
            }

            ModelState.AddModelError(string.Empty, "Login failed");
            return View(viewModel);
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Register(string? returnUrl)
        {
            var viewModel = new RegisterViewModel { ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User
            {
                UserName = viewModel.UserName
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Redirect(viewModel.ReturnUrl ?? "/");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(viewModel);
        }

        [HttpPost]
        [Route("/logout")]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutContext = await _interactionService.GetLogoutContextAsync(logoutId);
            return Redirect(logoutContext.PostLogoutRedirectUri);
        }
    }
}

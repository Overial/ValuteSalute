using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ValuteSalute.Models;
using ValuteSalute.Service;

namespace ValuteSalute.Controllers
{
    [Authorize]
    public class AccountController
        : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController
        (
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
        )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login
        (
            LoginViewModel model,
            string returnUrl
        )
        {
            ViewBag.ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                IdentityUser user = await this._userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    await this._signInManager.SignOutAsync();

                    Microsoft.AspNetCore.Identity.SignInResult signInResult =
                        await this._signInManager.PasswordSignInAsync
                        (
                            user,
                            model.Password,
                            model.RememberMe,
                            false
                        );

                    if (signInResult.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).CutController());
        }
    }
}

using DrinkAndGo.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DrinkAndGo.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(loginViewModel);
        }

        var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                    return RedirectToAction("Index", "Home");
                return Redirect(loginViewModel.ReturnUrl);
            }
        }
        ModelState.AddModelError("", "Username/password not found");
        return View(loginViewModel);
    }

    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser() { UserName = loginViewModel.UserName };
            var result = await _userManager.CreateAsync(user, loginViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        return View(loginViewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
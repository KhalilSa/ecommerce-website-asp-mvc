using Project278.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Project278.Models;

namespace Project278.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager, AppDbContext db)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("AccessDenied")]
        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password,
                isPersistent: model.RememberMe,
                lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                    Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Label");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid username/password.");
            TempData["Error"] = "Invalid username/password.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var user = new User { UserName=model.Username, Email=model.Email };
                var result = await _userManager.CreateAsync(user, model.Password); 

                if (result.Succeeded) {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Label");
                }

                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            TempData["Error"] = "Invalid Input.";
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Label");
        }

    }
}

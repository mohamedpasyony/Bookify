using Bookify.Models;
using Bookify.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bookify.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByNameAsync(loginVM.username);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(user, isPersistent: loginVM.IsPresistent);

                        //claims
                        //  await signInManager.SignInWithClaimsAsync(user, loginVM.IsPresistent);
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Index", "Home");

                        }
                        if (User.IsInRole("Customer"))
                        {
                            return RedirectToAction("Index", "Custmer");

                        }// create cookie

                    }
                }

                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(loginVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index" , "Custmer");
        }
    }
}

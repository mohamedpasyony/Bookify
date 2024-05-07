using Bookify.Models;
using Bookify.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Bookify.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {

            Claim claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            string userId = claim.Value;
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterVM registerVM)
        {

            if (ModelState.IsValid)
            {
                // mapping
                ApplicationUser user = new()
                {
                    UserName = registerVM.Username,
                    FirstName = registerVM.FirstName,
                    LastName = registerVM.LastName,
                    Address = registerVM.Address,
                    Gender = registerVM.Gender,
                    Phone =registerVM.Phone,
                    Email = registerVM.Email,
                    PasswordHash = registerVM.Password
                };
                IdentityResult result = await userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                
                    // add user to role
                    await userManager.AddToRoleAsync(user, "Customer");

                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerVM);
        }
        
    }
}

using Bookify.GuestRepositary;
using Bookify.Models;
using Bookify.UserRepositary;
using Bookify.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Controllers
{
    [Authorize(Roles = "Admin")]

    public class HomeController : Controller
    {
        private readonly HotelDbContext _context;
        private readonly IUserRepo userRepo;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(HotelDbContext context, IUserRepo userRepo , Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userRepo = userRepo;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            ViewBag.RoomsCount = _context.Rooms.Count();
            ViewBag.RoomsTypeCount = _context.RoomTypes.Count();
            ViewBag.ReservationsCount = _context.Bookings.Count();
            ViewBag.UsersCount = _context.Users.Count();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            ViewBag.roles =  await roleManager.Roles.Select(r => r.Name).ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(UserVM model)
        {
            if (ModelState.IsValid)
            {
                await userRepo.AddUser(model);
                return RedirectToAction("AllUsers");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AllUsers()
        {
            List<ApplicationUser> Users = await userRepo.GetAllUser();
            return View(Users);
        }
    }    
}

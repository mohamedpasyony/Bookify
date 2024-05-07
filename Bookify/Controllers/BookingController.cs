using Bookify.Models;
using Bookify.BookingRepositary;
using Microsoft.AspNetCore.Mvc;
using Bookify.ViewModel;
using Bookify.RoomRepositary;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace Bookify.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BookingController : Controller
    {
        private readonly IBookingRepo _bookingRepository;
        private readonly IRoomRepo _roomRepo;
        private readonly UserManager<ApplicationUser> _userManager;





        public BookingController(IBookingRepo BookingRepo, IRoomRepo roomRepo, UserManager<ApplicationUser> userManager)
        {
            _bookingRepository = BookingRepo;
            _roomRepo = roomRepo;
            _userManager = userManager;
        }


        public  ActionResult Index()
        {
         
            var bookings = _bookingRepository.GetAllBookings();
            return View(bookings);
        }

     
        public async Task < ActionResult> Create()
        {
            ViewBag.Rooms = _roomRepo.getAllAvalibleRooms();
            var users = await _userManager.GetUsersInRoleAsync("Customer");
            ViewBag.Guests = users.ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Create(BookingVM model)
        {
            
                _bookingRepository.AddBooking(model);

                return RedirectToAction("Index");
           
            // return View(booking);
        }
        public ActionResult Delete (int id,int roomid)
        {

            _bookingRepository.DeleteBooking(id);
            Room room = _roomRepo.getRoomById(roomid);
            room.Status = "Avaliable";
            _roomRepo.Update(room);
            return RedirectToAction("Index");

            // return View(booking);
        }

    }

        
       
    }


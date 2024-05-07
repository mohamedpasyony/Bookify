using Bookify.BookingRepositary;
using Bookify.Models;
using Bookify.RoomRepositary;
using Bookify.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Controllers
{
    public class CustmerController : Controller
    {
        private readonly IRoomRepo _roomRepo;
        private readonly IBookingRepo _bookingRepository;

        private readonly UserManager<ApplicationUser> userManager;



        public CustmerController(IRoomRepo roomRepo, UserManager<ApplicationUser> userManager, IBookingRepo bookingRepository)
        {
            _roomRepo = roomRepo;
            this.userManager = userManager;
            _bookingRepository = bookingRepository;
        }

        public IActionResult Index()
        {
            var rooms=_roomRepo.getAllAvalibleRooms();
            return View(rooms);
        }

        [HttpGet]
        [Authorize]
        public async Task< IActionResult> GuestBooking(int roomNum)
        {
            var user = await userManager.GetUserAsync(User);
            ViewBag.GuestId = user.Id;
            ViewBag.RoomNum = roomNum;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult GuestBooking(BookingVM booking)
        {
            Room room = _roomRepo.getRoomById(booking.RoomTypeId);
            if(room != null)
            {
                TimeSpan difference = booking.CheckOutDate - booking.CheckInDate;
                int days = (int)difference.TotalDays;
                booking.TotalPrice = days * room.RoomType.PricePerNight;
            }
            
            
            _bookingRepository.AddBooking(booking);
            return RedirectToAction("Index");
        }
        [HttpGet]

        public IActionResult showDetails(int RoomNum)
        {
            var room = _roomRepo.getRoomById(RoomNum);
            return View("RoomDetails", room);
        }
        [HttpGet]
        [Authorize]
        public IActionResult CustomerReservations(String id)
        {
            var userReservations = _bookingRepository.GetUserBookings(id);
            return View(userReservations);
        }
        public ActionResult Cancel(int id, int roomid)
        {

            _bookingRepository.DeleteBooking(id);
            Room room = _roomRepo.getRoomById(roomid);
            room.Status = "Avaliable";
            _roomRepo.Update(room);
            return RedirectToAction("CustomerReservations");

            
        }
    }
}

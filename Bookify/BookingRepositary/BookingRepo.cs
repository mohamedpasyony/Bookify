using Bookify.Models;
using Bookify.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Bookify.BookingRepositary
{
    public class BookingRepo :IBookingRepo
    {
        public BookingRepo(HotelDbContext _context)
        {
            Context = _context;
        }

        public HotelDbContext Context { get; }
        public List<Booking> GetUserBookings(String UserId)
        {
            return Context.Bookings.Where(b=>b.GuestId ==UserId).ToList();
        }

        public List<Booking> GetAllBookings()
        {
            return Context.Bookings.ToList();
        }
        public void AddBooking(BookingVM model)
        {
            Booking booking = new Booking()
            {
                CheckInDate = model.CheckInDate,
                CheckOutDate = model.CheckOutDate,
                TotalPrice = model.TotalPrice,
                RoomNum = model.RoomTypeId,
                GuestId = model.GuestId,
            };
            var room = Context.Rooms.Find(model.RoomTypeId);
            room.Status = "Reservied";
            Context.Bookings.Add(booking);
            Context.SaveChanges();
        }

        public Booking DeleteBooking(int id)
        {
            var booking = Context.Bookings.Find(id);
            Context.Bookings.Remove(booking);
            Context.SaveChanges();
            return booking;
        }

        
    }

  
}

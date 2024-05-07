using Bookify.Models;
using Bookify.ViewModel;

namespace Bookify.RoomRepositary
{
    public class RoomRepo : IRoomRepo
    {
        public RoomRepo(HotelDbContext _context)
        {
            Context = _context;
        }

        public HotelDbContext Context { get; }
        public Room Add(RoomVM model)
        {
            Room room = new Room()
            {
                RoomTypeId = model.RoomTypeId,
                Status  = model.Status,

            };
            Context.Rooms.Add(room);
            Context.SaveChanges();
            return room;
        }
        public Room Update(Room room)
        {
            Context.Rooms.Update(room);
            Context.SaveChanges();
            return room;
        }

        public Room delete(int roomnum)
        {
            Room room = getRoomById(roomnum);
            Context.Remove(room);
            Context.SaveChanges();
            return room;
        }

        public List<Room> getAllRooms()
        {
            return Context.Rooms.ToList();
        }

        public  List<RoomType> getAllRoomsTypes()
        {
            return Context.RoomTypes.ToList();
        }

        public List<Room> getAllAvalibleRooms()
        {
            return Context.Rooms.Where(r => r.Status == "Avaliable").Select(r=>new Room {RoomNum=r.RoomNum ,RoomType=r.RoomType }).ToList();
        }

        public Room getRoomById(int roomnum)
        {
            return Context.Rooms.Find(roomnum);
        }
    }
}

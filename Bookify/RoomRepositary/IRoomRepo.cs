using Bookify.Models;
using Bookify.ViewModel;

namespace Bookify.RoomRepositary
{
    public interface IRoomRepo
    {
        public List<Room> getAllRooms();
        public  List<RoomType> getAllRoomsTypes();
        public List<Room> getAllAvalibleRooms();
        public Room getRoomById(int roomnum);
        
        public Room Add(RoomVM model);
        public Room delete(int roomnum);
        public Room Update(Room room);
    }
}

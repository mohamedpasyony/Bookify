using Bookify.Models;
using Bookify.ViewModel;

namespace Bookify.UserRepositary
{
    public interface IUserRepo
    {
        public Task<List<ApplicationUser>> GetAllUser();
        public Task<ApplicationUser> GetUserByIdAsync(string userId);
        public Task<IEnumerable<string>> GetRolesForUser(string userId);
        public Task<bool> AddRoleToUser(string userId, string roleName);
        public Task<ApplicationUser> RemoveUser(string userId);
        public void RemoveRole();
        public Task<ApplicationUser> AddUser(UserVM model);

        public Task<ApplicationUser> UpdateUser(UserVM model);
    }
}

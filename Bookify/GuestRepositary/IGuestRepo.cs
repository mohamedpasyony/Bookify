using Bookify.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.GuestRepositary
{
    public interface IGuestRepo
    {
        public  Task<List<ApplicationUser>> GetAllCustomers();
        public Task<ApplicationUser> GetUserByIdAsync(string userId);
        public Task<IEnumerable<string>> GetRolesForUser(string userId);
        public  Task<bool> AddRoleToUser(string userId, string roleName);
        public Task<ApplicationUser> removeUser(string userId);
        public void removeRole();
        public void addUser(ApplicationUser user);
        
        public void updateuser(ApplicationUser user);
    }
}

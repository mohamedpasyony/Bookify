using Bookify.GuestRepositary;
using Bookify.Models;
using Bookify.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
namespace Bookify.UserRepositary
{
    public class UserRepo(UserManager<ApplicationUser> userManager, HotelDbContext context) : IUserRepo
    {
        public Task<bool> AddRoleToUser(string userId, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> AddUser(UserVM model)
        {
            ApplicationUser user = new()
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Gender = model.Gender,
                Phone = model.Phone,
                Email = model.Email,
                PasswordHash = model.Password
            };
            await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, model.RoleName);
            return user;
        }

        public async Task<List<ApplicationUser>> GetAllUser()
        {
            var users = await userManager.Users.ToListAsync();
            return users;
        }

        public Task<IEnumerable<string>> GetRolesForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }

        public void RemoveRole()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> RemoveUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> UpdateUser(UserVM model)
        {
            throw new NotImplementedException();
        }
    }
}

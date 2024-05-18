using Onlineauction.Models;
using Server.Dtos.User;


namespace Onlineauction.Interfaces

{
    public interface IUserRepository
    {
        Task<bool> UserExists(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<int> AddUserReturnIdAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User?> UpdateUserAsync(int id, UpdateUserDto userDto);

    }
}
using MySql.Data.MySqlClient;
using Onlineauction;
using Onlineauction.Interfaces;
using Onlineauction.Models;
using Server.Data;
using Server.Dtos.User;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Server.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddUserReturnIdAsync(User userDto)
        {
            string query = @"
            INSERT INTO users (Username, Password, Email, Name, Role) 
            VALUES (@Username, @Password, @Email, @Name, @Role); 
            SELECT LAST_INSERT_ID();";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@Username", userDto.Username);
                command.Parameters.AddWithValue("@Password", userDto.Password);
                command.Parameters.AddWithValue("@Email", userDto.Email);
                command.Parameters.AddWithValue("@Name", userDto.Name);
                command.Parameters.AddWithValue("@Role", userDto.Role);

                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }



        public async Task DeleteUserAsync(int id)
        {
            string query = "DELETE FROM users WHERE id = @id";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
        }






        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            string query = "SELECT * FROM users";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var user = new User
                        {
                            Id = reader.GetInt32("id"),
                            Username = reader.GetString("username"),
                            Password = reader.GetString("password"),
                            Email = reader.GetString("email"),
                            Name = reader.GetString("name"),
                            Role = reader.GetString("role")
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            string query = "SELECT * FROM users WHERE id = @id";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var user = new User
                        {
                            Id = reader.GetInt32("id"),
                            Username = reader.GetString("username"),
                            Password = reader.GetString("password"),
                            Email = reader.GetString("email"),
                            Name = reader.GetString("name"),
                            Role = reader.GetString("role")
                        };
                        return user;
                    }
                }
            }
            return null;
        }

        public async Task<User?> UpdateUserAsync(int id, UpdateUserDto userDto)
        {
            string strQuery = @"
            UPDATE users 
            SET username = @username, password = @password, email = @email, name = @name, role = @role 
            WHERE id = @id";

            using (var command = new MySqlCommand(strQuery, _context.Connection))
            {
                command.Parameters.AddWithValue("@username", userDto.Username);
                command.Parameters.AddWithValue("@password", userDto.Password);
                command.Parameters.AddWithValue("@email", userDto.Email);
                command.Parameters.AddWithValue("@name", userDto.Name);
                command.Parameters.AddWithValue("@role", userDto.Role);
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();   
            }
            return await GetUserByIdAsync(id);
        }

        public async Task<bool> UserExists(int id)
        {
            string query = "SELECT COUNT(1) FROM users WHERE id = @id";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@id", id);
                var count = Convert.ToInt32(await command.ExecuteScalarAsync());
                return count > 0;
            }
        }
    }
}

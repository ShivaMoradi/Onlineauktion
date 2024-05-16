using MySql.Data.MySqlClient;
using Onlineauction;
using Onlineauction.Interfaces;
using Onlineauction.Models;
using Server.Data;
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

        public async Task AddUserAsync(User user)
        {
            string query = "INSERT INTO users (Username, Password, Email, Name, Role) VALUES (@Username, @Password, @Email, @Name, @Role)";

            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Role", user.Role);

                await command.ExecuteNonQueryAsync();
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
                    return null;
                }
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            string strQuery = "UPDATE users SET username = @username, password = @password, email = @email, name = @name, role = @role WHERE id = @id";

            using (var command = new MySqlCommand(strQuery, _context.Connection))
            {
                command.Parameters.AddWithValue("@id", user.Id);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@role", user.Role);
                await command.ExecuteNonQueryAsync();   
            }
        }
    }
}

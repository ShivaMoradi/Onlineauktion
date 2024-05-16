using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace 
{
    public class UsersR
    {
        public record User(int Id, string Username, string Password, string Email, string Name, string Roll)


        public static async Task<List<User>> All(ApplicationDbContext context)
        {
            var users = new List<User>();
            string strQuery ="SELECT * FROM users"

            using (var command = new MySqlCommand(strQuery, context.Connection))
            {
                using(var reader = await command.ExecuteReaderAsync())

            }

        }

    }
}
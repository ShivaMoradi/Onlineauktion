//using MySql.Data.MySqlClient;
//using Server.Data;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using Onlineauction.Models;


//namespace Onlineauction
//{
//    public class Users
//    {

//        public static async Task<List<User>> GetAll(ApplicationDbContext context)
//        {
//            var users = new List<User>();
//            string strQuery = "SELECT * FROM users";

//            using (var command = new MySqlCommand(strQuery, context.Connection))
//            {
//                using (var reader = await command.ExecuteReaderAsync())
//                {
//                    while (await reader.ReadAsync())
//                    {
//                        var user = new User(
//                            reader.GetInt32("Id"),
//                            reader.GetString("Username"),
//                            reader.GetString("Password"),
//                            reader.GetString("Name"),
//                            reader.GetString("Email"),
//                            reader.GetString("Role")
//                            );

//                        users.Add(user);

//                    }
//                }

//            }
//            return users;
//        }

//        public static async Task<User> GetById(ApplicationDbContext context, int id)
//        {
//            string strQuery = "SELECT * FROM users WHERE id = @id";

//            using (var command = new MySqlCommand(@strQuery, context.Connection))
//            {
//                command.Parameters.AddWithValue("@id", id);

//                using (var reader = await command.ExecuteReaderAsync())
//                {
//                    if (await reader.ReadAsync())
//                    {
//                        var user = new User(
//                                reader.GetInt32("Id"),
//                                reader.GetString("Username"),
//                                reader.GetString("Password"),
//                                reader.GetString("Name"),
//                                reader.GetString("Email"),
//                                reader.GetString("Role")

//                            );
//                        return user;
//                    }
//                    else
//                    {
//                        return null;
//                    }
//                }
//            }

//        }







//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Server.Data
{
    public class ApplicationDbContext : IDisposable
    {
        private readonly string _connectionString;
        private MySqlConnection _connection;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;

        }


        // Lazy loading. Only opens a connectionstring when used.
        public MySqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new MySqlConnection(_connectionString);
                    _connection.Open();
                }
                return _connection;
            }
        }

        
        // Implementing IDisposable to ensure connection is properly closed and disposed
        public void Dispose()
        {
            if(_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }

        }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN381_UI
{
   
    
        public class DatabaseConnection
        {
            private MySqlConnection connection;
            private string connectionString;

            public DatabaseConnection(string server, string database, string username, string password)
            {
                connectionString = $"Server={server};Database={database};User ID={username};Password={password};";
                connection = new MySqlConnection(connectionString);
            }

            public bool OpenConnection()
            {
                try
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                        return true;
                    }
                    return false; // Connection was already open
                }
                catch (MySqlException ex)
                {
                    // Handle the exception or log it
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }

            public bool CloseConnection()
            {
                try
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                        return true;
                    }
                    return false; // Connection was already closed
                }
                catch (MySqlException ex)
                {
                    // Handle the exception or log it
                    Console.WriteLine($"Error: {ex.Message}");
                    return false;
                }
            }

            public MySqlConnection GetConnection()
            {
                return connection;
            }
        }
    }


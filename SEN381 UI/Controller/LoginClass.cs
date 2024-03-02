using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN381_UI
{
    public class LoginLogic
    {
        private DatabaseConnection dbConnection;

        public LoginLogic(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public bool ValidateUser(string email, string password)
        {
            bool result = false;

            using (MySqlConnection connection = dbConnection.GetConnection())
            {
                if (dbConnection.OpenConnection())
                {
                    string query = "SELECT * FROM login WHERE email = @Email AND password = @Password";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            result = true;
                        }
                    }
                }

                dbConnection.CloseConnection();
            }

            return result;
        }
        }
    }

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN381_UI.Controller
{
    public  class ClientDetailsClass

    {
        public string ClientId { get; set; }
        public string ClientFName { get; set; }
        public string ClientLName { get; set; }
        public string ClientRole { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }


        public ClientDetailsClass(string clientId, string clientFName, string clientLName, string clientRole, string clientEmail,string clientPhone)
        {
            ClientId = clientId;
            ClientFName = clientFName;
            ClientLName = clientLName;
            ClientRole = clientRole;
            ClientEmail = clientEmail;
            ClientPhone = clientPhone;
        }

        public (string, string, string, string, string,string) GetClientDetails()
        {
            return (ClientId, ClientFName, ClientLName, ClientRole, ClientEmail, ClientPhone);
        }

        

        public bool UpdateClient(string clientId, string newFirstName, string newLastName, string newRole, string newEmail, string newPhone)
        {

            string server = "127.0.0.1"; // Add your server address
            string database = "Projectt"; // Add your database name
            string username = "root"; // Add your MySQL username
            string DBassword = ""; // Add Database password
            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBassword);
            try
            {
                dbConnection.OpenConnection();

                string query = "UPDATE client SET first_name = @FirstName, last_name = @LastName, role = @Role, email = @Email, phone_number = @Phone WHERE ID = @ClientId";

                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@FirstName", newFirstName);
                    cmd.Parameters.AddWithValue("@LastName", newLastName);
                    cmd.Parameters.AddWithValue("@Role", newRole);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@ClientId", clientId);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true; // Update was successful
                    }
                }

                return false; // Update failed
            }
            catch (MySqlException ex)
            {
                // Handle the exception or log it
                Console.WriteLine($"Error: {ex.Message}");
                return false; // Update failed
            }
            finally
            {
                dbConnection.CloseConnection();
            }
        }
    }
}






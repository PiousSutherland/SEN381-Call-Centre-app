using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN381_UI.Controller
{
    
    
        public class ServiceContactDataAccess
        {
            private DatabaseConnection dbConnection;

            public ServiceContactDataAccess(DatabaseConnection connection)
            {
                dbConnection = connection;
            }

            public DataTable GetServiceContactData(string clientId)
            {
                DataTable serviceContactData = new DataTable();

                try
                {
                    if (dbConnection.OpenConnection())
                    {
                        string query = "SELECT * FROM servicecontract WHERE client_id = @clientId";

                        using (MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection()))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientId);

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                            {
                                adapter.Fill(serviceContactData);
                            }
                        }

                        dbConnection.CloseConnection();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    // You can handle or log the exception as needed
                }

                return serviceContactData;
            }

            
        }
    
}
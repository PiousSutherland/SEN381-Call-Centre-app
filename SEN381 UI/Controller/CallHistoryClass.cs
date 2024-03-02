using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN381_UI.Controller
{
    public class CallHistoryClass
    {
        private DatabaseConnection dbConnection;

        public CallHistoryClass(DatabaseConnection connection)
        {
            dbConnection = connection;
        }

        public DataTable GetCallHistoryData(string clientId)
        {
            DataTable CallHistory = new DataTable();

            try
            {
                if (dbConnection.OpenConnection())
                {
                    string query = "SELECT ID, client_id, employee_id, employee_initiated, start, call_duration_in_seconds FROM clientcalldetail WHERE client_id = @clientId";

                    using (MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(CallHistory);
                        }
                    }

                    dbConnection.CloseConnection();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                
            }

            return CallHistory;
        }

        public DataTable GetNotesData(string clientId)
        {
            DataTable Notes = new DataTable();

            try
            {
                if (dbConnection.OpenConnection())
                {
                    string query = "SELECT notes FROM clientcalldetail WHERE client_id = @clientId";

                    using (MySqlCommand cmd = new MySqlCommand(query, dbConnection.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(Notes);
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

            return Notes;
        }


    }
}

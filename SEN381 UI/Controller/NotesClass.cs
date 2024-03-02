using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEN381_UI.Controller
{
    public class NotesClass
    {
        private DatabaseConnection dbConnection;

        public NotesClass(DatabaseConnection connection)
        {
            dbConnection = connection;
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

        public bool UpdateNotesData(string ID, string updatedNotes)
        {
            try
            {
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    connection.Open();

                    string query = "UPDATE clientcalldetail SET notes = @UpdatedNotes WHERE ID = @ClientId";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@UpdatedNotes", updatedNotes);
                        cmd.Parameters.AddWithValue("@ClientId", ID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true; // Update was successful
                        }
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
        }
    }
}

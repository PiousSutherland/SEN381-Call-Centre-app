using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEN381_UI.Controller
{
    public class ClientClass
    {
        private DatabaseConnection dbConnection;

        public ClientClass(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public DataTable GetAllClients()
        {
            try
            {
                if (dbConnection.OpenConnection())
                {
                    string sql = "SELECT * from client";

                    MySqlCommand cmd = new MySqlCommand(sql, dbConnection.GetConnection());
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

            return null;
        }

        public DataTable GetAllJobs()
        {
            try
            {
                if (dbConnection.OpenConnection())
                {
                    string sql = "SELECT job.ID, CONCAT(client.first_name , ' ', client.last_name) as ClientName, CONCAT(employee.first_name , ' ', employee.last_name) as AssignedTo, priority.description as Priority, open, job.description FROM `job` JOIN client on client.ID = job.client_id\r\nJOIN employee on assigned_to = employee.ID\r\nJOIN priority ON priority.ID = priority";

                    MySqlCommand cmd = new MySqlCommand(sql, dbConnection.GetConnection());
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                dbConnection.CloseConnection();
            }

            return null;
        }

        public void searchData(DataGridView dataGridView1, string searchValue)
        {
            try
            {
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    if (dbConnection.OpenConnection())
                    {
                        string query = "SELECT * FROM client where CONCAT(first_name, ' ', last_name) LIKE @Name";
                        MySqlCommand cmd = new MySqlCommand(query, connection);

                        cmd.Parameters.AddWithValue("Name", "%" + searchValue + "%");

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
                    }

                    dbConnection.CloseConnection();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No results found with that name.");
            }
        }
    }
}

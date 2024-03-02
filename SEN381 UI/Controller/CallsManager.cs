using MatthiWare.SmsAndCallClient.Api;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Voice;


namespace SEN381_UI.Controller
{
    public class CallsManager
    {
        DatabaseConnection dbConnection = new DatabaseConnection("127.0.0.1", "projectt", "root", "");
        private DateTime callStartTime;
        private List<string> notes = new List<string>();
        private string twilioSid = "AC57f4f890cd040e0915604a8625dddf1a";
        private string twilioAuth = "90b414d74f019fc04b5df432c952c072";
        private string fromNumber = "+12705139932";
        public CallsManager(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public CallsManager()
        {
        }

        public void uploadData(DataGridView dataGridView1)

        {
            try
            {
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    if (dbConnection.OpenConnection())
                    {
                        string query = "SELECT ID, CONCAT(first_name,' ', last_name) as FullName, role, phone_number FROM `client`";
                        MySqlCommand cmd = new MySqlCommand(query, connection);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void displatCallHistoryDataGridView(DataGridView dataGridView1)
        {
            try
            {
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    if (dbConnection.OpenConnection())
                    {
                        string query = "SELECT clientcalldetail.ID, client.ID, CONCAT(first_name,' ', last_name) as FullName, phone_number, start, notes " +
                            "FROM `clientcalldetail` " +
                            "INNER JOIN client " +

                            "ON clientcalldetail.client_id = client.ID;";
                        MySqlCommand cmd = new MySqlCommand(query, connection);

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void addDataToDatabase(DataGridView dataGridView1, string toNumber, string note, int client_id)
        {
            try
            {
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    if (dbConnection.OpenConnection())
                    {
                        string insertQuery = "INSERT INTO clientcalldetail (client_id, employee_id , employee_initiated, start, call_duration_in_seconds, notes) " +
                            "VALUES (@client_id, @employee_id, @employee_initiated, @start, @call_duration_in_seconds, @notes)";

                        MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);

                        insertCmd.Parameters.AddWithValue("@client_id", client_id);
                        insertCmd.Parameters.AddWithValue("@employee_id", 4);
                        insertCmd.Parameters.AddWithValue("@employee_initiated", 1);
                        insertCmd.Parameters.AddWithValue("@start", DateTime.Today);
                        insertCmd.Parameters.AddWithValue("@call_duration_in_seconds",15 );//ReturnDuration(toNumber)
                        insertCmd.Parameters.AddWithValue("@notes", ReturnLastNote());
                        MessageBox.Show("Row inserted successfully.");

                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Row inserted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to insert the row.");
                        }

                        string selectQuery = "SELECT clientcalldetail.ID, client.ID, CONCAT(first_name,' ', last_name) as FullName, phone_number, start, notes " +
                            "FROM `clientcalldetail` " +
                            "INNER JOIN client " +
                            "ON clientcalldetail.client_id = client.ID;";
                        MySqlCommand selectCmd = new MySqlCommand(selectQuery, connection);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(selectCmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
                    }

                    dbConnection.CloseConnection();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SaveNote(string note)
        {
            notes.Add(note);
        }
        public string ReturnLastNote()
        {
            if (notes.Count > 0)
            {
                return notes.Last();
            }
            else
            {
                return "No notes available.";
            }
        }
        public async void CallButtonClick(string toNumber)
        {
            try
            {
                string message = "This is a message from premiure solutions. Have a good day";

                TwilioWrapperClient twilioClient = new TwilioWrapperClient(twilioSid, twilioAuth);

                if (!twilioClient.IsInitialized)
                {
                    twilioClient.Init();
                }

                // Record the start time of the call
                callStartTime = DateTime.Now;

                var callResponse = await twilioClient.CallAsync(fromNumber, toNumber, message);

                // Handle the callResponse as needed
                MessageBox.Show($"Call Status: {callResponse.Status}");

                // Refresh the form to update its content
                System.Windows.Forms.Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Tried to make it work but kinda failed
        public async Task<TimeSpan> ReturnDuration(string toNumber)
        {
            try
            {
                string message = "This is a message from premiure solutions. Have a good day";

                TwilioWrapperClient twilioClient = new TwilioWrapperClient(twilioSid, twilioAuth);

                // Record the start time of the call
                callStartTime = DateTime.Now;

                var callResponse = await twilioClient.CallAsync(fromNumber, toNumber, message);

                // Handle the callResponse as needed
                MessageBox.Show($"Call Status: {callResponse.Status}");

                // Call has ended, capture the end time
                DateTime callEndTime = DateTime.Now;

                // Calculate the call duration
                TimeSpan callDuration = callEndTime - callStartTime;

                // Return the call duration
                return callDuration;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return TimeSpan.Zero;
            }
        }

        public void searchData(DataGridView dataGridView1, string searchValue)
        {
            try
            {
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    if (dbConnection.OpenConnection())
                    {
                        string query = "SELECT ID, CONCAT(first_name,' ', last_name) as FullName, role, phone_number FROM client WHERE CONCAT(first_name, ' ', last_name) LIKE @Name";
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
        public void CallHistorySearchData(DataGridView dataGridView1, string searchValue)
        {
            try
            {
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    if (dbConnection.OpenConnection())
                    {
                        string query = "SELECT clientcalldetail.ID, client.ID, CONCAT(client.first_name, ' ', client.last_name) as FullName, client.phone_number, clientcalldetail.start, clientcalldetail.notes " +
                            "FROM clientcalldetail " +
                            "LEFT JOIN client ON clientcalldetail.client_id = client.ID " +
                            "WHERE CONCAT(client.first_name, ' ', client.last_name) LIKE @Name";

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

        public void btnExit()
        {
            DialogResult iExit;
            try
            {
                iExit = MessageBox.Show("Do you want to exit", "Tester", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (iExit == DialogResult.Yes)
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

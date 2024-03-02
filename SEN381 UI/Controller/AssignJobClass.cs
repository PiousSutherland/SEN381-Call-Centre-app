using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using MySql.Data.MySqlClient;
using MatthiWare.SmsAndCallClient.Api;

namespace SEN381_UI.Controller
{
    public class AssignJobClass
    {

        private DatabaseConnection dbConnection;
        
        


        public AssignJobClass(DatabaseConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }



        public void LoadClients(ComboBox cbxClientID)
        {
            
            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            try
            {

                dbConnection.OpenConnection();
               
                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                 
                    string query = "SELECT ID, CONCAT(first_name, ' ', last_name) AS client_name FROM client";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the ComboBox
                    cbxClientID.DataSource = dataTable;
                    cbxClientID.DisplayMember = "client_name";
                    cbxClientID.ValueMember = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message);
            }
        }

        public void LoadAddress(ComboBox combobox1,int ID)
        {

            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";
            
            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            try
            {

                dbConnection.OpenConnection();

                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                   
                    string query = "SELECT address.ID as ID, CONCAT(street_number, ' ', street, ' ', building_number, ' ', suburb) as Address FROM address\r\nJOIN client on client_id = client.ID ";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", ID);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Set the ComboBox properties
                    combobox1.DataSource = dataTable;
                    combobox1.DisplayMember = "Address";
                    combobox1.ValueMember = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message);
            }
        }

        public void LoadEmployee(ComboBox cbAssign)
        {

            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            try
            {

                dbConnection.OpenConnection();

                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    
                    string query = "SELECT ID, CONCAT(first_name, ' ', last_name) AS employee_name FROM employee";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the ComboBox
                    cbAssign.DataSource = dataTable;
                    cbAssign.DisplayMember = "employee_name";
                    cbAssign.ValueMember = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message);
            }
        }

        public void LoadPriority(ComboBox cbPriortiy)
        {

            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            try
            {

                dbConnection.OpenConnection();

                using (MySqlConnection connection = dbConnection.GetConnection())
                {

                    string query = "SELECT ID,priority_level FROM priority";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the DataTable to the ComboBox
                    cbPriortiy.DataSource = dataTable;
                    cbPriortiy.DisplayMember = "priority_level";
                    cbPriortiy.ValueMember = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message);
            }
        }

        public bool SaveJob(int clientName, int priority, int address, int employee, string description)
        {
            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);

            try
            {
                dbConnection.OpenConnection();
                string query = "INSERT INTO Job (client_id, priority, address_id, assigned_to, description, open) " +
                                              "VALUES (@clientName, @priority, @address, @employee, @description, @open)";

                using (MySqlConnection connection = dbConnection.GetConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@clientName", clientName);
                    cmd.Parameters.AddWithValue("@priority", priority);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@employee", employee);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@open",1);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    MessageBox.Show("Data saved");



                    
                    return rowsAffected > 0;

                    
                }
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


        public string GetEmployeePhoneNumber(int employeeID)
        {
            string phoneNumber = string.Empty;
            string query = "SELECT phone_number FROM employee WHERE ID = @EmployeeID";

            using (MySqlConnection connection = dbConnection.GetConnection())
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            phoneNumber = reader["phone_number"].ToString();
                        }
                    }
                }
            }

            return phoneNumber;
        }

        public async void MessageButton_Click(string TO, string Message)
        {
            string twilioSid = "AC57f4f890cd040e0915604a8625dddf1a";
            string twilioAuth = "90b414d74f019fc04b5df432c952c072";
            string fromNumber = "+12705139932";
           

            TwilioWrapperClient twilioClient = new TwilioWrapperClient(twilioSid, twilioAuth);

            if (!twilioClient.IsInitialized)
            {
                twilioClient.Init();
            }

            try
            {
                var smsResponse = await twilioClient.SendSmsAsync(fromNumber, TO, Message);

                // Handle the smsResponse as needed
                MessageBox.Show($"SMS Status: {smsResponse.Status}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SMS failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


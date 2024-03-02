using Xunit;
using SEN381_UI; 
using MySql.Data.MySqlClient;
using System;

namespace SEN381_UI.Tests
{
    public class AddNewClientTests
    {
        [Fact]
        public void SaveClient_ValidInput_InsertsClient()
        {
            // Arrange
            string server = "127.0.0.1";
            string database = "projectt"; 
            string username = "root";
            string DBPassword = "";

            // Create a DatabaseConnection instance for testing
            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            dbConnection.OpenConnection();

            // Initialize the AddNewClient form
            AddNewClient addNewClientForm = new AddNewClient();
            addNewClientForm.tbxFname.Text = "Bass";
            addNewClientForm.tbxLname.Text = "Vis";
            addNewClientForm.tbxEmail.Text = "BASS@gmail.com";
            addNewClientForm.cbRole.SelectedIndex = 0; 
            addNewClientForm.textBox1.Text = "+27635230252"; // A valid phone number

            // Act
            addNewClientForm.btnSave_Click(null, null); // Simulate button click

            // Assert
            
            using (MySqlConnection connection = new MySqlConnection($"Server={server};Database={database};Uid={username};Pwd={DBPassword};"))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM client WHERE first_name = 'Bass' AND last_name = 'Vis' AND email = 'BASS@gmail.com'";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    Assert.Equal(1, count); // Expecting one row (the newly inserted client)
                }
            }
        }
    }
}

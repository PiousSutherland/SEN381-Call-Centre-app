using Xunit;
using SEN381_UI;
using MySql.Data.MySqlClient;
using System;

namespace SEN381_UI.Tests
{
    public class Login_Test
    {
        [Fact]
        public void Employee_Login()
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
            Login login = new Login();
            login.tbxEmail.Text = "Piet.Pompies@premiersol.co.za";
            login.tbxPassword.Text = "0-0123456789";


            // Act
            login.btnLogin_Click(null, null); // Simulate button click

            // Assert

            using (MySqlConnection connection = new MySqlConnection($"Server={server};Database={database};Uid={username};Pwd={DBPassword};"))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM login WHERE Email = 'Piet.Pompies@premiersol.co.za' AND Password='0-0123456789'";
                using (MySqlCommand cmd = new MySqlCommand(sql, connection))
                {
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    Assert.Equal(1, count); // Expecting one row (the newly inserted client)
                }
            }
        }
    }
}

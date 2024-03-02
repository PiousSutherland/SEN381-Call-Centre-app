using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace SEN381_UI
{
    public partial class Login : Form
    {
        private int loginAttempts = 0;

        public Login()
        {
            InitializeComponent();
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            string email = tbxEmail.Text;
            string password = tbxPassword.Text;

            string server = "127.0.0.1"; // Add your server address
            string database = "Projectt"; // Add your database name
            string username = "root"; // Add your MySQL username
            string DBassword = ""; // Add Database password
            

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter email and password.");
                return;
            }

            if (!email.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            // Create a DatabaseConnection instance and a LoginLogic instance
            DatabaseConnection dbConnection = new DatabaseConnection(server,database,username,DBassword);
            LoginLogic loginLogic = new LoginLogic(dbConnection);

            bool isAuthenticated = loginLogic.ValidateUser(email, password);

            if (isAuthenticated)
            {
                // Successful login, perform actions (e.g., open a new form, show a message)
                MessageBox.Show("Login successful!");
                Dashboard newForm = new Dashboard();
                newForm.Show();
                this.Hide();
                loginAttempts = 0;
            }
            else
            {
                loginAttempts++;

                if (loginAttempts >= 3)
                {
                    MessageBox.Show("You have exceeded the maximum login attempts. Please contact support.");
                    btnLogin.Enabled = false;
                }
                else
                {
                    MessageBox.Show($"Invalid login credentials. You have {3 - loginAttempts} attempts left.");
                }
                }

            dbConnection.CloseConnection();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

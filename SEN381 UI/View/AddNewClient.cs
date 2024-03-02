using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEN381_UI
{
    public partial class AddNewClient : Form
    {
        public AddNewClient()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Clients newForm = new Clients();
            newForm.Show();
            this.Hide();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {

            string fName = tbxFname.Text;
            string lName = tbxLname.Text;
            string email = tbxEmail.Text;
            string role = cbRole.SelectedItem.ToString();
            string phone = textBox1.Text;
            bool canContinue = true;

            if (string.IsNullOrEmpty(fName) || string.IsNullOrEmpty(lName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(role) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please fill in all the required fields.");
                canContinue = false;
            }
            else if (!IsValidEmail(email))
            {
                MessageBox.Show("Your email is not accepted.");
                canContinue = false;
            }
            else if (phone.Length != 12 || !phone.Contains("+27") || phone.Contains(" "))
            {
                MessageBox.Show("Your phone number must start with +27 and must be in total 12 characters long");
                canContinue= false;
            }
            else if (canContinue)
            {
                string server = "127.0.0.1";
                string database = "projectt";
                string username = "root";
                string DBPassword = "";

                try
                {
                    DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
                    dbConnection.OpenConnection();

                    string sql = "INSERT INTO client (first_name, last_name, role, email, phone_number) VALUES (@firstName, @lastName, @role, @email, @phone)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, dbConnection.GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@firstName", fName);
                        cmd.Parameters.AddWithValue("@lastName", lName);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone", phone);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0) // ONLY if the query executes sucessfully will the next form be shown
                        {
                            Console.WriteLine("Data inserted successfully.");
                            dbConnection.CloseConnection();
                        }
                        else
                        {
                            Console.WriteLine("Insert failed.");
                            dbConnection.CloseConnection();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                    return;
                }
                finally
                {
                    Clients newForm = new Clients();
                    newForm.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Client form not filled in propperly");
            }

        }
        bool IsValidEmail(string email)
        {
            // Regex pattern for email
            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(email, pattern);
        }
    }
    
}

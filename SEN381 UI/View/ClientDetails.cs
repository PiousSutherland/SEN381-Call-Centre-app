using MySqlX.XDevAPI.Relational;
using SEN381_UI.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEN381_UI
{
    public partial class ClientDetails : Form
    {
        private ClientDetailsClass clientDetailsClass;
        public string SelectedClientId { get; set; }
        public ClientDetails(string id, string fName, string lName, string role, string email,string phone)
        {
            InitializeComponent();
           
            clientDetailsClass = new ClientDetailsClass(id, fName, lName, role, email, phone);

            // Get the client details from the class
            var (clientId, clientFName, clientLName, clientRole, clientEmail, clientPhone) = clientDetailsClass.GetClientDetails();

            // Set the TextBox values using the client details
            tbxFname.Text = clientId;
            textBox1 .Text = clientFName;
            textBox2.Text = clientLName;
            textBox3.Text = clientRole;
            textBox4.Text = clientEmail;
            textBox5.Text = clientPhone;
        }

   


      


        private void LoadData()
        {
            
            string server = "127.0.0.1"; // Add your server address
            string database = "Projectt"; // Add your database name
            string username = "root"; // Add your MySQL username
            string DBassword = ""; // Add Database password
            ServiceContactDataAccess serviceContactDataAccess = new ServiceContactDataAccess(new DatabaseConnection(server, database, username, DBassword));
            CallHistoryClass callHistoryClass = new CallHistoryClass(new DatabaseConnection(server, database, username, DBassword));
            


            DataTable serviceContactData = serviceContactDataAccess.GetServiceContactData(SelectedClientId);
            DataTable callHistory = callHistoryClass.GetCallHistoryData(SelectedClientId);
            DataTable notes = callHistoryClass.GetNotesData(SelectedClientId); 

            dataGridViewCH.DataSource = callHistory;
            dataGridViewSC.DataSource = serviceContactData;
            dataGridView3.DataSource = notes;
        }



        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Clients newForm = new Clients();
            newForm.Show();
            this.Hide();
        }

        void styleDatagridview()
        {
            dataGridViewSC.BorderStyle = BorderStyle.None;
            dataGridViewSC.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridViewSC.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewSC.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridViewSC.DefaultCellStyle.SelectionBackColor = Color.MediumPurple;
            dataGridViewSC.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewSC.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewSC.EnableHeadersVisualStyles = false;
            dataGridViewSC.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewSC.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference San Serif", 10);
            dataGridViewSC.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 37);
            dataGridViewSC.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewSC.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridViewSC.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridViewCH.BorderStyle = BorderStyle.None;
            dataGridViewCH.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridViewCH.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCH.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridViewCH.DefaultCellStyle.SelectionBackColor = Color.MediumPurple;
            dataGridViewCH.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewCH.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCH.EnableHeadersVisualStyles = false;
            dataGridViewCH.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCH.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference San Serif", 10);
            dataGridViewCH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 37);
            dataGridViewCH.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCH.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridViewCH.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.MediumPurple;
            dataGridView3.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView3.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference San Serif", 10);
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 37);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView3.RowsDefaultCellStyle.ForeColor = Color.Black;
            dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }

        private void ClientDetails_Load(object sender, EventArgs e)
        {
            LoadData();
            styleDatagridview();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Retrieve the new values from the textboxes
            string sameID =tbxFname.Text;
            string newFirstName = textBox1.Text;
            string newLastName = textBox2.Text;
            string newRole = textBox3.Text;
            string newEmail = textBox4.Text;
            string newPhone = textBox5.Text;

            // Call the method to update the client's data in the database
            if (clientDetailsClass.UpdateClient(sameID, newFirstName, newLastName, newRole, newEmail,newPhone))
            {
                MessageBox.Show("Client data updated successfully.");
            }
            else
            {
                MessageBox.Show("Failed to update client data.");
            }


        }

       
    }
}

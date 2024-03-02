using MySql.Data.MySqlClient;
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
    public partial class Clients : Form
    {
       
        public Clients()
        {
            InitializeComponent();
            
        }

        private void InitializeDataGridView()
        {
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick_1; 
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard newForm = new Dashboard();
            newForm.Show();
            this.Hide();
        }

        private void btnCalls_Click(object sender, EventArgs e)
        {
            Calls newForm = new Calls();
            newForm.Show();
            this.Hide();
        }
        void populateDatagridview()
        {
            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server,database,username,DBPassword);
            ClientClass clientClass = new ClientClass(dbConnection);
            DataTable clientsData = clientClass.GetAllClients();

            if (clientsData != null)
            {
                dataGridView1.DataSource = clientsData;
            }
        }

        void styleDatagridview()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.MediumPurple;
            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference San Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 37);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;
            

        }

        private void Clients_Load(object sender, EventArgs e)
        {
            styleDatagridview();
            populateDatagridview();
        }

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            AddNewClient addNew = new AddNewClient();
            addNew.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }

     

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
 
            // Check if the double-click happened in a valid row
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                // Get the selected client's data from the DataGridView
                string clientId = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                string clientFName = dataGridView1.Rows[e.RowIndex].Cells["first_name"].Value.ToString();
                string clientLName = dataGridView1.Rows[e.RowIndex].Cells["last_name"].Value.ToString();
                string clientrole = dataGridView1.Rows[e.RowIndex].Cells["role"].Value.ToString();
                string clientEmail = dataGridView1.Rows[e.RowIndex].Cells["email"].Value.ToString();
                string clientPhone = dataGridView1.Rows[e.RowIndex].Cells["phone_number"].Value.ToString();
                ClientDetails newForm = new ClientDetails(clientId, clientFName, clientLName, clientrole, clientEmail, clientPhone);
                newForm.SelectedClientId = clientId;
                newForm.Show();
                this.Hide();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            ClientClass clientClass = new ClientClass(dbConnection);
            clientClass.searchData(dataGridView1, tbxSearch.Text);
        }

    }
}


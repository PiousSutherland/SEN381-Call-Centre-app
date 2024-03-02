using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SEN381_UI.Controller;
using MatthiWare.SmsAndCallClient;

namespace SEN381_UI
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            populateDatagridview();
            styleDatagridview();
        }
        void populateDatagridview()
        {

            string server = "127.0.0.1"; // Add your server address
            string database = "Projectt"; // Add your database name
            string username = "root"; // Add your MySQL username
            string DBassword = ""; // Add Database password
            ClientClass clientClass = new ClientClass(new DatabaseConnection(server, database, username, DBassword));
            DataTable JobsData = clientClass.GetAllJobs();

            if (JobsData != null)
            {
                dataGridView1.DataSource = JobsData;
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

        private void btnCalls_Click(object sender, EventArgs e)
        {
            Calls frmCalls = new Calls();
            frmCalls.Show();
            this.Hide();
        }

        private void btnNewIncident_Click(object sender, EventArgs e)
        {
            
            AssignJob newForm = new AssignJob();
            newForm.Show();
            this.Hide();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            Clients newForm = new Clients();
            newForm.Show();
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
    }
}

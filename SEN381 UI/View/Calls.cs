using SEN381_UI.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEN381_UI
{

    public partial class Calls : Form
    {
        static DatabaseConnection dbConnection = new DatabaseConnection("127.0.0.1", "projectt", "root", "");
        CallsManager callsManager = new CallsManager(dbConnection);
        public Calls()
        {
            InitializeComponent();
        }

        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCalls_Click(object sender, EventArgs e)
        {

        }

        private void panelTabControl_Paint(object sender, PaintEventArgs e)
        {

        }
        void datagridviewStyle()
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                lbNameDisplay.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                lbNumberDisplay.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Calls_Load(object sender, EventArgs e)
        {
            datagridviewStyle();
            callsManager.uploadData(dataGridView1);
        }

        private void btnCallHistory_Click(object sender, EventArgs e)
        {
            CallHistory newForm = new CallHistory();
            newForm.Show();
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard newForm = new Dashboard();
            newForm.Show();
            this.Hide();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            Clients newForm = new Clients();
            newForm.Show();
            this.Hide();
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected ID from the DataGridView
                    string selectedID = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();

                    // Create a new instance of the Notes Form and pass the selected ID
                    Notes notesForm = new Notes(selectedID);
                    notesForm.ShowDialog(); // Use ShowDialog to display it as a modal dialog
                    this.Hide();
                }
            
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            callsManager.btnExit();
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Login newForm = new Login();
            newForm.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            callsManager.searchData(dataGridView1, tbxSearch.Text);
        }

        private void tbxSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

       

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            //callsManager.searchData(dataGridView1,tbxSearch.Text);
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            callsManager.searchData(dataGridView1, tbxSearch.Text);
        }

        private void guna2ImageButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                callsManager.CallButtonClick(lbNumberDisplay.Text);
                callsManager.addDataToDatabase(dataGridView1, lbNumberDisplay.Text, callsManager.ReturnLastNote(), Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                this.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}



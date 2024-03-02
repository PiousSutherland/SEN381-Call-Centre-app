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
    public partial class Notes : Form
    {
        private string ID;
        private NotesClass notes;
        public Notes(string ID)
        {
            InitializeComponent();
            this.ID = ID;

            string server = "127.0.0.1"; // Add your server address
            string database = "Projectt"; // Add your database name
            string username = "root"; // Add your MySQL username
            string DBassword = ""; // Add Database password

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBassword);
            notes = new NotesClass(dbConnection);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Calls newForm = new Calls();
            newForm.Show();
            this.Hide();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
           
            string updatedNotes = textBox1.Text;

            // Update the notes using your NotesClass
            if (notes.UpdateNotesData(ID, updatedNotes))
            {
                MessageBox.Show("Notes updated successfully.");
                Calls newForm = new Calls();
                newForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed to update notes.");
            }
           
        }

        private void Notes_Load(object sender, EventArgs e)
        {
           
            DataTable data = notes.GetNotesData(ID);

            if (data.Rows.Count > 0)
            {
                
                string notesText = data.Rows[0][0].ToString();

                textBox1.Text = notesText;
            }
        }
    }
}

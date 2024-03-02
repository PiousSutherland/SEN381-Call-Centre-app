using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MatthiWare.SmsAndCallClient;
using MySql.Data.MySqlClient;
using SEN381_UI.Controller;
using TheArtOfDevHtmlRenderer.Adapters;
using MatthiWare.SmsAndCallClient.Api;
namespace SEN381_UI
{
    public partial class AssignJob : Form
    {
        public string To { get; set; }
        public string Body { get; set; }

        private MainForm mainForm;
       
        private SqlConnection connection;

        public AssignJob()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            InitializeLogicClass();
            
        }

        private void InitializeDatabaseConnection()
        {
           
        }

        private void InitializeLogicClass()
        {
           // assignJobLogic = new AssignJobClass(dbConnection);
        }

           
            
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Dashboard newForm = new Dashboard();
            newForm.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int client_id = (int)cbxClientID.SelectedValue;
            int priority_id = (int)cbPriority.SelectedValue;
            int address_id = (int)comboBox1.SelectedValue;
            int employee_id = (int)cbAssign.SelectedValue;
            string description = tbxDescription.Text;

            string client = cbxClientID.Text;
            string priority = cbPriority.Text;
            string address = comboBox1.Text;


            

            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            AssignJobClass assignJobClass = new AssignJobClass(dbConnection);


            assignJobClass.SaveJob(client_id, priority_id, address_id,employee_id ,description);
            string To = assignJobClass.GetEmployeePhoneNumber(employee_id);
            string Body = "Client name: " + client + "/n" + "Priority: " + priority + "/n" + "Address : " + address + "/n" + "Description: " + description;


            assignJobClass.MessageButton_Click(To, Body);


           
            Dashboard newDash = new Dashboard();
            newDash.Show();
            //MainForm mainForm = new MainForm();
            //mainForm.Show();
            this.Hide();



            



          //  mainForm.txtBody.text = description;
            //mainForm.txtFrom.text = "+12705139932";//Add your phone number from twilio in there
           // mainForm.txtTo.Text = assignJobClass.GetEmployeePhoneNumber(employee_id); 
           // Button sendText = mainForm.Controls.Find("btnText", true)[0] as Button;
           // sendText.PerformClick();
           
        }

        private void AssignJob_Load(object sender, EventArgs e)
        {
            string server = "127.0.0.1";
            string database = "projectt";
            string username = "root";
            string DBPassword = "";

            DatabaseConnection dbConnection = new DatabaseConnection(server, database, username, DBPassword);
            AssignJobClass assignJobClass = new AssignJobClass(dbConnection);

            assignJobClass.LoadClients(cbxClientID);
            assignJobClass.LoadPriority(cbPriority);
            assignJobClass.LoadEmployee(cbAssign);
            assignJobClass.LoadAddress(comboBox1, (int)cbxClientID.SelectedValue);
              



        }

        private void cbxClientID_SelectedIndexChanged(object sender, EventArgs e)
        {

          
            


        }


       
    }
}

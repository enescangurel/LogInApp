using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInApp
{
    public partial class Form7 : Form
    {
        public static List<Patient> PatientList { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public int PatientAge { get; set; }
        public string PatientPhoneNumber { get; set; }
        public int UserID { get; set; }
        public Form7()
        {
            InitializeComponent();
            int selectedUserID = UserID;
        }

        private void Form7_Load_1(object sender, EventArgs e)
        {
            List<Patient> list = new List<Patient>();

            foreach (Patient item in Form5.PatientList)
            {
                if (item.PatientID == Form5.PatientID)
                {
                    list.Add(item);
                }
            }

            dataGridView2.DataSource = list;
            dataGridView2.Columns[0].HeaderText = "Hasta Numarası";
            dataGridView2.Columns[1].HeaderText = "Ad";
            dataGridView2.Columns[2].HeaderText = "Soyad";
            dataGridView2.Columns[3].HeaderText = "Yaş";
            dataGridView2.Columns[4].HeaderText = "Telefon No";
            dataGridView2.Columns[5].Visible = false;
            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

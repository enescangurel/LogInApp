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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            Patient patient = new Patient();

            if (!(string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text) && string.IsNullOrEmpty(textBox4.Text)))
            {
                patient.PatientName = textBox1.Text;
                patient.PatientSurname = textBox2.Text;
                patient.PatientAge = Convert.ToInt32(textBox3.Text);
                patient.PatientPhoneNumber = textBox4.Text;
                patient.UserID = Form1.GelenUser.UserID;
                Patient sonuc =  controller.SaveNewPatient(patient);
                Form5.PatientList.Add(sonuc);
                MessageBox.Show("Hasta Kaydedilmiştir.");
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurun.");
            }
        }
    }
}

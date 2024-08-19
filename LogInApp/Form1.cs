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
    public partial class Form1 : Form
    {
        public static User GelenUser { get; set; }
        public static Patient GelenPatient { get; set; }
        public Form1()
        {
            InitializeComponent();
            GelenUser = new User();
            GelenPatient = new Patient();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();


            if (textBox1.Text.Contains("@"))
            {
                GelenUser = controller.GetUserByMail(textBox1.Text, textBox2.Text);
            }
            else
            {
                GelenUser = controller.GetUser(textBox1.Text, textBox2.Text);
            }

            if (GelenUser != null)
            {
                GelenUser = controller.GetUser(textBox1.Text, textBox2.Text);
                this.Visible = false;
                Form5 form5 = new Form5();
                form5.FormClosing += new FormClosingEventHandler(this.Form5_FormClosing); // Form5'in her an kapanıp kapanmadığını kontrol eder
                form5.Show();
            }
            else
            {

                MessageBox.Show("Böyle bir kullanıcı bulunamadı.");
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Form2 form2 = new Form2();
            form2.FormClosing += new FormClosingEventHandler(this.Form2_FormClosing);
            form2.Show();
            this.Visible=false;
        }



        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.FormClosing += new FormClosingEventHandler(this.Form3_FormClosing);
            form3.Show();
            this.Visible = false;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(this, EventArgs.Empty);
            }
        }
    }
}

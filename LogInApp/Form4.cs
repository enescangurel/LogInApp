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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text))
            {
                if(textBox3.Text == textBox4.Text)
                {
                    User glnUser = new User();

                    glnUser = controller.GetUser(textBox1.Text, textBox2.Text);

                    if (glnUser != null)
                    {
                        glnUser.Password = textBox3.Text;
                        int sonuc = controller.UpdateUser(glnUser);
                        if (sonuc > 0)
                        {
                            MessageBox.Show("Şifre başarıyla değiştirildi.");
                        }
                        else
                        {
                            MessageBox.Show("Bir hata oluştu.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı ya da şifre hatalı.");
                    }
                }
                else
                {
                    MessageBox.Show("Yeni şifreler eşleşmiyor.");
                }
            }
            else
            {
                MessageBox.Show("Tüm Alanları Doldurun.");
            }
        }

    }
}

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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            User kontrolUser = new User();


            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "UYARI", MessageBoxButtons.OK);
            }
            else
            {
                kontrolUser = controller.CheckUser(textBox1.Text);

                if (kontrolUser == null)
                {
                    kontrolUser = controller.CheckUserByMail(textBox4.Text);
                    if (kontrolUser == null)
                    {
                        if(textBox2.Text == textBox3.Text)
                        {
                            User suser = new User();
                            suser.UserName = textBox1.Text;
                            suser.Password = textBox2.Text;
                            suser.MailAdress = textBox4.Text;

                           
                           int sonuc = controller.SaveUser(suser);

                            if(sonuc > 0)
                            {
                                MessageBox.Show("Kayıt başarılı.");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Sistemsel bir hata var. Lütfen daha sonra deneyin.");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Şifreler uyuşmuyor.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bu kullanıcı sistemde kayıtlı.");

                    }
                }
                else
                {
                    MessageBox.Show("Bu kullanıcı sistemde kayıtlı.");
                }















                // 1) Önce bu Kullanıcı Adı ya da mail adresi veri tabanında var mı kontrol et.
                // Eğer yoksa kullanıcıyı kaydet.
               // Eğer varsa bu kullanıcı kayıtlı şeklinde uyarı göster.
            }
        }
    }
}

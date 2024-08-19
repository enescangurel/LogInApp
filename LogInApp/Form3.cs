using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogInApp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public void SendPasswordByEmail(string userEmail, string userPassword)
        {
            try
            {
                // SMTP ayarları
                SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("ensgrl58@hotmail.com", "Jetteam7"),
                    EnableSsl = true,
                };

                // E-posta mesajı oluşturma
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("ensgrl58@hotmail.com"),
                    Subject = "Şifre Hatırlatma",
                    Body = $"Merhaba, \n\nŞifreniz: {userPassword}\n\nİyi günler dileriz.",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(userEmail);

                // E-posta gönderme
                smtpClient.Send(mailMessage);

                MessageBox.Show("Şifreniz e-posta ile gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("E-posta gönderilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();

            User sifreUser = new User();

            sifreUser = controller.CheckUser(textBox1.Text);

            if ( sifreUser != null )
            {
                sifreUser = controller.CheckUserByMail(textBox2.Text);

                if( sifreUser != null )
                {
                    SendPasswordByEmail(sifreUser.MailAdress, sifreUser.Password);
                }
                else
                {
                    MessageBox.Show("Hatalı kullanıcı.");
                }

            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı.");
            }
        }






















    }
}

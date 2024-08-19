using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LogInApp
{
    public class Controller
    {
        public SqlConnection Con { get; set; }
        public SqlCommand Cmd { get; set; }

        public Controller()
        {
            Con = new SqlConnection("Server = DESKTOP-LA1DUL9\\SQLEXPRESS; Database = LogInAppDB; Trusted_Connection = True;");
            Cmd = new SqlCommand();
            Cmd.Connection = Con;
        }

        public User GetUser(string userName, string password)   // Kullanıcıya KULLANICI ADI ile giriş yaptırmak için oluşturuldu.
        {
            User user = null;
            string query = "SELECT * FROM [LogInAppDB].[dbo].[User] WHERE UserName = @UserName AND Password = @UserPass";
            Cmd.CommandText = query;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@UserName", userName);
            Cmd.Parameters.AddWithValue("@UserPass", password);

            try      // try bloğunun içindekini dene, başarısızlık olursa catch ya da finally'e geç
            {
                Con.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User();

                    user.UserID = (int)reader["UserID"];
                    user.UserName = reader["UserName"].ToString();
                    user.MailAdress = reader["MailAdress"].ToString();
                    user.Password = reader["Password"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
            return user;
        }

        public User GetUserByMail(string mailAdress, string password) // Kullanıcıya MAİL ile giriş yaptırmak için oluşturuldu.
        {
            User user = null;
            string query = "SELECT * FROM [LogInAppDB].[dbo].[User] WHERE MailAdress = @mail AND Password = @UserPass";
            Cmd.CommandText = query;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@mail", mailAdress);
            Cmd.Parameters.AddWithValue("@UserPass", password);

            try      // try bloğunun içindekini dene, başarısızlık olursa catch ya da finally'e geç
            {
                Con.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User();

                    user.UserID = (int)reader["UserID"];
                    user.UserName = reader["UserName"].ToString();
                    user.MailAdress = reader["MailAdress"].ToString();
                    user.Password = reader["Password"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
            return user;
        }

        // WHERE UserName veri tabanındaki adı
        // userName parametre-kullanıcıdan alınacak -- @ise hacklenmemesi için bir yöntem

        public User CheckUser(string userName) // Yeni kayıt sırasında daha önceden bu isimde bi kullanıcı adı var mı diye bakmak için.
        {
            User user = null;
            string query = "SELECT * FROM [LogInAppDB].[dbo].[User] WHERE UserName = @name";
            Cmd.CommandText = query;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@name", userName);

            try      // try bloğunun içindekini dene, başarısızlık olursa catch ya da finally'e geç
            {
                Con.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User();

                    user.UserID = (int)reader["UserID"];
                    user.UserName = reader["UserName"].ToString();
                    user.MailAdress = reader["MailAdress"].ToString();
                    user.Password = reader["Password"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
            return user;
        }

        public User CheckUserByMail(string mailAdress)
        {

            User user = null;
            string query = "SELECT * FROM [LogInAppDB].[dbo].[User] WHERE MailAdress = @mail";
            Cmd.CommandText = query;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@mail", mailAdress);

            try      // try bloğunun içindekini dene, başarısızlık olursa catch ya da finally'e geç
            {
                Con.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

                if (reader.Read())
                {
                    user = new User();

                    user.UserID = (int)reader["UserID"];
                    user.UserName = reader["UserName"].ToString();
                    user.MailAdress = reader["MailAdress"].ToString();
                    user.Password = reader["Password"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
            return user;
        }
        public List<User> GetUsers()
        {
            return null;
        }

        public int SaveUser(User user)
        {
            try
            {
                Con.Open();

                string query = "INSERT INTO [LogInAppDB].[dbo].[User] (UserName, Password, MailAdress) VALUES (@name, @pass, @mail)";
                Cmd.CommandText = query;
                Cmd.Parameters.Clear();
                Cmd.Parameters.AddWithValue("@name", user.UserName);
                Cmd.Parameters.AddWithValue("@pass", user.Password);
                Cmd.Parameters.AddWithValue("@mail", user.MailAdress);
                int sonuc = Cmd.ExecuteNonQuery();
                return sonuc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                Con.Close();
            }
        }

        public int UpdateUser(User user)
        {
            try
            {
                Con.Open();

                string query = "UPDATE [LogInAppDB].[dbo].[User] SET UserName = @name, Password = @pass, MailAdress = @mail WHERE UserName = @name";
                Cmd.CommandText = query;
                Cmd.Parameters.Clear();
                Cmd.Parameters.AddWithValue("@name", user.UserName);
                Cmd.Parameters.AddWithValue("@pass", user.Password);
                Cmd.Parameters.AddWithValue("@mail", user.MailAdress);
                int sonuc = Cmd.ExecuteNonQuery();
                return sonuc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                Con.Close();
            }
        }

        public int DeleteUser(User user)
        {
            try
            {
                Con.Open();

                string query = "DELETE FROM [LogInAppDB].[dbo].[User] WHERE UserName = @name";
                Cmd.CommandText = query;
                Cmd.Parameters.Clear();
                Cmd.Parameters.AddWithValue("@name", user.UserName);
                int sonuc = Cmd.ExecuteNonQuery();
                return sonuc;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                Con.Close();
            }
        }

        public Patient GetPatientByPatientID(int patientID)
        {
            return null;
        }

        public List<Patient> GetPatientList(int UserID)
        {
            string query = "SELECT * FROM [LogInAppDB].[dbo].[Patient] WHERE UserID = @userid";
            Cmd.CommandText = query;
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@userid", UserID);

            List<Patient> patients = new List<Patient>();

            try
            {
                Con.Open();
                SqlDataReader reader = Cmd.ExecuteReader();

                while (reader.Read())
                {
                    Patient patient = new Patient();

                    patient.PatientID = (int)reader["PatientID"];
                    patient.PatientName = reader["PatientName"].ToString();
                    patient.PatientSurname = reader["PatientSurname"].ToString();
                    patient.PatientAge = (int)reader["PatientAge"];
                    patient.PatientPhoneNumber = reader["PatientPhoneNumber"].ToString();
                    patient.UserID = (int)reader["UserID"];
                    patients.Add(patient);

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
            return patients;
        }

        // buraya bir hasta arama metodu yazacağız, parametre olarak senden string isteyecek(textboxtaki yazıyı), return olarak hasta listesi verecek, sonrasında bu metodu kullanırken metottan gelen değeri dataGridView'a dataSource olarak verecek

        public Patient SaveNewPatient(Patient patient)
        {
            try
            {
                Con.Open();

                string query = "INSERT INTO [LogInAppDB].[dbo].[Patient] (PatientName, PatientSurname, PatientAge, PatientPhoneNumber, UserID) VALUES (@name, @surname, @age, @phone, @userid)";
                Cmd.CommandText = query;
                Cmd.Parameters.Clear();
                Cmd.Parameters.AddWithValue("@name", patient.PatientName);
                Cmd.Parameters.AddWithValue("@surname", patient.PatientSurname);
                Cmd.Parameters.AddWithValue("@age", patient.PatientAge);
                Cmd.Parameters.AddWithValue("@phone", patient.PatientPhoneNumber);
                Cmd.Parameters.AddWithValue("@userid", patient.UserID);

                int sonuc = Cmd.ExecuteNonQuery();
                patient.PatientID = sonuc;
                return patient;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                Con.Close();
            }

        }
        public int DeletePatient(int PatientID)
        {
            try
            {
                {
                    Con.Open();

                    string query = "DELETE FROM Patient WHERE PatientID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ID", PatientID);
                        int sonuc = cmd.ExecuteNonQuery();
                        return sonuc;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
                return -1;
            }
        }

    }
}

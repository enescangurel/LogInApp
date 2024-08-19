using LogInApp;
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

namespace LogInApp
{
    public partial class Form5 : Form
    {
        public static int PatientID { get; set; }
        public static List<Patient> PatientList { get; set; }

        public Form5()
        {
            InitializeComponent();
            PatientList = new List<Patient>();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            DialogResult result = MessageBox.Show("Kullanıcıyı silmek istediğinize emin misiniz?", "AMAN HA!", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                int silinen = controller.DeleteUser(Form1.GelenUser);

                if (silinen > 0)
                {
                    MessageBox.Show("Hesap başarıyla silindi.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sistemsel bir hata oluştu.");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.ShowDialog();

        }
        private void Form5_Load(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            List<Patient> hastalistesi = controller.GetPatientList(Form1.GelenUser.UserID);
            PatientList = hastalistesi;

            dataGridView1.DataSource = hastalistesi;
            dataGridView1.Columns[0].HeaderText = "Hasta Numarası";
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "Yaş";
            dataGridView1.Columns[4].HeaderText = "Telefon No";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            // {
            //     column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // }                        Yukarıdaki işlemin döngü ile kolaylaştırılmış hali.
            int userId = Form1.GelenUser.UserID;
            LoadComboBoxData(userId);
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);



            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Resizable = DataGridViewTriState.False;
            }

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ReadOnly = true;

            //dataGridView1.ColumnHeadersVisible = false;     Başlıkları kaldırır.
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "Hasta Arama";
        }
        //private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dataGridView1.SelectedRows.Count > 0)
        //    {
        //        int selectedUserID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);

        //        Form7 form7 = new Form7();
        //        form7.UserID = selectedUserID;
        //        form7.Show();
        //    }
        //}

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Controller controller = new Controller();
            Patient patient = new Patient();


            if (e.RowIndex >= 0)
            {
                int selectedRowIndex = e.RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                PatientID = Convert.ToInt32(selectedRow.Cells[0].Value);
                Form7 form7 = new Form7();

                form7.Show();

            }
        }

        private void LoadComboBoxData(int UserID)
        {
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-LA1DUL9\\SQLEXPRESS;Database=LogInAppDB;Trusted_Connection=True;"))
            {
                DataTable dataTable = new DataTable();
                try
                {
                    con.Open();

                        // Sadece belirli bir doktora ait hastaları çekmek için SQL sorgusunu filtreliyoruz
                        string query = "SELECT PatientName, PatientID FROM Patient";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con)
                    {
                        adapter.Fill(dataTable);
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", UserID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string patientName = reader["PatientName"].ToString();
                                comboBox1.ValueMember = UserID.ToString();


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veriler yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
                {
                    Controller controller = new Controller();

                    if (comboBox1.SelectedItem != null)
                    {
                int selectedPatient = Convert.ToInt32(comboBox1.SelectedValue);

                        int result = controller.DeletePatient(selectedPatient);

                        if (result > 0)
                        {
                            MessageBox.Show("Kişi başarıyla silindi.");
                            comboBox1.Items.Remove(selectedPatient); // ComboBox'tan da kaldır
                        }
                        else
                        {
                            MessageBox.Show("Kişi silinirken bir hata oluştu.");
                        }
                    }
                }


    }
        }

    





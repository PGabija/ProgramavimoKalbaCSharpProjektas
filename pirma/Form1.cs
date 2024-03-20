using System;
using System.Data;
using System.Windows.Forms;

namespace pirma
{
    public partial class Login : Form
    {
        private string username;
        private string user_password;
        private DatabaseManager databaseManager;

        public Login()
        {
            InitializeComponent();
            this.databaseManager = new DatabaseManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable studentTable = databaseManager.GetStudentData(PrisijungimoVardas.Text, Slaptazodis.Text);

                if (studentTable.Rows.Count > 0)
                {
                    username = PrisijungimoVardas.Text;
                    user_password = Slaptazodis.Text;

                    Studento form2 = new Studento(username, user_password);
                    form2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Neteisingas prisijungimo vardas arba slaptažodis", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PrisijungimoVardas.Clear();
                    Slaptazodis.Clear();
                    PrisijungimoVardas.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Klaida: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username, user_password;

            username = PrisijungimoVardas2.Text;
            user_password = Slaptazodis2.Text;

            try
            {
                DataTable lecturerTable = databaseManager.GetLecturerData(username, user_password);

                if (lecturerTable.Rows.Count > 0)
                {
                    username = PrisijungimoVardas2.Text;
                    user_password = Slaptazodis2.Text;

                    Destytojo form3 = new Destytojo();
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Neteisingas prisijungimo vardas arba slaptažodis", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PrisijungimoVardas2.Clear();
                    Slaptazodis2.Clear();
                    PrisijungimoVardas2.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Klaida: " + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Slaptazodis.PasswordChar = '\0';
            }
            else
            {
                Slaptazodis.PasswordChar = '*';
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Slaptazodis2.PasswordChar = '\0';
            }
            else
            {
                Slaptazodis2.PasswordChar = '*';
            }
        }
    }
}
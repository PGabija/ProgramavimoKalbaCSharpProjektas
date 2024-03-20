using System;
using System.Data;
using System.Windows.Forms;

namespace pirma
{
    public partial class Studento : Form
    {
        private string username;
        private string user_password;

        private readonly DatabaseManager dbManager;

        public Studento(string username, string user_password)
        {
            InitializeComponent();
            this.username = username;
            this.user_password = user_password;
            this.dbManager = new DatabaseManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private void Studento_Load_1(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = dbManager.GetStudentGrades(username);

                if (dataTable.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dataTable;

                    double totalSum = 0;
                    double totalCredits = 0;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        double credits = Convert.ToDouble(row["KredituSkaicius"]);
                        double grade = Convert.ToDouble(row["Ivertinimas"]);

                        totalSum += credits * grade;
                        totalCredits += credits;
                    }

                    double average = Math.Round(totalSum / totalCredits, 2);
                    textBox1.Text = average.ToString();
                }
                else
                {
                    MessageBox.Show("Nerasta duomenų pagal jūsų vartotojo vardą.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Klaida įkeliant duomenis iš duomenų bazės: " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
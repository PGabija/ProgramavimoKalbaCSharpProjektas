using System;
using System.Windows.Forms;

namespace pirma
{
    public partial class Moduliai : Form
    {
        private readonly DatabaseManager databaseManager;

        public Moduliai()
        {
            InitializeComponent();
            this.databaseManager = new DatabaseManager();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Destytojo form = new Destytojo();
            form.Show();
            this.Hide();
        }

        private void Moduliai_Load(object sender, EventArgs e)
        {
            this.modulisTableAdapter.Fill(this.vertinimasDa.modulis);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string moduleName = textBox1.Text;
                string credits = textBox2.Text;
                this.databaseManager.InsertModule(moduleName, credits);
                MessageBox.Show("Duomenys buvo sėkmingai įrašyti į lentelę 'modulis'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Įvyko klaida: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string taskName = textBox3.Text;
                this.databaseManager.InsertTask(taskName);
                MessageBox.Show("Duomenys buvo sėkmingai įrašyti į lentelę 'uzduotis'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Įvyko klaida: " + ex.Message);
            }
        }
    }
}

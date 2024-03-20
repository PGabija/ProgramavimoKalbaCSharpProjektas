using System;
using System.Data;
using System.Windows.Forms;

namespace pirma
{
    public partial class PazymiaiEdit : Form
    {
        private readonly DatabaseManager dbManager;

        public PazymiaiEdit()
        {
            InitializeComponent();
            this.dbManager = new DatabaseManager();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.uzduotisTableAdapter.Fill(this.vertinimasDataB.uzduotis);
            this.pazymysTableAdapter.Fill(this.vertinimasDa.pazymys);
            this.modulisTableAdapter.Fill(this.vertinimasDa.modulis);
            this.studentaiTableAdapter.Fill(this.vertinimasDa.studentai);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboBox1.SelectedItem;

                if (selectedRow != null)
                {
                    string studentoNumeris = selectedRow["StudentoNumeris"].ToString();
                    string studentoVardas = dbManager.GetStudentName(studentoNumeris);
                    string studentoPavarde = dbManager.GetStudentSurname(studentoNumeris);
                    string studentoGrupe = dbManager.GetStudentGroup(studentoNumeris);

                    textBox1.Text = studentoNumeris;
                    textBox2.Text = studentoVardas;
                    textBox3.Text = studentoPavarde;
                    textBox4.Text = studentoGrupe;
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboBox2.SelectedItem;

                if (selectedRow != null)
                {
                    string modulioPavadinimas = selectedRow["ModulioPavadinimas"].ToString();
                    int kredituSkaicius = dbManager.GetModuleCredits(modulioPavadinimas);

                    textBox5.Text = modulioPavadinimas;
                    textBox6.Text = kredituSkaicius.ToString();
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboBox3.SelectedItem;

                if (selectedRow != null)
                {
                    string uzduotis = selectedRow["UzduotiesPavadinimas"].ToString();

                    textBox7.Text = uzduotis;
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)comboBox4.SelectedItem;

                if (selectedRow != null)
                {
                    string balas = selectedRow["Balas"].ToString();
                    textBox8.Text = balas;

                    if (Int32.TryParse(balas, out int balasSkaicius) && balasSkaicius < 5)
                    {
                        textBox9.Text = "NEIS";
                    }
                    else
                    {
                        textBox9.Text = "IS";
                    }
                }
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string selectedDate = monthCalendar1.SelectionStart.ToShortDateString();

            textBox10.Text = selectedDate;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Destytojo form = new Destytojo();
            form.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string studentoNumeris = textBox1.Text;
                string studentoVardas = textBox2.Text;
                string studentoPavarde = textBox3.Text;
                string studentoGrupe = textBox4.Text;
                string modulioPavadinimas = textBox5.Text;
                string kredituSkaicius = textBox6.Text;
                string uzduotis = textBox7.Text;
                string iskaityta = textBox9.Text;
                string balas = textBox8.Text;
                string data = textBox10.Text;

                dbManager.InsertGrade(studentoNumeris, studentoVardas, studentoPavarde, studentoGrupe, modulioPavadinimas, kredituSkaicius, uzduotis, iskaityta, balas, data);

                MessageBox.Show("Duomenys buvo sėkmingai įrašyti į lentelę 'ivertinimas'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Įvyko klaida: " + ex.Message);
            }
        }
    }
}


using Devart.Data.MySql;
using System;
using System.Data;

namespace pirma
{
    public class DatabaseManager
    {
        private readonly MySqlConnection mySqlConnection;

        public DatabaseManager()
        {
            string connectionString = "server=127.0.0.1; user=root; database=vertinimas; password=admin";
            this.mySqlConnection = new MySqlConnection(connectionString);
        }

        public DataTable GetStudentData(string username, string password)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM studentai WHERE StudentoNumeris = @username AND Slaptazodis = @password", mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Klaida gaunant studento duomenis: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable GetLecturerData(string username, string password)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM destytojai WHERE VartotojoVardas = @username AND Slaptazodis = @password", mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Klaida gaunant destytojo duomenis: " + ex.Message);
            }
            return dataTable;
        }

        public DataTable GetStudentGrades(string username)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT Grupe, Pavarde, Vardas, ModulioPavadinimas, KredituSkaicius, UzduotiesPavadinimas, Iskaityta, Ivertinimas, StudentoNumeris, data FROM ivertinimas WHERE StudentoNumeris = @studentoNumeris", mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@studentoNumeris", username);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Klaida gaunant studento pažymius: " + ex.Message);
            }
            return dataTable;
        }

        public string GetStudentName(string studentNumber)
        {
            string query = "SELECT Vardas FROM studentai WHERE StudentoNumeris = @StudentoNumeris";
            return GetStringColumn(query, studentNumber);
        }

        public string GetStudentSurname(string studentNumber)
        {
            string query = "SELECT Pavarde FROM studentai WHERE StudentoNumeris = @StudentoNumeris";
            return GetStringColumn(query, studentNumber);
        }

        public string GetStudentGroup(string studentNumber)
        {
            string query = "SELECT Grupe FROM studentai WHERE StudentoNumeris = @StudentoNumeris";
            return GetStringColumn(query, studentNumber);
        }

        public int GetModuleCredits(string moduleName)
        {
            string query = "SELECT KredituSkaicius FROM modulis WHERE ModulioPavadinimas = @ModulioPavadinimas";
            return GetIntColumn(query, moduleName);
        }

        private int GetIntColumn(string query, string parameterValue)
        {
            int result = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@ModulioPavadinimas", parameterValue);
                    mySqlConnection.Open();
                    result = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            finally
            {
                mySqlConnection.Close();
            }
            return result;
        }

        private string GetStringColumn(string query, string parameterValue)
        {
            string result = "";
            try
            {
                using (MySqlCommand command = new MySqlCommand(query, mySqlConnection))
                {
                    command.Parameters.AddWithValue("@StudentoNumeris", parameterValue);
                    mySqlConnection.Open();
                    result = (string)command.ExecuteScalar();
                }
            }
            finally
            {
                mySqlConnection.Close();
            }
            return result;
        }

        public void InsertGrade(string studentNumber, string name, string surname, string group, string moduleName, string credits, string task, string passed, string grade, string date)
        {
            try
            {
                mySqlConnection.Open();
                string query = "INSERT INTO ivertinimas (Grupe, Pavarde, Vardas, ModulioPavadinimas, KredituSkaicius, UzduotiesPavadinimas, Iskaityta, Ivertinimas, StudentoNumeris, data) VALUES (@Grupe, @Pavarde, @Vardas, @ModulioPavadinimas, @KredituSkaicius, @UzduotiesPavadinimas, @Iskaityta, @Balas, @StudentoNumeris, @data)";
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                command.Parameters.AddWithValue("@Grupe", group);
                command.Parameters.AddWithValue("@Pavarde", surname);
                command.Parameters.AddWithValue("@Vardas", name);
                command.Parameters.AddWithValue("@ModulioPavadinimas", moduleName);
                command.Parameters.AddWithValue("@KredituSkaicius", credits);
                command.Parameters.AddWithValue("@UzduotiesPavadinimas", task);
                command.Parameters.AddWithValue("@Iskaityta", passed);
                command.Parameters.AddWithValue("@Balas", grade);
                command.Parameters.AddWithValue("@StudentoNumeris", studentNumber);
                command.Parameters.AddWithValue("@data", date);
                command.ExecuteNonQuery();
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        public void InsertModule(string moduleName, string credits)
        {
            try
            {
                mySqlConnection.Open();
                string query = "INSERT INTO modulis (ModulioPavadinimas, KredituSkaicius) VALUES (@ModulioPavadinimas, @KredituSkaicius)";
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                command.Parameters.AddWithValue("@ModulioPavadinimas", moduleName);
                command.Parameters.AddWithValue("@KredituSkaicius", credits);
                command.ExecuteNonQuery();
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        public void InsertTask(string taskName)
        {
            try
            {
                mySqlConnection.Open();
                string query = "INSERT INTO uzduotis (UzduotiesPavadinimas) VALUES (@UzduotiesPavadinimas)";
                MySqlCommand command = new MySqlCommand(query, mySqlConnection);
                command.Parameters.AddWithValue("@UzduotiesPavadinimas", taskName);
                command.ExecuteNonQuery();
            }
            finally
            {
                mySqlConnection.Close();
            }
        }
    }
}
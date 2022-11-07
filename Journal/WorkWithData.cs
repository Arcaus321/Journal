using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Security.Cryptography;

namespace Journal
{
    static internal class WorkWithData
    {
        static public DataTable ExecuteSqlQueryAsDataTable(string sqlQuery)
        {
            DataTable table = new DataTable();
            SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, connection);
            adapter.SelectCommand = new SQLiteCommand(sqlQuery, connection);
            adapter.Fill(table);
            return table;
        }

        static public string GetBase64(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
        static public EnumerableRowCollection<DataRow> ExecuteSqlQueryAsEnumerable(string sqlQuery)
        {
            return ExecuteSqlQueryAsDataTable(sqlQuery).AsEnumerable();
        }

        static public string GetSha256(string str)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        static public bool CheckDataCorrectnes(RegistrationUC uC)
        {
            string emailPattern = @"^[a-z]+@[a-z]+[.][a-z]+"; //Регулярное выражения для проверки почты

            if (!Regex.IsMatch(uC.Controls[0].Controls["tbEmail"].Text, emailPattern, RegexOptions.IgnoreCase))
            {
                MessageBox.Show("Некоректный адрес электронной почты");
                return false;
            }

            if (uC.Controls[0].Controls["tbSecondName"].Text == String.Empty)
            {
                MessageBox.Show("Не указана фамилия");
                return false;
            }

            if (uC.Controls[0].Controls["tbFirstName"].Text == String.Empty)
            {
                MessageBox.Show("Не указано имя");
                return false;
            }

            if (uC.Controls[0].Controls["tbLogin"].Text == String.Empty)
            {
                MessageBox.Show("Не указан логин");
                return false;
            }

            if (uC.Controls[0].Controls["tbLogin"].Text == String.Empty)
            {
                MessageBox.Show("Не указан логин");
                return false;
            }

            if (uC.Controls[0].Controls["tbPassword"].Text == String.Empty)
            {
                MessageBox.Show("Не указан пароль");
                return false;
            }

            if (uC.Controls[0].Controls["tbPassword"].Text != uC.Controls[0].Controls["tbRepeatPassword"].Text)
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }
            return true;
        }

       
    }
}

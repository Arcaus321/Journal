using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Journal
{
    static internal class WorkWithData
    {
        static public DataTable ExecuteSqlQueryAsDataTable(string sqlQuery)
        {
            DataTable dataTable = new DataTable();

            using (SQLiteConnection db = new SQLiteConnection(@"Data Source = C:\Users\l.m\source\repos\Journal\database.db"))
            {
                SQLiteCommand selectCommand = new SQLiteCommand(sqlQuery, db);
                try
                {
                    db.Open();
                    SQLiteDataReader reader = selectCommand.ExecuteReader();

                    if (reader.HasRows)
                        for (int i = 0; i < reader.FieldCount; i++)
                            dataTable.Columns.Add(new DataColumn(reader.GetName(i)));

                    int j = 0;
                    while (reader.Read())
                    {
                        DataRow row = dataTable.NewRow();
                        dataTable.Rows.Add(row);

                        for (int i = 0; i < reader.FieldCount; i++)
                            dataTable.Rows[j][i] = (reader.GetValue(i));

                        j++;
                    }

                    db.Close();
                }
                catch (Exception e)
                {
                    db.Close();
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
                return dataTable;
            }

        }

        static public bool CheckDataCorrectnes(RegistrationUC uC)
        {
            string pattern = @"^[a-z]+@[a-z]+[.][a-z]+"; //Регулярное выражения для проверки почты

            if (!Regex.IsMatch(uC.Controls[0].Controls["tbEmail"].Text, pattern, RegexOptions.IgnoreCase))
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

        static public EnumerableRowCollection<DataRow> ExecuteSqlQueryAsEnumerable(string sqlQuery)
        {
            return ExecuteSqlQueryAsDataTable(sqlQuery).AsEnumerable();
        }
    }
}

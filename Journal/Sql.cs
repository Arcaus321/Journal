using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Journal
{
    static internal class Sql
    {
        static public EnumerableRowCollection<DataRow> ExecuteSqlQueryAsEnumerable(string sqlQuery)
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
                return dataTable.AsEnumerable();
            }

        }
    }
}

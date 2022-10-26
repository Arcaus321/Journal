using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Journal
{
    public partial class RegistrationUC : UserControl
    {
        public RegistrationUC()
        {
            InitializeComponent();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Navigation.OpenEntryUC(this.ParentForm);
            this.Dispose();
        }

        private void bRegistration_Click(object sender, EventArgs e)
        {
            var x = this;
            if (!WorkWithData.CheckDataCorrectnes(this))
            {
                return;
            }

            SQLiteConnection connection = new SQLiteConnection(@"Data Source = C:\Users\l.m\source\repos\Journal\database.db");
            string sqlQuery = $"INSERT INTO Users (FirstName, LastName, MiddleName, Login, Password, UserRole, UserGroup, Email) VALUES ('{tbSecondName.Text}', '{tbFirstName.Text}', '{tbThirdName.Text}', '{tbLogin.Text}', '{tbPassword.Text}', 1, {cbGroup.SelectedIndex}, '{tbEmail.Text}');";
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(sqlQuery, connection);
            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show("Регистрация прошла успешно");
            Navigation.OpenEntryUC(ParentForm);
        }

        private void RegistrationUC_Load(object sender, EventArgs e)
        {
            string sqlQuery = $"SELECT GroupName FROM Groups";
            List<string> list = new List<string> { "(Нет)" };
            list.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable(sqlQuery).Select(x => x.Field<string>("GroupName")).ToList());
            cbGroup.DataSource = list;
        }
    }
}

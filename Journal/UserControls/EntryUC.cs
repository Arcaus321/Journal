using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    public partial class EntryUC : UserControl
    {
        public EntryUC()
        {
            InitializeComponent();
        }

        private void bRegistration_Click(object sender, EventArgs e)
        {
            Navigation.OpenRegistrationUC(this.ParentForm);
            this.Dispose();
        }

        private void bLogIn_Click(object sender, EventArgs e)
        {
            string query = $"SELECT id, login, UserRole FROM Users WHERE(Login = '{textBox2.Text}' OR Login = '{textBox2.Text}') AND Password = '{textBox1.Text}' AND isDelete = 0";
            var userData = WorkWithData.ExecuteSqlQueryAsEnumerable(query);
            if(userData.Count() == 0)
            {
                MessageBox.Show("Неверное имя аккаунта или пароль");
                return;
            }

            var userId = Convert.ToInt32(userData.ToArray()[0].ItemArray[0]);
            var userRole = userData.ToArray()[0].ItemArray[2];

            switch (userRole)
            {
                case "4":
                    Navigation.OpenAdminUC(this.ParentForm);
                    break;
                case "3":
                    Navigation.OpenTeacherUC(this.ParentForm);
                    break;
                case "2":
                    Navigation.OpenStudentUC(this.ParentForm, userId);
                    break;
                default:
                    MessageBox.Show("Нет доступа");
                    break;
            }
        }
    }
}

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
    public partial class AccountFilter : UserControl
    {
        public AccountFilter()
        {
            InitializeComponent();
        }

        private void AccountFilter_Load(object sender, EventArgs e)
        {
            List<string> roles = new List<string>() { "(Нет)" };
            List<string> groups = new List<string>() { "(Нет)" };

            roles.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT RoleName FROM Roles").Select(x => x.Field<string>("RoleName")).ToList());
            groups.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList());

            cbAccessLevel.DataSource = roles;
            cbGroup.DataSource = groups;
        }

        private void cbGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)ParentForm.Controls["AdminUC"].Controls["dataGridView1"];


            string query = $@"SELECT id as ID, FirstName as Фамилия, LastName as Имя, MiddleName as Отчество, Login as Логин, Password as Пароль, UserRole, UserGroup, Email as Почта, isDelete as Удалён
                             FROM Users
                             WHERE UserGroup = {cbGroup.SelectedIndex}";
            if(cbAccessLevel.SelectedIndex != 0)
            {
                query += $" AND UserRole = {cbAccessLevel.SelectedIndex}";
            }
            DataTable usersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);

            grid.DataSource = usersTable;
        }

        private void cbAccessLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)ParentForm.Controls["AdminUC"].Controls["dataGridView1"];

            string query = $@"SELECT id as ID, FirstName as Фамилия, LastName as Имя, MiddleName as Отчество, Login as Логин, Password as Пароль, UserRole, UserGroup, Email as Почта, isDelete as Удалён
                             FROM Users
                             WHERE UserRole = {cbAccessLevel.SelectedIndex}";

            if (cbGroup.SelectedIndex != 0)
            {
                query += $" AND UserGroup = {cbGroup.SelectedIndex}";
            }
            DataTable usersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);

            grid.DataSource = usersTable;
        }
    }
}

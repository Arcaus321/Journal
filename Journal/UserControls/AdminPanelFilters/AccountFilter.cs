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
            grid.Rows.Clear();

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable($@"SELECT Users.Id, FirstName, LastName, MiddleName, Login, Password, Roles.RoleName , Groups.GroupName, Email, Users.isDelete
                                                                                       FROM Users
                                                                                       LEFT JOIN Groups ON Users.UserGroup = Groups.Id
                                                                                       LEFT JOIN Roles ON Users.UserRole = Roles.Id
                                                                                       WHERE Groups.GroupName = '{cbGroup.SelectedItem}'");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                grid.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    grid.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void cbAccessLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)ParentForm.Controls["AdminUC"].Controls["dataGridView1"];
            grid.Rows.Clear();

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable($@"SELECT Users.Id, FirstName, LastName, MiddleName, Login, Password, Roles.RoleName , Groups.GroupName, Email, Users.isDelete
                                                                                       FROM Users
                                                                                       LEFT JOIN Groups ON Users.UserGroup = Groups.Id
                                                                                       LEFT JOIN Roles ON Users.UserRole = Roles.Id
                                                                                       WHERE Roles.RoleName = '{cbAccessLevel.SelectedItem}'");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                grid.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    grid.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }
    }
}

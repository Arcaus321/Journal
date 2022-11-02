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
    public partial class StudentsFilter : UserControl
    {
        public StudentsFilter()
        {
            InitializeComponent();
        }

        private void cbGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)ParentForm.Controls["AdminUC"].Controls["dataGridView1"];

            string query = $@"SELECT id, UserGroup, FirstName, LastName, MiddleName, Users.isDelete
                              FROM Users
                              WHERE UserRole = 2 AND UserGroup = {cbGroup.SelectedIndex}";
            DataTable usersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);

            grid.DataSource = usersTable;
        }

        private void StudentsFilter_Load(object sender, EventArgs e)
        {
            List<string> groups = new List<string>() { "(Нет)" };

            groups.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList());

            cbGroup.DataSource = groups;
        }
    }
}

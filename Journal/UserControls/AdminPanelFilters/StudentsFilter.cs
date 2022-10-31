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
            grid.Rows.Clear();
            var a = cbGroup.SelectedItem;
            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable($@"SELECT GroupName, FirstName, LastName, MiddleName, Users.isDelete 
                                                                        FROM Users LEFT JOIN
                                                                             Groups g ON g.Id = Users.UserGroup 
                                                                        WHERE UserRole = 2 AND GroupName = '{cbGroup.SelectedItem}'");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                grid.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    grid.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void StudentsFilter_Load(object sender, EventArgs e)
        {
            List<string> groups = new List<string>() { "(Нет)" };

            groups.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList());

            cbGroup.DataSource = groups;
        }
    }
}

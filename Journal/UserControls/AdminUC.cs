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
    public partial class AdminUC : UserControl
    {
        public AdminUC()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    panel1.Controls.Clear();
                    panel1.Controls.Add(new AccountFilter() { Dock = DockStyle.Fill});

                    List<string> userRoleList = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT RoleName FROM Roles").Select(x => x.Field<string>("RoleName")).ToList();
                    List<string> userGroupList = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList();

                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "Id" });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "FirstName", HeaderText = "Фамилия" });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LastName", HeaderText = "Имя" });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MiddleName", HeaderText = "Отчество" });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Login", HeaderText = "Логин" });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Password", HeaderText = "Пароль" });
                    dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "UserRole", HeaderText = "Доступ", DataSource = userRoleList});
                    dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "UserGroup", HeaderText = "Группа", DataSource = userGroupList });
                    dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Email", HeaderText = "Почта" });
                    dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });

                    DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT Users.Id, FirstName, LastName, MiddleName, Login, Password, Roles.RoleName , Groups.GroupName, Email, isDelete
                                                                                       FROM Users
                                                                                       LEFT JOIN Groups ON Users.UserGroup = Groups.Id
                                                                                       LEFT JOIN Roles ON Users.UserRole = Roles.Id");

                    for(int i = 0; i < table.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                        }
                    }
                    break;
                case 4:
                    panel1.Controls.Clear();
                    panel1.Controls.Add(new StudentsFilter() { Dock = DockStyle.Fill });
                    dataGridView1.DataSource = null;
                    break;
                case 5:
                    panel1.Controls.Clear();
                    panel1.Controls.Add(new SubjectsFilter() { Dock = DockStyle.Fill });
                    break;
                default: 
                    panel1.Controls.Clear();
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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
        SQLiteDataAdapter adapter = new SQLiteDataAdapter();
        private void cbChoiceDataTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbChoiceDataTable.SelectedIndex)
            {
                case 0:
                    ViewAccountTable();
                    break;
                case 1:
                    ViewSpecializationTable();
                    break;
                case 2:
                    ViewGroupTable();
                    break;
                case 3:
                    ViewTeacherTable();
                    break;
                case 4:
                    ViewStudentsTable();
                    break;
                case 5:
                    ViewSubjectsTable();
                    break;
                default:
                    panel1.Controls.Clear();
                    break;
            }
        }

        private void ViewAccountTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();
            panel1.Controls.Add(new AccountFilter() { Dock = DockStyle.Fill });

            var userRoles = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT Id, RoleName FROM Roles").Select(x => new { Id = x.Field<string>("ID"), RoleName = x.Field<string>("RoleName") }).ToList();
            var userGroups = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT Id, GroupName FROM Groups").Select(x => new { Id = x.Field<string>("ID"), GroupName = x.Field<string>("GroupName") }).ToList(); 
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "Id" });
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "FirstName", HeaderText = "Фамилия" });
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LastName", HeaderText = "Имя" });
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MiddleName", HeaderText = "Отчество" });
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Login", HeaderText = "Логин" });
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Password", HeaderText = "Пароль" });
            //dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "UserRole", HeaderText = "Доступ", DataSource = userRoles});
            //dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "UserGroup", HeaderText = "Группа", DataSource = userGroups});
            //dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Email", HeaderText = "Почта" });
            //dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });
            ////dataGridView1.AutoGenerateColumns = true;
            //(dataGridView1.Columns["UserRole"] as DataGridViewComboBoxColumn).DataPropertyName = "UserRole";
            //(dataGridView1.Columns["UserRole"] as DataGridViewComboBoxColumn).DisplayMember = "RoleName";
            //(dataGridView1.Columns["UserRole"] as DataGridViewComboBoxColumn).ValueMember = "Id";
            //(dataGridView1.Columns["GroupName"] as DataGridViewComboBoxColumn).DataPropertyName = "UserGroup";
            //(dataGridView1.Columns["GroupName"] as DataGridViewComboBoxColumn).DisplayMember = "GroupName";
            //(dataGridView1.Columns["GroupName"] as DataGridViewComboBoxColumn).ValueMember = "Id";

            //DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT Users.Id, FirstName, LastName, MiddleName, Login, Password, Roles.RoleName, Groups.GroupName, Email, Users.isDelete
            //                                                                           FROM Users
            //                                                                           LEFT JOIN Groups ON Users.UserGroup = Groups.Id
            //                                                                           LEFT JOIN Roles ON Users.UserRole = Roles.Id");
            string query = @"SELECT Users.Id, FirstName, LastName, MiddleName, Login, Password, UserRole, UserGroup, Email, Users.isDelete
FROM Users;";
            DataTable table = new DataTable();
            SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");
            
            adapter.SelectCommand = new SQLiteCommand(query, connection);
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    dataGridView1.Rows.Add();
            //    for (int j = 0; j < table.Columns.Count; j++)
            //    {
            //        dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
            //    }
            //}
            
        }

        private void ViewAccountTable1()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();
            panel1.Controls.Add(new AccountFilter() { Dock = DockStyle.Fill });

            List<string> userRoles = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT RoleName FROM Roles").Select(x => x.Field<string>("RoleName")).ToList();
            List<string> userGroups = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "Id" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "FirstName", HeaderText = "Фамилия" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "LastName", HeaderText = "Имя" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MiddleName", HeaderText = "Отчество" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Login", HeaderText = "Логин" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Password", HeaderText = "Пароль" });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "UserRole", HeaderText = "Доступ", DataSource = userRoles });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "UserGroup", HeaderText = "Группа", DataSource = userGroups });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Email", HeaderText = "Почта" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT Users.Id, FirstName, LastName, MiddleName, Login, Password, Roles.RoleName, Groups.GroupName, Email, Users.isDelete
                                                                                       FROM Users
                                                                                       LEFT JOIN Groups ON Users.UserGroup = Groups.Id
                                                                                       LEFT JOIN Roles ON Users.UserRole = Roles.Id");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void ViewSpecializationTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "Id" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SpecializationName", HeaderText = "Название специализации", Width = 250 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "SemesterCount", HeaderText = "Кол-во семестров" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT id, SpecializationName, SemestersCount, isDelete
                                                                                       FROM Specialization");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }
        
        private void ViewGroupTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();

            List<string> specializations = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT SpecializationName FROM Specialization").Select(x => x.Field<string>("SpecializationName")).ToList();
            
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "Id" });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "Specialization", HeaderText = "Специализация", DataSource = specializations });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "GroupName", HeaderText = "Группа" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "GroupCode", HeaderText = "Код группы" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "StartStuding", HeaderText = "Начало обучения" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "EndStuding", HeaderText = "Конец обучения" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT Groups.id, SpecializationName, GroupName, GroupCode, StartStuding, EndStuding 
                                                                               FROM Groups LEFT JOIN 
                                                                                    Specialization s on s.Id = Groups.SpecializationId ");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void ViewTeacherTable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "firstName", HeaderText = "Фамилия" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "lastName", HeaderText = "Имя"});
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "middleName", HeaderText = "Отчество" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT FirstName, LastName, MiddleName, isDelete FROM Users
                                                                        WHERE UserRole = 3");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void ViewStudentsTable()
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(new StudentsFilter() { Dock = DockStyle.Fill });
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            List<string> groups = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList();

            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "groupName", HeaderText = "Группа", DataSource = groups });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "firstName", HeaderText = "Фамилия" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "lastName", HeaderText = "Имя" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "middleName", HeaderText = "Отчество" });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT GroupName, FirstName, LastName, MiddleName, Users.isDelete 
                                                                        FROM Users LEFT JOIN
                                                                             Groups g ON g.Id = Users.UserGroup 
                                                                        WHERE UserRole = 2");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void ViewSubjectsTable()
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(new SubjectsFilter() { Dock = DockStyle.Fill });
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            List<string> groups = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList();
            List<string> specializations = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT SpecializationName FROM Specialization").Select(x => x.Field<string>("SpecializationName")).ToList();
            List<string> teachers = WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT (FirstName || ' ' || LastName || ' ' || MiddleName) AS TeacherName FROM Users WHERE UserRole = 3").Select(x => x.Field<string>("TeacherName")).ToList();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "Id" });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "specializations", HeaderText = "Специальность", DataSource = specializations });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "group", HeaderText = "Группа", DataSource = groups });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "subject", HeaderText = "Предмет" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "hours", HeaderText = "Часы" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "semester", HeaderText = "Семестр" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn() { Name = "description", HeaderText = "Описание" });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "teacher", HeaderText = "Преподаватель", DataSource = teachers });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "isDelete", HeaderText = "Удалён" });

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable(@"SELECT Subjects.Id, SpecializationName, GroupName, SubjectName, Hours, Semester, Description, (FirstName || ' ' || LastName || ' ' || MiddleName) AS TeacherName, Subjects.IsDelete FROM Subjects 
LEFT JOIN Specialization s ON s.Id = Subjects.SpecializationId
LEFT JOIN Groups g ON g.Id = Subjects.GroupId 
LEFT JOIN Users u ON u.Id = Subjects.Teacher ");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void bWriteToBase_Click(object sender, EventArgs e)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");

            SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(adapter);
            string a1 = commandBuilder.GetUpdateCommand().CommandText;
            string a2 = commandBuilder.GetInsertCommand().CommandText;
            string a3 = commandBuilder.GetDeleteCommand().CommandText;

            adapter.SelectCommand = new SQLiteCommand(a1, connection);

            adapter.Update(dataGridView1.DataSource as DataTable);
        }
    }
}

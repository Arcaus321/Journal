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
        SQLiteDataAdapter adapter;
        SQLiteCommandBuilder commandBuilder;
        public string query;
        private void cbChoiceDataTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataTable();
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            UpdateDataTable();
        }
        private void UpdateDataTable()
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
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();
            panel1.Controls.Add(new AccountFilter() { Dock = DockStyle.Fill });

            DataTable userRoles = WorkWithData.ExecuteSqlQueryAsDataTable("SELECT Id, RoleName FROM Roles");
            DataTable userGroups = WorkWithData.ExecuteSqlQueryAsDataTable("SELECT Id, GroupName FROM Groups");

            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { 
                Name = "UserRole", 
                DataPropertyName = "UserRole", 
                HeaderText = "Доступ", 
                DataSource = userRoles, 
                ValueMember = "Id", 
                DisplayMember = "RoleName"
            });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { 
                Name = "UserGroup", 
                DataPropertyName = "UserGroup", 
                HeaderText = "Группа", 
                DataSource = userGroups, 
                ValueMember = "Id", 
                DisplayMember = "GroupName" 
            });

            query = @"SELECT id as ID, FirstName as Фамилия, LastName as Имя, MiddleName as Отчество, Login as Логин, Password as Пароль, UserRole, UserGroup, Email as Почта, isDelete as Удалён
                      FROM Users";
            DataTable usersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);

            dataGridView1.DataSource = usersTable;
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["UserRole"].DisplayIndex = 7;
            dataGridView1.Columns["UserGroup"].DisplayIndex = 4;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void ViewSpecializationTable()
        {
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();

            query = @"SELECT id, SpecializationName as Специализация, SemestersCount as 'Количество семестров', isDelete as Удален
                      FROM Specialization";
            DataTable usersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            dataGridView1.DataSource = usersTable;
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["Специализация"].Width = 250;
            dataGridView1.AllowUserToAddRows = true;
        }
        
        private void ViewGroupTable()
        {
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();

            DataTable specializations = WorkWithData.ExecuteSqlQueryAsDataTable("SELECT id, SpecializationName FROM Specialization");

            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() {
                Name = "Specialization",
                DataPropertyName = "SpecializationId",
                HeaderText = "Специализация", 
                DataSource = specializations,
                ValueMember="Id",
                DisplayMember= "SpecializationName"
            });

            query = @"SELECT id, SpecializationId, GroupName, GroupCode, StartStuding, EndStuding 
                      FROM Groups";
            DataTable groupTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            dataGridView1.DataSource = groupTable;
            dataGridView1.Columns["Specialization"].DisplayIndex = 1;
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.AllowUserToAddRows = true;
        }

        private void ViewTeacherTable()
        {
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();

            query = @"SELECT id, FirstName, LastName, MiddleName, isDelete 
                      FROM Users
                      WHERE UserRole = 3";
            DataTable teachersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            dataGridView1.DataSource = teachersTable;
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void ViewStudentsTable()
        {
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();
            panel1.Controls.Add(new StudentsFilter() { Dock = DockStyle.Fill });

            DataTable groups = WorkWithData.ExecuteSqlQueryAsDataTable("SELECT id, (GroupName || ' (' || GroupCode || ')') as GroupName FROM Groups");

            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = "Group",
                DataPropertyName = "UserGroup",
                HeaderText = "Группа",
                DataSource = groups,
                ValueMember = "Id",
                DisplayMember = "GroupName"
            });

            query = @"SELECT id, UserGroup, FirstName, LastName, MiddleName, Users.isDelete
                      FROM Users
                      WHERE UserRole = 2";
            DataTable studentsTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            dataGridView1.DataSource = studentsTable;
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["ID"].DisplayIndex = 0;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void ViewSubjectsTable()
        {
            dataGridView1.Columns.Clear();
            panel1.Controls.Clear();
            panel1.Controls.Add(new SubjectsFilter() { Dock = DockStyle.Fill });

            DataTable groups = WorkWithData.ExecuteSqlQueryAsDataTable("SELECT id, GroupName FROM Groups");
            DataTable specializations = WorkWithData.ExecuteSqlQueryAsDataTable("SELECT id, SpecializationName FROM Specialization");
            DataTable teachers = WorkWithData.ExecuteSqlQueryAsDataTable("SELECT id, (FirstName || ' ' || LastName || ' ' || MiddleName) AS TeacherName FROM Users WHERE UserRole = 3");

            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "specializations", DataPropertyName = "SpecializationId", HeaderText = "Специальность", DataSource = specializations, ValueMember = "Id", DisplayMember = "SpecializationName"});
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "group", DataPropertyName = "GroupId", HeaderText = "Группа", DataSource = groups, ValueMember = "Id", DisplayMember = "GroupName" });
            dataGridView1.Columns.Add(new DataGridViewComboBoxColumn() { Name = "teacher", DataPropertyName = "Teacher", HeaderText = "Преподаватель", DataSource = teachers, ValueMember = "Id", DisplayMember = "TeacherName" });

            query = @"SELECT id, SpecializationId, GroupId, SubjectName, Hours, Semester, Description, Teacher, IsDelete FROM Subjects";
            DataTable subjectsTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            dataGridView1.DataSource = subjectsTable;
            dataGridView1.Columns["ID"].ReadOnly = true;
            dataGridView1.Columns["ID"].DisplayIndex = 0;
            dataGridView1.Columns["group"].DisplayIndex = 1;
            dataGridView1.Columns["teacher"].DisplayIndex = 6;
            dataGridView1.AllowUserToAddRows = true;
        }

        private void bWriteToBase_Click(object sender, EventArgs e)
        {
            if(cbChoiceDataTable.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите раздел");
                return;
            }
            SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");
            connection.Open();
            adapter = new SQLiteDataAdapter(query, connection);
            commandBuilder = new SQLiteCommandBuilder(adapter);
            string comd = commandBuilder.GetUpdateCommand().CommandText;

            try
            {
                adapter.SelectCommand = commandBuilder.GetUpdateCommand();
                adapter.Update(dataGridView1.DataSource as DataTable);
                UpdateDataTable();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}

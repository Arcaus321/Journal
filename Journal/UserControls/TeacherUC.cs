using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    public partial class TeacherUC : UserControl
    {
        private int TeacherId;
        string query;

        List<string> marks = new List<string> { "","5", "4", "3", "2", "Зачёт", "Н"};
        public TeacherUC(int id)
        {
            InitializeComponent();
            TeacherId = id;
        }

        private void TeacherUC_Load(object sender, EventArgs e)
        {
            query = $@"SELECT (FirstName || ' ' || LastName || ' ' || MiddleName) as Name FROM Users
                       WHERE id = {TeacherId}";
            labelTeacherName.Text = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => x.Field<string>("Name")).First(); ;

            query = $@"Select DISTINCT GroupId, (g.GroupName || ' (' || g.GroupCode || ')') as GroupName FROM Subjects LEFT JOIN
                       Groups g on g.Id = GroupId
                       WHERE Teacher = {TeacherId} AND 
                             g.isDelete = 0 AND 
                             Subjects.isDelete = 0";
            var groups = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new { Id = x.Field<Int64>("GroupId"), GroupName = x.Field<string>("GroupName")}).ToList();
            cbGroup.DataSource = groups;
            cbGroup.DisplayMember = "GroupName";
            cbGroup.ValueMember = "Id";

            UpdateFilter();
        }

        private void UpdateFilter()
        {

            query = $@"Select DISTINCT SubjectName FROM Subjects
                       WHERE GroupId = {cbGroup.SelectedValue} AND Teacher = {TeacherId} AND isDelete = 0";
            var subjects = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => x.Field<string>("SubjectName") ).ToList();
            
            cbSubject.DataSource = subjects;

            query = $@"Select DISTINCT Semester FROM Subjects
                       WHERE GroupId = {cbGroup.SelectedValue} AND Teacher = {TeacherId} AND SubjectName = '{cbSubject.Text}' AND isDelete = 0";
            var semesters = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => x.Field<Int64>("Semester")).ToList();

            cbSemester.DataSource = semesters;
        }

        private void cbGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void cbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            query = $@"Select Semester FROM Subjects
                       WHERE GroupId = {cbGroup.SelectedValue} AND Teacher = {TeacherId} AND SubjectName = '{cbSubject.Text}' AND isDelete = 0";
            var semesters = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => x.Field<Int64>("Semester")).ToList();

            cbSemester.DataSource = semesters;
        }

        private void FillRightTable()
        {
            dataGridView2.Columns.Clear();
            query = "Select id, Name From MarksTypes";

            var marksTypes = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new {Id = x.Field<Int64>("id"), Name = x.Field<string>("Name")}).ToList();

            dataGridView2.Columns.Add(new DataGridViewComboBoxColumn()
            {
                Name = "MarkType",
                DataPropertyName = "Тип",
                HeaderText = "Тип",
                DataSource = marksTypes,
                ValueMember = "Id",
                DisplayMember = "Name"
            });

            query = $@"SELECT DISTINCT [data] as Дата, description as Описание, markType as Тип FROM Marks 
                       WHERE subject = (SELECT id FROM Subjects 
                                        WHERE GroupId = {cbGroup.SelectedValue} AND 
                                              Semester = {cbSemester.SelectedValue} AND 
                                              SubjectName = '{cbSubject.Text}' AND 
                                              Teacher = {TeacherId}) AND
                             isDelete = 0";

            dataGridView2.DataSource = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            dataGridView2.Columns["markType"].DisplayIndex = 2;
        }

        public void FillLeftTable()
        {
            dataGridView1.Columns.Clear();
            query = $@"Select id, (FirstName || ' ' || LastName || ' ' || MiddleName) as 'Ф.И.О.' from Users
                       WHERE UserRole = 2 AND UserGroup = {cbGroup.SelectedValue} AND isDelete = 0";

            var table = WorkWithData.ExecuteSqlQueryAsDataTable(query);
            var students = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new { Id = x.Field<Int64>("id"), SubjectName = x.Field<string>("Ф.И.О.") }).ToList();


            int f = students.FindIndex(x => x.Id == 4);
            //dataGridView1.DataSource = users;

            query = $@"SELECT student, mark, data FROM Marks 
                       WHERE subject = (SELECT id FROM Subjects 
                                        WHERE GroupId = {cbGroup.SelectedValue} AND 
                                              Semester = {cbSemester.SelectedValue} AND 
                                              SubjectName = '{cbSubject.Text}' AND 
                                              Teacher = {TeacherId}) AND
                             isDelete = 0";
            var marsds = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new { Id = x.Field<Int64>("student"), Mark = x.Field<string>("mark"), Date = x.Field<string>("data") }).ToList();
            var dates = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => x.Field<string>("data")).Distinct().ToList();

            for(int i = 0; i < dates.Count; i++)
            {
                table.Columns.Add(new DataColumn()
                {
                    ColumnName = dates[i]
                });
                dataGridView1.Columns.Add(new DataGridViewComboBoxColumn()
                {
                    HeaderText = dates[i],
                    Name = dates[i],
                    DataPropertyName = dates[i],
                    DataSource = marks
                });
            }
            foreach(var item in marsds)
            {
                table.Rows[students.FindIndex(x => x.Id == item.Id)][item.Date] = item.Mark;
            }
            dataGridView1.DataSource = table;
            dataGridView1.Columns["id"].DisplayIndex = 0;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Ф.И.О."].DisplayIndex = 1;
        }


        public void WriteToTable()
        {
            //query = "SELECT student, subject, data FROM Marks";
            if(dataGridView1.Columns.Count <= 2)
            {
                return;
            }
            query = $@"SELECT subject, student, mark, data FROM Marks 
                       WHERE subject = (SELECT id FROM Subjects 
                                        WHERE GroupId = {cbGroup.SelectedValue} AND 
                                              Semester = {cbSemester.SelectedValue} AND 
                                              SubjectName = '{cbSubject.Text}' AND 
                                              Teacher = {TeacherId} AND 
                                              isDelete = 0) AND 
                             isDelete = 0";
            var mar = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new { student = x.Field<Int64>("student"), subject = x.Field<Int64>("subject"), date = x.Field<string>("data") }).ToList();
            SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");
            connection.Open();
            query = $"SELECT id FROM Subjects WHERE GroupId = {cbGroup.SelectedValue} AND SubjectName = '{cbSubject.Text}' AND Semester = {cbSemester.SelectedValue} AND isDelete = 0";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            var subjectId = command.ExecuteScalar();
            query = $@"REPLACE INTO Marks (mark, data, student, subject, description, markType, isDelete) VALUES ";
            for (int i = 2; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {   
                    if ((dataGridView1.Rows[j].Cells[i].Value as string) != null)
                    {
                        query += $@"('{dataGridView1.Rows[j].Cells[i].Value}', 
                                           '{dataGridView1.Columns[i].HeaderText}', 
                                            {dataGridView1.Rows[j].Cells[0].Value}, 
                                            {subjectId}, 
                                           '{dataGridView2.Rows[i - 2].Cells[1].Value}', 
                                            {dataGridView2.Rows[i - 2].Cells[2].Value},
                                            0),";
                    }
                    else if(mar.Exists(x => x.date == dataGridView1.Columns[i].HeaderText && x.student == Convert.ToInt64(dataGridView1.Rows[j].Cells[0].Value) && x.subject == Convert.ToInt64(subjectId)))
                    {
                        query += $@"('{dataGridView1.Rows[j].Cells[i].Value}', 
                                           '{dataGridView1.Columns[i].HeaderText}', 
                                            {dataGridView1.Rows[j].Cells[0].Value}, 
                                            {subjectId}, 
                                           '{dataGridView2.Rows[i - 2].Cells[1].Value}', 
                                            {dataGridView2.Rows[i - 2].Cells[2].Value},
                                            1),";
                    }
                }
            }
            query = query.Remove(query.Length - 1, 1);
            query += ";";
            command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WriteToTable();
        }

        private void bShowMarks_Click(object sender, EventArgs e)
        {
            labelGroupInfo.Text = "";
            query = $"select SpecializationName from Specialization WHERE id = (select SpecializationId from groups WHERE groups.id = {cbGroup.SelectedValue}  AND isDelete = 0) AND isDelete = 0";
            SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            labelGroupInfo.Text += command.ExecuteScalar() + "\r";
            labelGroupInfo.Text += "Группа "+ cbGroup.Text + "\r";
            query = $@"SELECT Hours, Description FROM Subjects
                       WHERE GroupId = {cbGroup.SelectedValue} AND SubjectName = '{cbSubject.Text}' AND Semester = {cbSemester.SelectedValue} AND isDelete = 0";
            var result = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new {Hours = x.Field<Int64>("Hours"), Description = x.Field<string>("Description")}).ToList();
            labelGroupInfo.Text += cbSemester.SelectedValue + " семестр\r";
            labelGroupInfo.Text += cbSubject.Text + " " + result[0].Hours + " часов " + result[0].Description;
            connection.Close();
            FillRightTable();
            FillLeftTable();
        }

        private void dataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var cells = dataGridView2.SelectedCells;
            DialogResult result = MessageBox.Show($"Вы действительно хотите удалить оценки за {cells[0].Value}", "Предупреждение", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                dataGridView1.Columns.Remove(cells[0].Value as string);
                SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");
                connection.Open();
                query = $"SELECT id FROM Subjects WHERE GroupId = {cbGroup.SelectedValue} AND SubjectName = '{cbSubject.Text}' AND Semester = {cbSemester.SelectedValue}  AND isDelete = 0";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                var subjectId = command.ExecuteScalar();
                query = $@"UPDATE Marks SET isDelete = 1 WHERE data = '{cells[0].Value}' AND Subject = {subjectId}";
                command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }

        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source = database.db");
            if(e.ColumnIndex == 0)
            {
                connection.Open();
                try
                {
                    query = $"SELECT id FROM Subjects WHERE GroupId = {cbGroup.SelectedValue} AND SubjectName = '{cbSubject.Text}' AND Semester = {cbSemester.SelectedValue} AND isDelete = 0";
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    var subjectId = command.ExecuteScalar();
                    query = $@"UPDATE Marks SET data = '{dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value}' 
                               WHERE data = '{dataGridView1.Columns[e.RowIndex + 2].HeaderText}' AND
                                     subject = {subjectId} AND
                                     isDelete = 0";
                    command = new SQLiteCommand(query, connection);
                    command.ExecuteNonQuery();
                    dataGridView1.Columns[e.RowIndex + 2].HeaderText = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
                }
                catch(ArgumentOutOfRangeException)
                {
                    dataGridView1.Columns.Add(new DataGridViewComboBoxColumn()
                    {
                        HeaderText = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string,
                        DataPropertyName = dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string,
                        DataSource = marks
                    });
                }
            }
            connection.Close();
        }
    }
}
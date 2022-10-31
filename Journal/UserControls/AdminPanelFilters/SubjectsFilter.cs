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
    public partial class SubjectsFilter : UserControl
    {
        public SubjectsFilter()
        {
            InitializeComponent();
        }

        private void cbSemester_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)ParentForm.Controls["AdminUC"].Controls["dataGridView1"];
            grid.Rows.Clear();

            if(cbSemester.SelectedIndex == 0)
            {
                return;
            }
            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable($@"SELECT Subjects.Id, SpecializationName, GroupName, SubjectName, Hours, Semester, Description, (FirstName || ' ' || LastName || ' ' || MiddleName) AS TeacherName, Subjects.IsDelete FROM Subjects 
                                                                         LEFT JOIN Specialization s ON s.Id = Subjects.SpecializationId
                                                                         LEFT JOIN Groups g ON g.Id = Subjects.GroupId 
                                                                         LEFT JOIN Users u ON u.Id = Subjects.Teacher
                                                                         WHERE Semester = {cbSemester.SelectedItem}");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                grid.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    grid.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void sbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)ParentForm.Controls["AdminUC"].Controls["dataGridView1"];
            grid.Rows.Clear();

            if (cbSemester.SelectedIndex == 0)
            {
                return;
            }

            DataTable table = WorkWithData.ExecuteSqlQueryAsDataTable($@"SELECT Subjects.Id, SpecializationName, GroupName, SubjectName, Hours, Semester, Description, (FirstName || ' ' || LastName || ' ' || MiddleName) AS TeacherName, Subjects.IsDelete FROM Subjects 
                                                                         LEFT JOIN Specialization s ON s.Id = Subjects.SpecializationId
                                                                         LEFT JOIN Groups g ON g.Id = Subjects.GroupId 
                                                                         LEFT JOIN Users u ON u.Id = Subjects.Teacher
                                                                         WHERE SubjectName = '{cbSubject.SelectedItem}'");

            for (int i = 0; i < table.Rows.Count; i++)
            {
                grid.Rows.Add();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    grid.Rows[i].Cells[j].Value = table.Rows[i].ItemArray[j];
                }
            }
        }

        private void SubjectsFilter_Load(object sender, EventArgs e)
        {
            List<string> semesters = new List<string>() { "(Нет)" };
            List<string> subjects = new List<string>() { "(Нет)" };

            subjects.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT DISTINCT SubjectName FROM Subjects").Select(x => x.Field<string>("SubjectName")).ToList());
            semesters.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT DISTINCT Semester FROM Subjects").Select(x => x.Field<string>("Semester")).ToList());

            cbSubject.DataSource = subjects;
            cbSemester.DataSource = semesters;
        }
    }
}

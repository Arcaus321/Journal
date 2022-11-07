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


            string query = $@"SELECT id, SpecializationId, GroupId, SubjectName, Hours, Semester, Description, Teacher, IsDelete 
                              FROM Subjects
                              WHERE Semester = '{cbSemester.SelectedIndex}'";
            if (cbSubject.SelectedIndex != 0)
            {
                query += $" AND SubjectName = '{cbSubject.SelectedValue}'";
            }
            DataTable usersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);

            grid.DataSource = usersTable;
        }

        private void sbSubject_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)ParentForm.Controls["AdminUC"].Controls["dataGridView1"];


            string query = $@"SELECT id, SpecializationId, GroupId, SubjectName, Hours, Semester, Description, Teacher, IsDelete 
                              FROM Subjects
                              WHERE SubjectName = '{cbSubject.SelectedValue}'";
            if (cbSemester.SelectedIndex != 0)
            {
                query += $" AND Semester = {cbSemester.SelectedIndex}";
            }
            DataTable usersTable = WorkWithData.ExecuteSqlQueryAsDataTable(query);

            grid.DataSource = usersTable;
        }

        private void SubjectsFilter_Load(object sender, EventArgs e)
        {
            List<string> semesters = new List<string>() { "(Нет)" };
            List<string> subjects = new List<string>() { "(Нет)" };

            subjects.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT DISTINCT SubjectName FROM Subjects").Select(x => x.Field<string>("SubjectName")).ToList());
            semesters.AddRange(WorkWithData.ExecuteSqlQueryAsEnumerable("SELECT DISTINCT Semester FROM Subjects").Select(x => Convert.ToString(x.Field<Int64>("Semester"))).ToList());

            cbSubject.DataSource = subjects;
            cbSemester.DataSource = semesters;
        }
    }
}

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
    public partial class StudentUC : UserControl
    {
        private User User;
        public StudentUC(int userId)
        {
            InitializeComponent();
            User = User.GetUser(userId);
        }

        private void bShowMarks_Click(object sender, EventArgs e)
        {
            GetTeacherInfo();
            var subjectId = cbSubject.SelectedValue;
            var semester = cbSemester.SelectedValue;
            string query = $@"SELECT mark as Оценка, data as Дата, m.description as Описание FROM Marks m LEFT JOIN
                              Subjects s on s.id = m.subject 
                              WHERE student = {User.Id} AND subject = {subjectId} AND s.Semester = {semester} AND m.markType = 1 AND m.isDelete = 0 AND s.isDelete = 0";
            dataGridView1.DataSource = WorkWithData.ExecuteSqlQueryAsDataTable(query);
        }

        private void StudentUC_Load(object sender, EventArgs e)
        {
            labelStudentInfo.Text += User.Name + "\r";
            labelStudentInfo.Text += User.Specialization + "\r";
            labelStudentInfo.Text += User.Group + "\r";

            cbSubject.DataSource = User.GetSubjects();
            cbSubject.DisplayMember = "SubjectName";
            cbSubject.ValueMember = "id";

            cbSemester.DataSource = User.GetSemesters();
            cbSemester.DisplayMember = "Semester";
            cbSemester.ValueMember = "Semester";
        }

        private void bCreditBook_Click(object sender, EventArgs e)
        {
            string query = $@"Select s.SubjectName as Предмет, s.Hours as Часы, s.Semester as Семестр, m.mark as Оценка, m.data as Дата, mt.Name as Тип, (u.FirstName || ' ' || u.LastName || ' ' || u.MiddleName) as Преподаватель from Marks m LEFT JOIN 
                              Subjects s on s.Id = m.subject LEFT JOIN
                              MarksTypes mt on mt.id = m.markType LEFT JOIN
                              Users u on u.Id = s.Teacher
                              WHERE m.markType != 1 AND student = {User.Id} AND s.IsDelete = 0 AND m.isDelete = 0
                              ORDER BY s.Semester";

            labelTeacher.Text = String.Empty;
            dataGridView1.DataSource = WorkWithData.ExecuteSqlQueryAsDataTable(query);
        }

        private void GetTeacherInfo()
        {
            var query = $@"SELECT (u.FirstName || ' ' || u.LastName || ' ' || u.MiddleName) as Name, s.SubjectName, s.Hours from Subjects s LEFT JOIN
                           Users u on u.id = s.teacher
                           WHERE s.Id = {cbSubject.SelectedValue} AND s.Semester = {cbSemester.SelectedValue} AND s.IsDelete = 0 AND u.isDelete = 0";
            var teacherInfo = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new { name = x.Field<string>("Name"), subjectName = x.Field<string>("SubjectName"), hours = x.Field<Int64>("Hours") }).ToList();
            if(teacherInfo.Count > 0)
            {
                labelTeacher.Text = String.Empty;
                labelTeacher.Text += "Преподаватель: " + teacherInfo[0].name + "\r";
                labelTeacher.Text += "Предмет: " + teacherInfo[0].subjectName + " ";
                labelTeacher.Text += teacherInfo[0].hours + "; часов";
            }
        }
    }
}

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
            var subjectId = cbSubject.SelectedValue;
            string query = $@"SELECT mark as Оценка, data as Дата, description as Описание FROM Marks
                             WHERE student = {User.Id} AND subject = {subjectId}";
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

        }
    }
}

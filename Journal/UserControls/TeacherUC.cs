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
    public partial class TeacherUC : UserControl
    {
        private int TeacherId;
        string query;
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

            query = $@"Select GroupId, (g.GroupName || ' (' || g.GroupCode || ')') as GroupName FROM Subjects LEFT JOIN
                       Groups g on g.Id = GroupId
                       WHERE Teacher = 2";
            var groups = WorkWithData.ExecuteSqlQueryAsEnumerable(query).Select(x => new { Id = x.Field<Int64>("GroupId"), GroupName = x.Field<string>("GroupName")}).ToList();
            cbGroup.DataSource = groups;
            cbGroup.DisplayMember = "GroupName";
            cbGroup.ValueMember = "Id";

            SQLiteCommand cmd = new SQLiteCommand();
            //cmd.Parameters.Add()
        }
    }
}
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
                    dataGridView1.DataSource = WorkWithData.ExecuteSqlQueryAsDataTable("Select * From Users");
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

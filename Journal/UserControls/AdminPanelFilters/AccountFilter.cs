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
    public partial class AccountFilter : UserControl
    {
        public AccountFilter()
        {
            InitializeComponent();
        }

        private void AccountFilter_Load(object sender, EventArgs e)
        {
            List<string> roles = new List<string>() { "(Нет)" };
            List<string> groups = new List<string>() { "(Нет)" };

            roles.AddRange(Sql.ExecuteSqlQueryAsEnumerable("SELECT RoleName FROM Roles").Select(x => x.Field<string>("RoleName")).ToList());
            groups.AddRange(Sql.ExecuteSqlQueryAsEnumerable("SELECT GroupName FROM Groups").Select(x => x.Field<string>("GroupName")).ToList());

            cbAccessLevel.DataSource = roles;
            cbGroup.DataSource = groups;
        }
    }
}

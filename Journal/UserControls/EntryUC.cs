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
    public partial class EntryUC : UserControl
    {
        public EntryUC()
        {
            InitializeComponent();
        }

        private void bRegistration_Click(object sender, EventArgs e)
        {
            Navigation.OpenRegistrationUC(this.ParentForm);
            this.Dispose();
            //form1.ActiveForm.Text = "Журнал - Регистрация нового пользователя";
            //this.Controls.Clear();
            //Controls.Add(new RegistrationUC { Dock = DockStyle.Fill });
        }

        private void bLogIn_Click(object sender, EventArgs e)
        {
            form1.ActiveForm.Text = "Журнал - Панель админа";
            form1.ActiveForm.FormBorderStyle = FormBorderStyle.Sizable;
            form1.ActiveForm.MaximizeBox = true;
            this.Controls.Clear();
            Controls.Add(new StudentUC { Dock = DockStyle.Fill });
        }
    }
}

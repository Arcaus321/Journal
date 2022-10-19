using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal
{
    public static class Navigation
    {
        public static void OpenEntryUC(Form form)
        {
            form.Text = "Журнал - Вход";
            form.Controls.Clear();
            form.Controls.Add(new EntryUC { Dock = DockStyle.Fill });
        }

        public static void OpenRegistrationUC(Form form)
        {
            form.Text = "Журнал - Регистрация нового пользователя";
            form.Controls.Clear();
            form.Controls.Add(new RegistrationUC { Dock = DockStyle.Fill });
        }
    }
}

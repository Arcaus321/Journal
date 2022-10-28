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

        public static void OpenAdminUC(Form form)
        {
            form.Text = "Журнал - Панель админа";
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.Controls.Clear();
            form.Controls.Add(new AdminUC { Dock = DockStyle.Fill }); 
        }
        
        public static void OpenTeacherUC(Form form)
        {
            form.Text = "Журнал - Панель преподавателя";
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.Controls.Clear();
            form.Controls.Add(new TeacherUC { Dock = DockStyle.Fill }); 
        }
        
        public static void OpenStudentUC(Form form)
        {
            form.Text = "Журнал - Оценки";
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.Controls.Clear();
            form.Controls.Add(new StudentUC { Dock = DockStyle.Fill }); 
        }
    }
}

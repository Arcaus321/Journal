﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
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
            form.Size = new Size(1200, 800) ;
        }
        
        public static void OpenTeacherUC(Form form, int userId)
        {
            form.Text = "Журнал - Панель преподавателя";
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.Controls.Clear();
            form.Controls.Add(new TeacherUC(userId) { Dock = DockStyle.Fill });
            form.Size = new Size(1200, 800);
        }
        
        public static void OpenStudentUC(Form form, int userId)
        {
            form.Text = "Журнал - Оценки";
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.Controls.Clear();
            form.Controls.Add(new StudentUC(userId) { Dock = DockStyle.Fill });
            form.Size = new Size(1200, 800);
        }
    }
}

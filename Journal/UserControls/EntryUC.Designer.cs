namespace Journal
{
    partial class EntryUC
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbLogIn = new System.Windows.Forms.GroupBox();
            this.labelPasswd = new System.Windows.Forms.Label();
            this.bRegistration = new System.Windows.Forms.Button();
            this.tbPasswd = new System.Windows.Forms.TextBox();
            this.bLogIn = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.tbLogin = new System.Windows.Forms.TextBox();
            this.gbLogIn.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbLogIn
            // 
            this.gbLogIn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbLogIn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbLogIn.Controls.Add(this.labelPasswd);
            this.gbLogIn.Controls.Add(this.bRegistration);
            this.gbLogIn.Controls.Add(this.tbPasswd);
            this.gbLogIn.Controls.Add(this.bLogIn);
            this.gbLogIn.Controls.Add(this.labelLogin);
            this.gbLogIn.Controls.Add(this.tbLogin);
            this.gbLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbLogIn.Location = new System.Drawing.Point(6, 4);
            this.gbLogIn.Name = "gbLogIn";
            this.gbLogIn.Size = new System.Drawing.Size(241, 207);
            this.gbLogIn.TabIndex = 4;
            this.gbLogIn.TabStop = false;
            this.gbLogIn.Text = "Вход";
            // 
            // labelPasswd
            // 
            this.labelPasswd.AutoSize = true;
            this.labelPasswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPasswd.Location = new System.Drawing.Point(21, 90);
            this.labelPasswd.Name = "labelPasswd";
            this.labelPasswd.Size = new System.Drawing.Size(45, 13);
            this.labelPasswd.TabIndex = 5;
            this.labelPasswd.Text = "Пароль";
            // 
            // bRegistration
            // 
            this.bRegistration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bRegistration.Location = new System.Drawing.Point(129, 155);
            this.bRegistration.Name = "bRegistration";
            this.bRegistration.Size = new System.Drawing.Size(89, 36);
            this.bRegistration.TabIndex = 1;
            this.bRegistration.Text = "Регистрация";
            this.bRegistration.UseVisualStyleBackColor = true;
            this.bRegistration.Click += new System.EventHandler(this.bRegistration_Click);
            // 
            // tbPasswd
            // 
            this.tbPasswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbPasswd.Location = new System.Drawing.Point(24, 106);
            this.tbPasswd.Name = "tbPasswd";
            this.tbPasswd.PasswordChar = '*';
            this.tbPasswd.Size = new System.Drawing.Size(194, 20);
            this.tbPasswd.TabIndex = 4;
            this.tbPasswd.UseSystemPasswordChar = true;
            // 
            // bLogIn
            // 
            this.bLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bLogIn.Location = new System.Drawing.Point(24, 155);
            this.bLogIn.Name = "bLogIn";
            this.bLogIn.Size = new System.Drawing.Size(89, 36);
            this.bLogIn.TabIndex = 0;
            this.bLogIn.Text = "Войти";
            this.bLogIn.UseVisualStyleBackColor = true;
            this.bLogIn.Click += new System.EventHandler(this.bLogIn_Click);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLogin.Location = new System.Drawing.Point(21, 40);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(38, 13);
            this.labelLogin.TabIndex = 3;
            this.labelLogin.Text = "Логин";
            // 
            // tbLogin
            // 
            this.tbLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbLogin.Location = new System.Drawing.Point(24, 56);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Size = new System.Drawing.Size(194, 20);
            this.tbLogin.TabIndex = 2;
            // 
            // EntryUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbLogIn);
            this.Name = "EntryUC";
            this.Size = new System.Drawing.Size(255, 216);
            this.gbLogIn.ResumeLayout(false);
            this.gbLogIn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLogIn;
        private System.Windows.Forms.Label labelPasswd;
        private System.Windows.Forms.Button bRegistration;
        private System.Windows.Forms.TextBox tbPasswd;
        private System.Windows.Forms.Button bLogIn;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.TextBox tbLogin;
    }
}

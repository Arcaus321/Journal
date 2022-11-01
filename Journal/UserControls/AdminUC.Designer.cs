namespace Journal
{
    partial class AdminUC
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbChoiceDataTable = new System.Windows.Forms.ComboBox();
            this.bUpdate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bWriteToBase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 49);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(795, 510);
            this.dataGridView1.TabIndex = 0;
            // 
            // cbChoiceDataTable
            // 
            this.cbChoiceDataTable.FormattingEnabled = true;
            this.cbChoiceDataTable.Items.AddRange(new object[] {
            "Аккаунты",
            "Специальности",
            "Группы",
            "Преподаватели",
            "Студенты",
            "Предметы"});
            this.cbChoiceDataTable.Location = new System.Drawing.Point(9, 20);
            this.cbChoiceDataTable.Name = "cbChoiceDataTable";
            this.cbChoiceDataTable.Size = new System.Drawing.Size(139, 21);
            this.cbChoiceDataTable.TabIndex = 1;
            this.cbChoiceDataTable.SelectedIndexChanged += new System.EventHandler(this.cbChoiceDataTable_SelectedIndexChanged);
            // 
            // bUpdate
            // 
            this.bUpdate.Location = new System.Drawing.Point(154, 20);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(114, 21);
            this.bUpdate.TabIndex = 2;
            this.bUpdate.Text = "Выбрать/Обновить";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(274, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 40);
            this.panel1.TabIndex = 3;
            // 
            // bWriteToBase
            // 
            this.bWriteToBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bWriteToBase.Location = new System.Drawing.Point(713, 20);
            this.bWriteToBase.Name = "bWriteToBase";
            this.bWriteToBase.Size = new System.Drawing.Size(86, 23);
            this.bWriteToBase.TabIndex = 4;
            this.bWriteToBase.Text = "Сохранить";
            this.bWriteToBase.UseVisualStyleBackColor = true;
            this.bWriteToBase.Click += new System.EventHandler(this.bWriteToBase_Click);
            // 
            // AdminUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bWriteToBase);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.cbChoiceDataTable);
            this.Name = "AdminUC";
            this.Size = new System.Drawing.Size(811, 565);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbChoiceDataTable;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bWriteToBase;
    }
}

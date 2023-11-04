namespace library_project
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.группыКнигToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.издателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.авторыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.книгиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.читателиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экземплярToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.утерянныеКнигиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выданныеКнигиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvReaders = new System.Windows.Forms.DataGridView();
            this.dgvBooksAndInstances = new System.Windows.Forms.DataGridView();
            this.dgvBookLoans = new System.Windows.Forms.DataGridView();
            this.btnAddLoan = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchReader = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReaders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooksAndInstances)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookLoans)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникиToolStripMenuItem,
            this.читателиToolStripMenuItem,
            this.экземплярToolStripMenuItem,
            this.отчетыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(991, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникиToolStripMenuItem
            // 
            this.справочникиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.группыКнигToolStripMenuItem,
            this.издателиToolStripMenuItem,
            this.авторыToolStripMenuItem,
            this.книгиToolStripMenuItem});
            this.справочникиToolStripMenuItem.Name = "справочникиToolStripMenuItem";
            this.справочникиToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.справочникиToolStripMenuItem.Text = "Справочники";
            // 
            // группыКнигToolStripMenuItem
            // 
            this.группыКнигToolStripMenuItem.Name = "группыКнигToolStripMenuItem";
            this.группыКнигToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.группыКнигToolStripMenuItem.Text = "Группы книг";
            this.группыКнигToolStripMenuItem.Click += new System.EventHandler(this.группыКнигToolStripMenuItem_Click);
            // 
            // издателиToolStripMenuItem
            // 
            this.издателиToolStripMenuItem.Name = "издателиToolStripMenuItem";
            this.издателиToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.издателиToolStripMenuItem.Text = "Издатели";
            this.издателиToolStripMenuItem.Click += new System.EventHandler(this.издателиToolStripMenuItem_Click);
            // 
            // авторыToolStripMenuItem
            // 
            this.авторыToolStripMenuItem.Name = "авторыToolStripMenuItem";
            this.авторыToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.авторыToolStripMenuItem.Text = "Авторы";
            this.авторыToolStripMenuItem.Click += new System.EventHandler(this.авторыToolStripMenuItem_Click);
            // 
            // книгиToolStripMenuItem
            // 
            this.книгиToolStripMenuItem.Name = "книгиToolStripMenuItem";
            this.книгиToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.книгиToolStripMenuItem.Text = "Книги";
            this.книгиToolStripMenuItem.Click += new System.EventHandler(this.книгиToolStripMenuItem_Click);
            // 
            // читателиToolStripMenuItem
            // 
            this.читателиToolStripMenuItem.Name = "читателиToolStripMenuItem";
            this.читателиToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.читателиToolStripMenuItem.Text = "Читатели";
            this.читателиToolStripMenuItem.Click += new System.EventHandler(this.читателиToolStripMenuItem_Click);
            // 
            // экземплярToolStripMenuItem
            // 
            this.экземплярToolStripMenuItem.Name = "экземплярToolStripMenuItem";
            this.экземплярToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.экземплярToolStripMenuItem.Text = "Экземпляр";
            this.экземплярToolStripMenuItem.Click += new System.EventHandler(this.экземплярToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.утерянныеКнигиToolStripMenuItem,
            this.выданныеКнигиToolStripMenuItem});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // утерянныеКнигиToolStripMenuItem
            // 
            this.утерянныеКнигиToolStripMenuItem.Name = "утерянныеКнигиToolStripMenuItem";
            this.утерянныеКнигиToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.утерянныеКнигиToolStripMenuItem.Text = "Утерянные книги";
            this.утерянныеКнигиToolStripMenuItem.Click += new System.EventHandler(this.утерянныеКнигиToolStripMenuItem_Click);
            // 
            // выданныеКнигиToolStripMenuItem
            // 
            this.выданныеКнигиToolStripMenuItem.Name = "выданныеКнигиToolStripMenuItem";
            this.выданныеКнигиToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.выданныеКнигиToolStripMenuItem.Text = "Выданные книги";
            this.выданныеКнигиToolStripMenuItem.Click += new System.EventHandler(this.выданныеКнигиToolStripMenuItem_Click);
            // 
            // dgvReaders
            // 
            this.dgvReaders.AllowUserToAddRows = false;
            this.dgvReaders.AllowUserToDeleteRows = false;
            this.dgvReaders.AllowUserToResizeColumns = false;
            this.dgvReaders.AllowUserToResizeRows = false;
            this.dgvReaders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReaders.Location = new System.Drawing.Point(12, 102);
            this.dgvReaders.Name = "dgvReaders";
            this.dgvReaders.ReadOnly = true;
            this.dgvReaders.Size = new System.Drawing.Size(315, 219);
            this.dgvReaders.TabIndex = 1;
            // 
            // dgvBooksAndInstances
            // 
            this.dgvBooksAndInstances.AllowUserToAddRows = false;
            this.dgvBooksAndInstances.AllowUserToDeleteRows = false;
            this.dgvBooksAndInstances.AllowUserToResizeColumns = false;
            this.dgvBooksAndInstances.AllowUserToResizeRows = false;
            this.dgvBooksAndInstances.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBooksAndInstances.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooksAndInstances.Location = new System.Drawing.Point(333, 102);
            this.dgvBooksAndInstances.Name = "dgvBooksAndInstances";
            this.dgvBooksAndInstances.Size = new System.Drawing.Size(646, 219);
            this.dgvBooksAndInstances.TabIndex = 2;
            // 
            // dgvBookLoans
            // 
            this.dgvBookLoans.AllowUserToAddRows = false;
            this.dgvBookLoans.AllowUserToResizeColumns = false;
            this.dgvBookLoans.AllowUserToResizeRows = false;
            this.dgvBookLoans.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBookLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBookLoans.Location = new System.Drawing.Point(12, 352);
            this.dgvBookLoans.Name = "dgvBookLoans";
            this.dgvBookLoans.Size = new System.Drawing.Size(967, 239);
            this.dgvBookLoans.TabIndex = 3;
            this.dgvBookLoans.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvBookLoans_UserDeletingRow);
            // 
            // btnAddLoan
            // 
            this.btnAddLoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddLoan.Location = new System.Drawing.Point(708, 57);
            this.btnAddLoan.Name = "btnAddLoan";
            this.btnAddLoan.Size = new System.Drawing.Size(271, 39);
            this.btnAddLoan.TabIndex = 4;
            this.btnAddLoan.Text = "Выдать книгу";
            this.btnAddLoan.UseVisualStyleBackColor = true;
            this.btnAddLoan.Click += new System.EventHandler(this.btnAddLoan_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(408, 324);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Выданные книги";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(408, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Выдача книги";
            // 
            // txtSearchReader
            // 
            this.txtSearchReader.Location = new System.Drawing.Point(12, 76);
            this.txtSearchReader.Name = "txtSearchReader";
            this.txtSearchReader.Size = new System.Drawing.Size(315, 20);
            this.txtSearchReader.TabIndex = 11;
            this.txtSearchReader.TextChanged += new System.EventHandler(this.txtSearchReader_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Поиск читателя по ФИО:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(330, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Поиск читателя по ФИО:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(333, 76);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(369, 20);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Location = new System.Drawing.Point(680, 597);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(179, 51);
            this.btnSaveChanges.TabIndex = 15;
            this.btnSaveChanges.Text = "Сохранить изменения";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(865, 597);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 51);
            this.button2.TabIndex = 16;
            this.button2.Text = "Выход";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(495, 597);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 51);
            this.button1.TabIndex = 17;
            this.button1.Text = "Обновить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 660);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSearchReader);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddLoan);
            this.Controls.Add(this.dgvBookLoans);
            this.Controls.Add(this.dgvBooksAndInstances);
            this.Controls.Add(this.dgvReaders);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1007, 699);
            this.MinimumSize = new System.Drawing.Size(1007, 699);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Библиотека";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReaders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooksAndInstances)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookLoans)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem группыКнигToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem издателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem авторыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem книгиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem читателиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem экземплярToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvReaders;
        private System.Windows.Forms.DataGridView dgvBooksAndInstances;
        private System.Windows.Forms.DataGridView dgvBookLoans;
        private System.Windows.Forms.Button btnAddLoan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearchReader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolStripMenuItem утерянныеКнигиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выданныеКнигиToolStripMenuItem;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}


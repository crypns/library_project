namespace library_project
{
    partial class AddInstanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpAcquisitionDate = new System.Windows.Forms.DateTimePicker();
            this.txtLibraryNumber = new System.Windows.Forms.TextBox();
            this.chkIsAvailable = new System.Windows.Forms.CheckBox();
            this.chkIsLost = new System.Windows.Forms.CheckBox();
            this.btnAddInstance = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpAcquisitionDate
            // 
            this.dtpAcquisitionDate.Location = new System.Drawing.Point(12, 25);
            this.dtpAcquisitionDate.Name = "dtpAcquisitionDate";
            this.dtpAcquisitionDate.Size = new System.Drawing.Size(153, 20);
            this.dtpAcquisitionDate.TabIndex = 0;
            // 
            // txtLibraryNumber
            // 
            this.txtLibraryNumber.Location = new System.Drawing.Point(12, 64);
            this.txtLibraryNumber.Name = "txtLibraryNumber";
            this.txtLibraryNumber.Size = new System.Drawing.Size(100, 20);
            this.txtLibraryNumber.TabIndex = 1;
            // 
            // chkIsAvailable
            // 
            this.chkIsAvailable.AutoSize = true;
            this.chkIsAvailable.Location = new System.Drawing.Point(12, 90);
            this.chkIsAvailable.Name = "chkIsAvailable";
            this.chkIsAvailable.Size = new System.Drawing.Size(75, 17);
            this.chkIsAvailable.TabIndex = 2;
            this.chkIsAvailable.Text = "Доступна";
            this.chkIsAvailable.UseVisualStyleBackColor = true;
            // 
            // chkIsLost
            // 
            this.chkIsLost.AutoSize = true;
            this.chkIsLost.Location = new System.Drawing.Point(12, 113);
            this.chkIsLost.Name = "chkIsLost";
            this.chkIsLost.Size = new System.Drawing.Size(69, 17);
            this.chkIsLost.TabIndex = 3;
            this.chkIsLost.Text = "Утеряна";
            this.chkIsLost.UseVisualStyleBackColor = true;
            // 
            // btnAddInstance
            // 
            this.btnAddInstance.Location = new System.Drawing.Point(12, 136);
            this.btnAddInstance.Name = "btnAddInstance";
            this.btnAddInstance.Size = new System.Drawing.Size(153, 23);
            this.btnAddInstance.TabIndex = 4;
            this.btnAddInstance.Text = "Добавить экземпляр";
            this.btnAddInstance.UseVisualStyleBackColor = true;
            this.btnAddInstance.Click += new System.EventHandler(this.btnAddInstance_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Дата получения:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Библ. номер:";
            // 
            // AddInstanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 169);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddInstance);
            this.Controls.Add(this.chkIsLost);
            this.Controls.Add(this.chkIsAvailable);
            this.Controls.Add(this.txtLibraryNumber);
            this.Controls.Add(this.dtpAcquisitionDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(194, 208);
            this.MinimumSize = new System.Drawing.Size(194, 208);
            this.Name = "AddInstanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить экземпляр";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpAcquisitionDate;
        private System.Windows.Forms.TextBox txtLibraryNumber;
        private System.Windows.Forms.CheckBox chkIsAvailable;
        private System.Windows.Forms.CheckBox chkIsLost;
        private System.Windows.Forms.Button btnAddInstance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
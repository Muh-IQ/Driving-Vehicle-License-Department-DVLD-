namespace MyDVLD_Win_Form
{
    partial class frmListLocalDrivingLicensesAppliaction
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListLocalDrivingLicensesAppliaction));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvAllLocalApplications = new System.Windows.Forms.DataGridView();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.smShowApplicationDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.smEditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.smDeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.smCancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.smScheduleTests = new System.Windows.Forms.ToolStripMenuItem();
            this.smScheduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.smScheduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.smScheduleStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.smIssueDrivingLicenseFirstTime = new System.Windows.Forms.ToolStripMenuItem();
            this.smShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.smShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecord = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLocalLicense = new System.Windows.Forms.Button();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalApplications)).BeginInit();
            this.cmsApplications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvAllLocalApplications);
            this.panel1.Location = new System.Drawing.Point(3, 324);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1917, 336);
            this.panel1.TabIndex = 41;
            // 
            // dgvAllLocalApplications
            // 
            this.dgvAllLocalApplications.AllowUserToAddRows = false;
            this.dgvAllLocalApplications.AllowUserToDeleteRows = false;
            this.dgvAllLocalApplications.AllowUserToOrderColumns = true;
            this.dgvAllLocalApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllLocalApplications.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAllLocalApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllLocalApplications.ContextMenuStrip = this.cmsApplications;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAllLocalApplications.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAllLocalApplications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllLocalApplications.Location = new System.Drawing.Point(0, 0);
            this.dgvAllLocalApplications.Margin = new System.Windows.Forms.Padding(4);
            this.dgvAllLocalApplications.Name = "dgvAllLocalApplications";
            this.dgvAllLocalApplications.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllLocalApplications.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAllLocalApplications.RowHeadersWidth = 51;
            this.dgvAllLocalApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllLocalApplications.Size = new System.Drawing.Size(1917, 336);
            this.dgvAllLocalApplications.TabIndex = 14;
            // 
            // cmsApplications
            // 
            this.cmsApplications.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smShowApplicationDetails,
            this.toolStripSeparator1,
            this.smEditApplication,
            this.smDeleteApplication,
            this.toolStripSeparator2,
            this.smCancelApplication,
            this.toolStripMenuItem9,
            this.smScheduleTests,
            this.toolStripMenuItem8,
            this.smIssueDrivingLicenseFirstTime,
            this.smShowLicense,
            this.smShowPersonLicenseHistory});
            this.cmsApplications.Name = "contextMenuStrip1";
            this.cmsApplications.Size = new System.Drawing.Size(289, 248);
            this.cmsApplications.Opening += new System.ComponentModel.CancelEventHandler(this.cmsApplications_Opening);
            // 
            // smShowApplicationDetails
            // 
            this.smShowApplicationDetails.Name = "smShowApplicationDetails";
            this.smShowApplicationDetails.Size = new System.Drawing.Size(288, 24);
            this.smShowApplicationDetails.Text = "Show Application Details";
            this.smShowApplicationDetails.Click += new System.EventHandler(this.smShowApplicationDetails_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(285, 6);
            // 
            // smEditApplication
            // 
            this.smEditApplication.Name = "smEditApplication";
            this.smEditApplication.Size = new System.Drawing.Size(288, 24);
            this.smEditApplication.Text = "Edit Application";
            this.smEditApplication.Click += new System.EventHandler(this.smEditApplication_Click);
            // 
            // smDeleteApplication
            // 
            this.smDeleteApplication.Name = "smDeleteApplication";
            this.smDeleteApplication.Size = new System.Drawing.Size(288, 24);
            this.smDeleteApplication.Text = "Delete Application";
            this.smDeleteApplication.Click += new System.EventHandler(this.smDeleteApplication_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(285, 6);
            // 
            // smCancelApplication
            // 
            this.smCancelApplication.Name = "smCancelApplication";
            this.smCancelApplication.Size = new System.Drawing.Size(288, 24);
            this.smCancelApplication.Text = "Cancel Application";
            this.smCancelApplication.Click += new System.EventHandler(this.smCancelApplication_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(285, 6);
            // 
            // smScheduleTests
            // 
            this.smScheduleTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smScheduleVisionTest,
            this.smScheduleWrittenTest,
            this.smScheduleStreetTest});
            this.smScheduleTests.Name = "smScheduleTests";
            this.smScheduleTests.Size = new System.Drawing.Size(288, 24);
            this.smScheduleTests.Text = "schedule Tests";
            // 
            // smScheduleVisionTest
            // 
            this.smScheduleVisionTest.Image = global::MyDVLD_Win_Form.Properties.Resources.Vision_Test_32;
            this.smScheduleVisionTest.Name = "smScheduleVisionTest";
            this.smScheduleVisionTest.Size = new System.Drawing.Size(233, 26);
            this.smScheduleVisionTest.Text = "Schedule Vision Test ";
            this.smScheduleVisionTest.Click += new System.EventHandler(this.smScheduleVisionTest_Click);
            // 
            // smScheduleWrittenTest
            // 
            this.smScheduleWrittenTest.Image = global::MyDVLD_Win_Form.Properties.Resources.Written_Test_32;
            this.smScheduleWrittenTest.Name = "smScheduleWrittenTest";
            this.smScheduleWrittenTest.Size = new System.Drawing.Size(233, 26);
            this.smScheduleWrittenTest.Text = "schedule Written Test";
            this.smScheduleWrittenTest.Click += new System.EventHandler(this.smScheduleWrittenTest_Click);
            // 
            // smScheduleStreetTest
            // 
            this.smScheduleStreetTest.Image = global::MyDVLD_Win_Form.Properties.Resources.Street_Test_32;
            this.smScheduleStreetTest.Name = "smScheduleStreetTest";
            this.smScheduleStreetTest.Size = new System.Drawing.Size(233, 26);
            this.smScheduleStreetTest.Text = "Schedule Street Test ";
            this.smScheduleStreetTest.Click += new System.EventHandler(this.smScheduleStreetTest_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(285, 6);
            // 
            // smIssueDrivingLicenseFirstTime
            // 
            this.smIssueDrivingLicenseFirstTime.Name = "smIssueDrivingLicenseFirstTime";
            this.smIssueDrivingLicenseFirstTime.Size = new System.Drawing.Size(288, 24);
            this.smIssueDrivingLicenseFirstTime.Text = "Issue Driving License(First Time)";
            this.smIssueDrivingLicenseFirstTime.Click += new System.EventHandler(this.smIssueDrivingLicenseFirstTime_Click);
            // 
            // smShowLicense
            // 
            this.smShowLicense.Name = "smShowLicense";
            this.smShowLicense.Size = new System.Drawing.Size(288, 24);
            this.smShowLicense.Text = "Show License";
            this.smShowLicense.Click += new System.EventHandler(this.smShowLicense_Click);
            // 
            // smShowPersonLicenseHistory
            // 
            this.smShowPersonLicenseHistory.Name = "smShowPersonLicenseHistory";
            this.smShowPersonLicenseHistory.Size = new System.Drawing.Size(288, 24);
            this.smShowPersonLicenseHistory.Text = "Show  Person License History";
            this.smShowPersonLicenseHistory.Click += new System.EventHandler(this.smShowPersonLicenseHistory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(804, 241);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(497, 41);
            this.label1.TabIndex = 39;
            this.label1.Text = "Local Driving Licenses Appliaction";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 282);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 31);
            this.label2.TabIndex = 35;
            this.label2.Text = "Filter By ";
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord.Location = new System.Drawing.Point(168, 674);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(80, 32);
            this.lblRecord.TabIndex = 44;
            this.lblRecord.Text = "{???}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 674);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 32);
            this.label3.TabIndex = 43;
            this.label3.Text = "# Record : ";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Silver;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::MyDVLD_Win_Form.Properties.Resources.close_5_;
            this.button3.Location = new System.Drawing.Point(1766, 670);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(154, 44);
            this.button3.TabIndex = 45;
            this.button3.Text = "       Close ";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(833, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(444, 226);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // btnLocalLicense
            // 
            this.btnLocalLicense.BackColor = System.Drawing.Color.White;
            this.btnLocalLicense.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnLocalLicense.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnLocalLicense.Image = global::MyDVLD_Win_Form.Properties.Resources.New_Application_64;
            this.btnLocalLicense.Location = new System.Drawing.Point(1839, 239);
            this.btnLocalLicense.Margin = new System.Windows.Forms.Padding(4);
            this.btnLocalLicense.Name = "btnLocalLicense";
            this.btnLocalLicense.Size = new System.Drawing.Size(81, 77);
            this.btnLocalLicense.TabIndex = 38;
            this.btnLocalLicense.UseVisualStyleBackColor = false;
            this.btnLocalLicense.Click += new System.EventHandler(this.btnLocalLicense_Click);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No.",
            "Full Name",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(119, 285);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(210, 33);
            this.cbFilterBy.TabIndex = 129;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(336, 286);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(256, 30);
            this.txtFilterValue.TabIndex = 128;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            // 
            // frmListLocalDrivingLicensesAppliaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1924, 724);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnLocalLicense);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmListLocalDrivingLicensesAppliaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Local Driving Licenses Appliaction";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicensesAppliaction_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalApplications)).EndInit();
            this.cmsApplications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvAllLocalApplications;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLocalLicense;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem smShowApplicationDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem smEditApplication;
        private System.Windows.Forms.ToolStripMenuItem smDeleteApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem smCancelApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem smScheduleTests;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem smIssueDrivingLicenseFirstTime;
        private System.Windows.Forms.ToolStripMenuItem smShowLicense;
        private System.Windows.Forms.ToolStripMenuItem smShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem smScheduleVisionTest;
        private System.Windows.Forms.ToolStripMenuItem smScheduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem smScheduleStreetTest;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
    }
}
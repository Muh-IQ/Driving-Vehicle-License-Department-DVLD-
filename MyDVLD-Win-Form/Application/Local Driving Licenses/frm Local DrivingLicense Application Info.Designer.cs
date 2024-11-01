namespace MyDVLD_Win_Form
{
    partial class frm_Local_DrivingLicense_Application_Info
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrl_LocalDrivingLicenseApplicationInfo1 = new MyDVLD_Win_Form.ctrl_LocalDrivingLicenseApplicationInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Silver;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::MyDVLD_Win_Form.Properties.Resources.close_5_;
            this.btnClose.Location = new System.Drawing.Point(866, 405);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(171, 46);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "       Close ";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrl_LocalDrivingLicenseApplicationInfo1
            // 
            this.ctrl_LocalDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(1, -3);
            this.ctrl_LocalDrivingLicenseApplicationInfo1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ctrl_LocalDrivingLicenseApplicationInfo1.Name = "ctrl_LocalDrivingLicenseApplicationInfo1";
            this.ctrl_LocalDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(1040, 404);
            this.ctrl_LocalDrivingLicenseApplicationInfo1.TabIndex = 43;
            // 
            // frm_Local_DrivingLicense_Application_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1045, 462);
            this.Controls.Add(this.ctrl_LocalDrivingLicenseApplicationInfo1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frm_Local_DrivingLicense_Application_Info";
            this.Text = "Local Driving LicenseApplicationInfo";
            this.Load += new System.EventHandler(this.frm_Local_DrivingLicense_Application_Info_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnClose;
        private ctrl_LocalDrivingLicenseApplicationInfo ctrl_LocalDrivingLicenseApplicationInfo1;
    }
}
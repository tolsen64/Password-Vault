namespace BoycoT_Password_Vault
{
    partial class FormCredential
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCredName = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRecoveryKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numPwdLength = new System.Windows.Forms.NumericUpDown();
            this.btnCreatePassword = new System.Windows.Forms.Button();
            this.chkSymbols = new System.Windows.Forms.CheckBox();
            this.chkNumbers = new System.Windows.Forms.CheckBox();
            this.chkLowerCase = new System.Windows.Forms.CheckBox();
            this.chkUpperCase = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPwdLength)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Credential Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "User/Email:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // txtCredName
            // 
            this.txtCredName.Location = new System.Drawing.Point(110, 6);
            this.txtCredName.Name = "txtCredName";
            this.txtCredName.Size = new System.Drawing.Size(351, 20);
            this.txtCredName.TabIndex = 1;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(110, 32);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(351, 20);
            this.txtUserID.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(110, 58);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(351, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(305, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(386, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(110, 113);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(351, 20);
            this.txtLink.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Link:";
            // 
            // txtRecoveryKey
            // 
            this.txtRecoveryKey.Location = new System.Drawing.Point(110, 84);
            this.txtRecoveryKey.Name = "txtRecoveryKey";
            this.txtRecoveryKey.Size = new System.Drawing.Size(351, 20);
            this.txtRecoveryKey.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Recovery Key:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numPwdLength);
            this.groupBox1.Controls.Add(this.btnCreatePassword);
            this.groupBox1.Controls.Add(this.chkSymbols);
            this.groupBox1.Controls.Add(this.chkNumbers);
            this.groupBox1.Controls.Add(this.chkLowerCase);
            this.groupBox1.Controls.Add(this.chkUpperCase);
            this.groupBox1.Location = new System.Drawing.Point(12, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 59);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Password";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(162, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "PwdLen";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numPwdLength
            // 
            this.numPwdLength.Location = new System.Drawing.Point(162, 33);
            this.numPwdLength.Name = "numPwdLength";
            this.numPwdLength.Size = new System.Drawing.Size(61, 20);
            this.numPwdLength.TabIndex = 5;
            // 
            // btnCreatePassword
            // 
            this.btnCreatePassword.Location = new System.Drawing.Point(232, 26);
            this.btnCreatePassword.Name = "btnCreatePassword";
            this.btnCreatePassword.Size = new System.Drawing.Size(49, 23);
            this.btnCreatePassword.TabIndex = 4;
            this.btnCreatePassword.Text = "Create";
            this.btnCreatePassword.UseVisualStyleBackColor = true;
            this.btnCreatePassword.Click += new System.EventHandler(this.btnCreatePassword_Click);
            // 
            // chkSymbols
            // 
            this.chkSymbols.AutoSize = true;
            this.chkSymbols.Location = new System.Drawing.Point(91, 36);
            this.chkSymbols.Name = "chkSymbols";
            this.chkSymbols.Size = new System.Drawing.Size(65, 17);
            this.chkSymbols.TabIndex = 3;
            this.chkSymbols.Text = "Symbols";
            this.chkSymbols.UseVisualStyleBackColor = true;
            // 
            // chkNumbers
            // 
            this.chkNumbers.AutoSize = true;
            this.chkNumbers.Location = new System.Drawing.Point(91, 19);
            this.chkNumbers.Name = "chkNumbers";
            this.chkNumbers.Size = new System.Drawing.Size(68, 17);
            this.chkNumbers.TabIndex = 2;
            this.chkNumbers.Text = "Numbers";
            this.chkNumbers.UseVisualStyleBackColor = true;
            // 
            // chkLowerCase
            // 
            this.chkLowerCase.AutoSize = true;
            this.chkLowerCase.Location = new System.Drawing.Point(6, 36);
            this.chkLowerCase.Name = "chkLowerCase";
            this.chkLowerCase.Size = new System.Drawing.Size(79, 17);
            this.chkLowerCase.TabIndex = 1;
            this.chkLowerCase.Text = "LowerCase";
            this.chkLowerCase.UseVisualStyleBackColor = true;
            // 
            // chkUpperCase
            // 
            this.chkUpperCase.AutoSize = true;
            this.chkUpperCase.Location = new System.Drawing.Point(6, 19);
            this.chkUpperCase.Name = "chkUpperCase";
            this.chkUpperCase.Size = new System.Drawing.Size(79, 17);
            this.chkUpperCase.TabIndex = 0;
            this.chkUpperCase.Text = "UpperCase";
            this.chkUpperCase.UseVisualStyleBackColor = true;
            // 
            // FormCredential
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(473, 197);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRecoveryKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this.txtCredName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCredential";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormCredential";
            this.Load += new System.EventHandler(this.FormCredential_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPwdLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCredName;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRecoveryKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCreatePassword;
        private System.Windows.Forms.CheckBox chkSymbols;
        private System.Windows.Forms.CheckBox chkNumbers;
        private System.Windows.Forms.CheckBox chkLowerCase;
        private System.Windows.Forms.CheckBox chkUpperCase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numPwdLength;
    }
}
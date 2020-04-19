namespace BoycoT_Password_Vault
{
    partial class FormSettings
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoXML = new System.Windows.Forms.RadioButton();
            this.chkShowPasswordDB = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoUseCredientials = new System.Windows.Forms.RadioButton();
            this.rdoIntegratedSecurity = new System.Windows.Forms.RadioButton();
            this.txtPasswordDB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUserIDDB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerFile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rdoSQLite = new System.Windows.Forms.RadioButton();
            this.rdoMySQL = new System.Windows.Forms.RadioButton();
            this.rdoMSSQL = new System.Windows.Forms.RadioButton();
            this.rdoJson = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.numAppTimeout = new System.Windows.Forms.NumericUpDown();
            this.btnTestDbConnection = new System.Windows.Forms.Button();
            this.chkMergeData = new System.Windows.Forms.CheckBox();
            this.chkUsePIN = new System.Windows.Forms.CheckBox();
            this.lblPIN = new System.Windows.Forms.Label();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAppTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(632, 156);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(713, 156);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoXML);
            this.groupBox1.Controls.Add(this.chkShowPasswordDB);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numPort);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.txtPasswordDB);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtUserIDDB);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtServerFile);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rdoSQLite);
            this.groupBox1.Controls.Add(this.rdoMySQL);
            this.groupBox1.Controls.Add(this.rdoMSSQL);
            this.groupBox1.Controls.Add(this.rdoJson);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 108);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // rdoXML
            // 
            this.rdoXML.AutoSize = true;
            this.rdoXML.Location = new System.Drawing.Point(277, 19);
            this.rdoXML.Name = "rdoXML";
            this.rdoXML.Size = new System.Drawing.Size(63, 17);
            this.rdoXML.TabIndex = 13;
            this.rdoXML.TabStop = true;
            this.rdoXML.Text = "XML file";
            this.rdoXML.UseVisualStyleBackColor = true;
            // 
            // chkShowPasswordDB
            // 
            this.chkShowPasswordDB.AutoSize = true;
            this.chkShowPasswordDB.Location = new System.Drawing.Point(717, 48);
            this.chkShowPasswordDB.Name = "chkShowPasswordDB";
            this.chkShowPasswordDB.Size = new System.Drawing.Size(53, 17);
            this.chkShowPasswordDB.TabIndex = 23;
            this.chkShowPasswordDB.Text = "Show";
            this.chkShowPasswordDB.UseVisualStyleBackColor = true;
            this.chkShowPasswordDB.CheckedChanged += new System.EventHandler(this.chkShowCreds_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(346, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Port:";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(383, 19);
            this.numPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(56, 20);
            this.numPort.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoUseCredientials);
            this.panel1.Controls.Add(this.rdoIntegratedSecurity);
            this.panel1.Location = new System.Drawing.Point(445, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 27);
            this.panel1.TabIndex = 22;
            // 
            // rdoUseCredientials
            // 
            this.rdoUseCredientials.AutoSize = true;
            this.rdoUseCredientials.Checked = true;
            this.rdoUseCredientials.Location = new System.Drawing.Point(3, 4);
            this.rdoUseCredientials.Name = "rdoUseCredientials";
            this.rdoUseCredientials.Size = new System.Drawing.Size(99, 17);
            this.rdoUseCredientials.TabIndex = 16;
            this.rdoUseCredientials.TabStop = true;
            this.rdoUseCredientials.Text = "Use Credentials";
            this.rdoUseCredientials.UseVisualStyleBackColor = true;
            // 
            // rdoIntegratedSecurity
            // 
            this.rdoIntegratedSecurity.AutoSize = true;
            this.rdoIntegratedSecurity.Location = new System.Drawing.Point(155, 4);
            this.rdoIntegratedSecurity.Name = "rdoIntegratedSecurity";
            this.rdoIntegratedSecurity.Size = new System.Drawing.Size(114, 17);
            this.rdoIntegratedSecurity.TabIndex = 17;
            this.rdoIntegratedSecurity.Text = "Integrated Security";
            this.rdoIntegratedSecurity.UseVisualStyleBackColor = true;
            // 
            // txtPasswordDB
            // 
            this.txtPasswordDB.Location = new System.Drawing.Point(597, 45);
            this.txtPasswordDB.Name = "txtPasswordDB";
            this.txtPasswordDB.Size = new System.Drawing.Size(114, 20);
            this.txtPasswordDB.TabIndex = 21;
            this.txtPasswordDB.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(560, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Pwd:";
            // 
            // txtUserIDDB
            // 
            this.txtUserIDDB.Location = new System.Drawing.Point(445, 46);
            this.txtUserIDDB.Name = "txtUserIDDB";
            this.txtUserIDDB.Size = new System.Drawing.Size(102, 20);
            this.txtUserIDDB.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(413, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Uid:";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(277, 46);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(130, 20);
            this.txtDatabase.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Database:";
            // 
            // txtServerFile
            // 
            this.txtServerFile.Location = new System.Drawing.Point(76, 46);
            this.txtServerFile.Name = "txtServerFile";
            this.txtServerFile.Size = new System.Drawing.Size(133, 20);
            this.txtServerFile.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Server/File:";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(6, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(764, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "When choosing MSSQL or MySQL, the database must be created and the connection str" +
    "ing must contain credentials that have rights to create and interact with the ta" +
    "bles it creates.";
            // 
            // rdoSQLite
            // 
            this.rdoSQLite.AutoSize = true;
            this.rdoSQLite.Location = new System.Drawing.Point(215, 19);
            this.rdoSQLite.Name = "rdoSQLite";
            this.rdoSQLite.Size = new System.Drawing.Size(57, 17);
            this.rdoSQLite.TabIndex = 5;
            this.rdoSQLite.Text = "SQLite";
            this.rdoSQLite.UseVisualStyleBackColor = true;
            this.rdoSQLite.CheckedChanged += new System.EventHandler(this.rdoDbType_CheckedChanged);
            // 
            // rdoMySQL
            // 
            this.rdoMySQL.AutoSize = true;
            this.rdoMySQL.Location = new System.Drawing.Point(149, 19);
            this.rdoMySQL.Name = "rdoMySQL";
            this.rdoMySQL.Size = new System.Drawing.Size(60, 17);
            this.rdoMySQL.TabIndex = 2;
            this.rdoMySQL.Text = "MySQL";
            this.rdoMySQL.UseVisualStyleBackColor = true;
            this.rdoMySQL.CheckedChanged += new System.EventHandler(this.rdoDbType_CheckedChanged);
            // 
            // rdoMSSQL
            // 
            this.rdoMSSQL.AutoSize = true;
            this.rdoMSSQL.Location = new System.Drawing.Point(81, 19);
            this.rdoMSSQL.Name = "rdoMSSQL";
            this.rdoMSSQL.Size = new System.Drawing.Size(62, 17);
            this.rdoMSSQL.TabIndex = 1;
            this.rdoMSSQL.Text = "MSSQL";
            this.rdoMSSQL.UseVisualStyleBackColor = true;
            this.rdoMSSQL.CheckedChanged += new System.EventHandler(this.rdoDbType_CheckedChanged);
            // 
            // rdoJson
            // 
            this.rdoJson.AutoSize = true;
            this.rdoJson.Location = new System.Drawing.Point(6, 19);
            this.rdoJson.Name = "rdoJson";
            this.rdoJson.Size = new System.Drawing.Size(69, 17);
            this.rdoJson.TabIndex = 0;
            this.rdoJson.Text = "JSON file";
            this.rdoJson.UseVisualStyleBackColor = true;
            this.rdoJson.CheckedChanged += new System.EventHandler(this.rdoDbType_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(172, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Auto-Minimize Timeout (in minutes):";
            // 
            // numAppTimeout
            // 
            this.numAppTimeout.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.numAppTimeout.Location = new System.Drawing.Point(190, 159);
            this.numAppTimeout.Name = "numAppTimeout";
            this.numAppTimeout.Size = new System.Drawing.Size(67, 20);
            this.numAppTimeout.TabIndex = 10;
            // 
            // btnTestDbConnection
            // 
            this.btnTestDbConnection.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnTestDbConnection.Location = new System.Drawing.Point(303, 156);
            this.btnTestDbConnection.Name = "btnTestDbConnection";
            this.btnTestDbConnection.Size = new System.Drawing.Size(167, 23);
            this.btnTestDbConnection.TabIndex = 11;
            this.btnTestDbConnection.Text = "Test Database Connection";
            this.btnTestDbConnection.UseVisualStyleBackColor = true;
            this.btnTestDbConnection.Click += new System.EventHandler(this.btnTestDbConnection_Click);
            // 
            // chkMergeData
            // 
            this.chkMergeData.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.chkMergeData.AutoSize = true;
            this.chkMergeData.Location = new System.Drawing.Point(490, 160);
            this.chkMergeData.Name = "chkMergeData";
            this.chkMergeData.Size = new System.Drawing.Size(113, 17);
            this.chkMergeData.TabIndex = 12;
            this.chkMergeData.Text = "Merge data on OK";
            this.chkMergeData.UseVisualStyleBackColor = true;
            // 
            // chkUsePIN
            // 
            this.chkUsePIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkUsePIN.AutoSize = true;
            this.chkUsePIN.Location = new System.Drawing.Point(303, 133);
            this.chkUsePIN.Name = "chkUsePIN";
            this.chkUsePIN.Size = new System.Drawing.Size(124, 17);
            this.chkUsePIN.TabIndex = 13;
            this.chkUsePIN.Text = "Use a PIN after login";
            this.chkUsePIN.UseVisualStyleBackColor = true;
            this.chkUsePIN.CheckedChanged += new System.EventHandler(this.chkUsePIN_CheckedChanged);
            // 
            // lblPIN
            // 
            this.lblPIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPIN.AutoSize = true;
            this.lblPIN.Location = new System.Drawing.Point(456, 134);
            this.lblPIN.Name = "lblPIN";
            this.lblPIN.Size = new System.Drawing.Size(28, 13);
            this.lblPIN.TabIndex = 14;
            this.lblPIN.Text = "PIN:";
            this.lblPIN.Visible = false;
            // 
            // txtPIN
            // 
            this.txtPIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPIN.Location = new System.Drawing.Point(490, 131);
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Size = new System.Drawing.Size(100, 20);
            this.txtPIN.TabIndex = 15;
            this.txtPIN.UseSystemPasswordChar = true;
            this.txtPIN.Visible = false;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 191);
            this.Controls.Add(this.txtPIN);
            this.Controls.Add(this.lblPIN);
            this.Controls.Add(this.chkUsePIN);
            this.Controls.Add(this.chkMergeData);
            this.Controls.Add(this.btnTestDbConnection);
            this.Controls.Add(this.numAppTimeout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAppTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoMySQL;
        private System.Windows.Forms.RadioButton rdoMSSQL;
        private System.Windows.Forms.RadioButton rdoJson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdoSQLite;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numAppTimeout;
        private System.Windows.Forms.Button btnTestDbConnection;
        private System.Windows.Forms.TextBox txtPasswordDB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUserIDDB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rdoIntegratedSecurity;
        private System.Windows.Forms.RadioButton rdoUseCredientials;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtServerFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.CheckBox chkShowPasswordDB;
        private System.Windows.Forms.CheckBox chkMergeData;
        private System.Windows.Forms.RadioButton rdoXML;
        private System.Windows.Forms.CheckBox chkUsePIN;
        private System.Windows.Forms.Label lblPIN;
        private System.Windows.Forms.TextBox txtPIN;
    }
}
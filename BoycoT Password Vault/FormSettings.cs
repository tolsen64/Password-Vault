using System;
using System.IO;
using System.Security;
using System.Windows.Forms;
using static BoycoT_Password_Vault.Settings;

namespace BoycoT_Password_Vault
{
    public partial class FormSettings : Form
    {
        public bool MergeSettings { get { return chkMergeData.Checked; } }

        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            DbType = settings.dbType;
            numPort.Value = settings.dbPort;
            AuthType = settings.dbAuthType;
            txtServerFile.Text = settings.dbServer.Length > 0 ? settings.dbServer.DecryptBase64StringToText().ToUnsecureString() : "";
            txtDatabase.Text = settings.dbDatabase.Length > 0 ? settings.dbDatabase.DecryptBase64StringToText().ToUnsecureString() : "";
            txtUserIDDB.Text = settings.dbUserID.Length > 0 ? settings.dbUserID.DecryptBase64StringToText().ToUnsecureString() : "";
            txtPasswordDB.Text = settings.dbPassword.Length > 0 ? settings.dbPassword.DecryptBase64StringToText().ToUnsecureString() : "";
            txtPIN.Text = settings.userPIN.Length > 0 ? settings.userPIN.DecryptBase64StringToText().ToUnsecureString() : "";
            numAppTimeout.Value = settings.appTimeout;
            chkUsePIN.Checked = settings.userUsePIN;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            settings.dbType = DbType;
            settings.dbPort = (int)numPort.Value;
            settings.dbAuthType = AuthType;
            settings.dbServer = txtServerFile.Text.Length > 0 ? txtServerFile.Text.ToSecureString().EncryptTextToBase64String() : "";
            settings.dbDatabase = txtDatabase.Text.Length > 0 ? txtDatabase.Text.ToSecureString().EncryptTextToBase64String() : "";
            settings.dbUserID = txtUserIDDB.Text.Length > 0 ? txtUserIDDB.Text.ToSecureString().EncryptTextToBase64String() : "";
            settings.dbPassword = txtPasswordDB.Text.Length > 0 ? txtPasswordDB.Text.ToSecureString().EncryptTextToBase64String() : "";
            settings.userPIN = txtPIN.Text.Length > 0 ? txtPIN.Text.ToSecureString().EncryptTextToBase64String() : "";
            settings.appTimeout = numAppTimeout.Value;
            settings.dbConnStr = BuildConnectionString().EncryptTextToBase64String();
            settings.userUsePIN = chkUsePIN.Checked;
            SaveSettings();
        }

        private void btnTestDbConnection_Click(object sender, EventArgs e)
        {
            IDatabase tmpDb = null;
            if (rdoJson.Checked) tmpDb = new JsonDatabase();
            if (rdoMSSQL.Checked) tmpDb = new SqlServerDatabase();
            if (rdoMySQL.Checked) tmpDb = new MySqlDatabase();
            if (rdoSQLite.Checked) tmpDb = new SQLiteDatabase();
            if (rdoXML.Checked) tmpDb = new XMLDatabase();
            if (rdoLiteDB.Checked) tmpDb = new LiteDbDatabase();

            tmpDb.ConnectionString = BuildConnectionString().EncryptTextToBase64String();
            if (tmpDb.TestConnection())
            {
                MessageBox.Show("Success!");
            }
        }

        private SecureString BuildConnectionString()
        {
            if (rdoJson.Checked || rdoXML.Checked || rdoLiteDB.Checked)
                return txtServerFile.Text.ToSecureString();

            else if (rdoSQLite.Checked)
                return $"Data Source={txtServerFile.Text}; Version=3; {(txtPasswordDB.Text != "" ? $"Password={txtPasswordDB.Text};" : "")}".ToSecureString();

            else if (rdoMySQL.Checked)
                return $"Server={txtServerFile.Text}; Database={txtDatabase.Text}; Uid={txtUserIDDB.Text}; Pwd={txtPasswordDB.Text}; {(numPort.Value > 0 ? $"Port={numPort.Value};" : "")}".ToSecureString();

            else if (rdoMSSQL.Checked)

                if (rdoUseCredientials.Checked)
                    return $"Server={txtServerFile.Text}{(numPort.Value > 0 ? $@",{numPort.Value}" : "")}; Database={txtDatabase.Text}; User Id={txtUserIDDB.Text}; Password={txtPasswordDB.Text};".ToSecureString();

                else if (rdoIntegratedSecurity.Checked)
                    return $"Server={txtServerFile.Text}{(numPort.Value > 0 ? $@",{numPort.Value}" : "")}; Database={txtDatabase.Text}; Trusted_Connection=True;".ToSecureString();

            return null;
        }

        private void rdoDbType_CheckedChanged(object sender, EventArgs e)
        {
            numPort.Enabled = rdoMSSQL.Checked || rdoMySQL.Checked;
            numPort.Value = 0;
            rdoUseCredientials.Visible = rdoMSSQL.Checked;
            rdoIntegratedSecurity.Visible = rdoMSSQL.Checked;
            txtServerFile.Enabled = true;
            txtServerFile.Clear();
            txtDatabase.Enabled = rdoMSSQL.Checked || rdoMySQL.Checked;
            txtDatabase.Clear();
            txtUserIDDB.Enabled = rdoMSSQL.Checked || rdoMySQL.Checked;
            txtUserIDDB.Clear();
            txtPasswordDB.Enabled = rdoMSSQL.Checked || rdoMySQL.Checked || rdoSQLite.Checked;
            txtPasswordDB.Clear();

            if (rdoJson.Checked)
                txtServerFile.Text = $"database.json";
            else if (rdoMSSQL.Checked)
                txtServerFile.Text = "(localdb)\\MSSQLLocalDB";
            else if (rdoSQLite.Checked)
                txtServerFile.Text = "database.sqlite";
            else if (rdoXML.Checked)
                txtServerFile.Text = $"database.xml";
            else if (rdoLiteDB.Checked)
                txtServerFile.Text = $"database.litedb";
        }

        private void chkShowCreds_CheckedChanged(object sender, EventArgs e)
        {
            txtPasswordDB.UseSystemPasswordChar = !chkShowPasswordDB.Checked;
        }

        private int DbType
        {
            set
            {
                rdoJson.Checked = value == 0;
                rdoMSSQL.Checked = value == 1;
                rdoMySQL.Checked = value == 2;
                rdoSQLite.Checked = value == 3;
                rdoXML.Checked = value == 4;
                rdoLiteDB.Checked = value == 5;
            }
            get
            {
                if (rdoJson.Checked) return 0;
                if (rdoMSSQL.Checked) return 1;
                if (rdoMySQL.Checked) return 2;
                if (rdoSQLite.Checked) return 3;
                if (rdoXML.Checked) return 4;
                if (rdoLiteDB.Checked) return 5;
                return -1;
            }
        }

        private int AuthType
        {
            set
            {
                rdoUseCredientials.Checked = value == 0;
                rdoIntegratedSecurity.Checked = value == 1;
            }
            get
            {
                if (rdoUseCredientials.Checked) return 0;
                if (rdoIntegratedSecurity.Checked) return 1;
                return -1;
            }
        }

        private void chkUsePIN_CheckedChanged(object sender, EventArgs e)
        {
            lblPIN.Visible = chkUsePIN.Checked;
            txtPIN.Visible = chkUsePIN.Checked;
            if (!chkUsePIN.Checked) txtPIN.Clear();
        }
    }
}

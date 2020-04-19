using System;
using System.Net;
using System.Security;
using System.Windows.Forms;
using static RijndaelAES;

namespace BoycoT_Password_Vault
{
    public partial class LoginForm : Form
    {
        internal SecureString id { get; set; }
        internal SecureString pwd { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            txtID.Select();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            id = txtID.Text.ToSecureString();
            Shared.DbUserTableName = id.EncryptTextToBase64String();
            pwd = txtPwd.Text.ToSecureString();
            txtID.Clear();
            txtPwd.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RijndaelAES;
using static BoycoT_Password_Vault.Settings;

namespace BoycoT_Password_Vault
{
    public partial class FormCredential : Form
    {
        internal int CredentialID { get; set; }
        internal SecureString CredentialName { get; set; }
        internal SecureString UserID { get; set; }
        internal SecureString Password { get; set; }
        internal SecureString Link { get; set; }

        public FormCredential()
        {
            InitializeComponent();
        }

        private void FormCredential_Load(object sender, EventArgs e)
        {
            txtCredName.Text = CredentialName.ToUnsecureString();
            txtUserID.Text = UserID.ToUnsecureString();
            txtPassword.Text = Password.ToUnsecureString();
            txtLink.Text = Link.ToUnsecureString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CredentialName = txtCredName.Text.ToSecureString();
            UserID = txtUserID.Text.ToSecureString();
            Password = txtPassword.Text.ToSecureString();
            Link = txtLink.Text.ToSecureString();
        }
    }
}

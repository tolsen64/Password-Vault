using System.Windows.Forms;
using static BoycoT_Password_Vault.Settings;

namespace BoycoT_Password_Vault
{
    public partial class FormPIN : Form
    {
        public bool PinCorrect = false;

        public FormPIN()
        {
            InitializeComponent();
            textBox1.Select();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text != settings.userPIN.DecryptBase64StringToText().ToUnsecureString())
                {
                    MessageBox.Show("Invalid PIN");
                    textBox1.Clear();
                }
                else
                {
                    PinCorrect = true;
                    Close();
                }
            }
        }
    }
}

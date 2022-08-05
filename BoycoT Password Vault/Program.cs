using System;
using System.IO;
using System.Windows.Forms;
using static BoycoT_Password_Vault.Settings;

namespace BoycoT_Password_Vault
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (LoginForm form = new LoginForm())
            {
                form.Text = "Enter or Create Credentials";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Shared.DbUserTableName = form.id.EncryptTextToBase64String();
                    Shared.SettingsFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"settings_{Shared.DbUserTableName.DecryptBase64StringToText().ToUnsecureString()}.json");

                    if (LoadSettings())
                    {
                        if (settings.appUserID == form.id.ToHash().ToUnsecureString() && settings.appPassword == form.pwd.ToHash().ToUnsecureString())
                        {
                            Application.Run(new Form1());
                        }
                    }
                    else
                    {
                        settings = new SettingsRoot
                        {
                            appUserID = form.id.ToHash().ToUnsecureString(),
                            appPassword = form.pwd.ToHash().ToUnsecureString(),
                            appTimeout = 1.00M,
                            dbServer = "database.json".ToSecureString().EncryptTextToBase64String(),
                            dbType = 0
                        };
                        SaveSettings();
                        MessageBox.Show("The Vault has been initialized. You can go into the settings by clicking on the program's icon in the upper-left corner of the window.");
                        Application.Run(new Form1());
                    }
                }
            }
        }
    }
}
